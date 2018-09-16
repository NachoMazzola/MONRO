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

	public List<CommandType> commandQueue;
	public List<ICommand> commands;

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
		this.commandQueue = new List<CommandType> ();
		this.commands = new List<ICommand>();
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
			Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(pos);
			this.ExecuteCurrentCommand(hitCollider);

				
			//Tap (GetTappedGameObject ());
			mouseIsPressed = true;
			return;
		}


		if (Input.GetMouseButton (0) && !mouseIsPressed) {
		
			HoldTap (GetTappedGameObject ());	
			mouseIsPressed = true;
			return;
		}
	}

	private void ExecuteCurrentCommand (Collider2D collider) {
		if (collider == null) {
			if (this.commandQueue.Count > 0) {
				WorldObjectsHelper.VerbsPanelUIGO().GetComponent<VerbsButtonPanelHandler> ().ResetButtons ();	
			}
			this.commandQueue.RemoveRange(0, this.commandQueue.Count);
			return;
		}
			
		Tappable tappable = collider.gameObject.GetComponent<Tappable> ();
		if (tappable != null && tappable.boxCollider == collider) {
			CommandManager.getComponent().target = collider.gameObject;
			if (this.commandQueue.Count > 0) {
				foreach (CommandType cType in this.commandQueue) {
					ICommand comm = CommandFactory.CreateCommand (cType, collider.gameObject);
					if (comm == null) {
						continue;
					}
					CommandManager.getComponent ().QueueCommand (comm);
				}
				//start executing command queue
				CommandManager.getComponent ().ExecuteCurrentCommand ();

				this.commandQueue = new List<CommandType> ();
				WorldObjectsHelper.VerbsPanelUIGO ().GetComponent<VerbsButtonPanelHandler> ().ResetButtons ();	
			}
		}
	}


	/// <summary>
	/// Old SHIT
	/// </summary>

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
