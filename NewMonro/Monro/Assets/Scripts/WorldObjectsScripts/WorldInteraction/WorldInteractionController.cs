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

	static public WorldInteractionController getComponent () {
		GameObject controller = GameObject.Find ("WorldInteractionController");
		if (controller) {
			return controller.GetComponent<WorldInteractionController> ();
		}
		return null;
	}

	void Awake ()
	{
		worldObservers = new List<IWorldInteractionObserver> ();
	}

	void OnDestroy() {
		worldObservers.RemoveAll(ImplementsInterface);
		worldObservers = null;
	}

	private static bool ImplementsInterface(IWorldInteractionObserver s)
	{
		return true;
	}

	// Update is called once per frame
	void Update ()
	{
	
		if (Input.GetMouseButtonUp (0)) {
			mouseIsPressed = false;
			return;
		}

		if (Input.GetMouseButtonDown (0) && !mouseIsPressed) {
			
			Tap (GetTappedGameObject());
			mouseIsPressed = true;
			return;
		}


		if (Input.GetMouseButton (0) && !mouseIsPressed) {
		
			HoldTap (GetTappedGameObject());	
			mouseIsPressed = true;
			return;
		}

	}

	private GameObject GetTappedGameObject() {
		Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GameObject tappedGO = ColliderFoundOnTouch (pos);
		currentInteraction = WorldInteractionType.TapOnGameObject;				

		//handle UI Tap
		if (tappedGO == null) {
			EventSystem c = EventSystem.current;
			if (c.currentSelectedGameObject != null) {
				if (EventSystem.current.currentSelectedGameObject.tag != "UIElement") {
					return null;
				}
				else {
					tappedGO = EventSystem.current.currentSelectedGameObject;
				}
			} 	
		}

		return tappedGO;
	}
		

	private void Tap (GameObject goTapped)
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (goTapped != null) {
			foreach (IWorldInteractionObserver obs in worldObservers) {
				obs.IWOTapped (pos, goTapped);
			}
		} else {
			currentInteraction = WorldInteractionType.Empty;
			foreach (IWorldInteractionObserver obs in worldObservers) {
				obs.IWOTapped (pos, null);
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
