using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class WorldInteractionController: MonoBehaviour
{

	public enum WorldInteractionType
	{
		Empty,
		TapOnGameObject,
		TapHoldOnGameObject
	}

	private bool mouseIsPressed;
	private WorldInteractionType currentInteraction;
	private List<IWorldInteractionObserver> worldObservers;

	Transform lastTappedObject;

	[HideInInspector]
	public bool enableInteractions = true;

	private ICommand currentCommand;
	public CommandType currentCommandType = CommandType.unknown;

	static public WorldInteractionController getComponent ()
	{
		GameObject controller = WorldObjectsHelper.getWorldInteractionControllerGO ();
		if (controller) {
			return controller.GetComponent<WorldInteractionController> ();
		}
		return null;
	}

	void Awake ()
	{
		this.worldObservers = new List<IWorldInteractionObserver> ();
		this.currentCommandType = CommandType.unknown;
	}

	void OnDestroy ()
	{
		worldObservers.RemoveAll (ImplementsInterface);
		worldObservers = null;
	}

	private static bool ImplementsInterface (IWorldInteractionObserver s)
	{
		return true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (enableInteractions == false) {
			return;
		}
	
		if (Input.GetMouseButtonUp (0)) {
			mouseIsPressed = false;
			return;
		}

		if (Input.GetMouseButtonDown (0) && !mouseIsPressed) {

			this.ExecuteCurrentCommand();
			//Tap (GetTappedGameObject ());
			mouseIsPressed = true;
			return;
		}


		if (Input.GetMouseButton (0) && !mouseIsPressed) {
		
			HoldTap (GetTappedGameObject ());	
			mouseIsPressed = true;
			return;
		}

		if (this.currentCommand != null) {
			this.currentCommand.UpdateCommand ();

			if (this.currentCommand.Finished()) {
				this.currentCommand = null;
				WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler>().ResetButtons();
			}
		}
	}

	private void ExecuteCurrentCommand()
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D[] hitColliders = Physics2D.OverlapPointAll (pos); 
		if (hitColliders != null && hitColliders.Length > 0) {
			foreach (BoxCollider2D collider in hitColliders) {
				Tappable tappable = collider.gameObject.GetComponent<Tappable> ();
				if (tappable != null && tappable.boxCollider == collider) {
					//podriamos por default siempre hacer un look at si no se eligio ningun
					//comando del menu de comandos. Esto es, instanciar aca un LookAtCommand 
					//y ejecutarlo pasandole como recibidor el collider.gameObject.

					//En caso de que se haya elegido un comando en particular, se deberia
					//ejecutar pasandole el collider.gameObject como recibidor del comando

					if (this.currentCommandType == CommandType.unknown) {
						this.currentCommand = CommandFactory.CreateCommand(CommandType.LookAtCommandType, collider.gameObject);
						return;
					}

					this.currentCommand = CommandFactory.CreateCommand(this.currentCommandType, collider.gameObject);
				}
			}
		}
		else {
			WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler>().ResetButtons();
		}
	}

	private void ExecuteCurrentCommandForTarget (GameObject target)
	{
		/** By default always create a look at command */
		if (currentCommand == null) {
			Lookable tappedLookable = target.GetComponent<Lookable> ();
			if (tappedLookable != null) {
				LookAtCommand lookAtCommand = new LookAtCommand ();
				lookAtCommand.lookable = target.GetComponent<Lookable>();
				lookAtCommand.whoLooks = WorldObjectsHelper.getPlayerGO ().GetComponent<TextboxDisplayer> ();
				lookAtCommand.WillStart ();	
			}
			return;
		}


	}

	private GameObject GetTappedGameObject ()
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GameObject tappedGO = ColliderFoundOnTouch (pos);
		currentInteraction = WorldInteractionType.TapOnGameObject;				

		//handle UI Tap
		if (tappedGO == null) {
			EventSystem c = EventSystem.current;
			if (c.currentSelectedGameObject != null) {
				if (EventSystem.current.currentSelectedGameObject.tag != "UIElement") {
					return null;
				} else {
					tappedGO = EventSystem.current.currentSelectedGameObject;

				}
			} 	
		} else {
			lastTappedObject = tappedGO.transform;	
		}

		return tappedGO;
	}


	private void Tap (GameObject goTapped)
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (goTapped != null) {
			foreach (IWorldInteractionObserver obs in worldObservers) {
				//obs.IWOTapped (pos, goTapped);
			}
		} else {
			currentInteraction = WorldInteractionType.Empty;
			foreach (IWorldInteractionObserver obs in worldObservers) {
				//obs.IWOTapped (pos, null);
			}
		}
	}

	private void HoldTap (GameObject goTapped)
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (goTapped != null) {
			foreach (IWorldInteractionObserver obs in worldObservers) {
				obs.IWOTapHold (pos, goTapped);
			}
		} else {
			currentInteraction = WorldInteractionType.Empty;
			foreach (IWorldInteractionObserver obs in worldObservers) {
				obs.IWOTapHold (pos, null);
			}
		}
	}

	public void InterruptInteractions ()
	{
		foreach (IWorldInteractionObserver obs in worldObservers) {
			if (this.lastTappedObject != obs.IWOGetTransform ()) {
				obs.IWOInterruptInteractions ();	
			}
		}
	}

	private GameObject ColliderFoundOnTouch (Vector2 targetPosition)
	{
		
		Collider2D[] hitColliders = Physics2D.OverlapPointAll (targetPosition); 
		if (hitColliders != null && hitColliders.Length > 0) {
			return hitColliders [0].gameObject;
		}

		return null;
	}

	private void NotifyObservers ()
	{
		
	}

	public void AddObserver (IWorldInteractionObserver obs)
	{
		worldObservers.Add (obs);
	}

	public void RemoveObserver (IWorldInteractionObserver obs)
	{
		worldObservers.Remove (obs);
	}

	public void RemoveAll ()
	{
		worldObservers.RemoveRange (0, worldObservers.Count);
	}
}
