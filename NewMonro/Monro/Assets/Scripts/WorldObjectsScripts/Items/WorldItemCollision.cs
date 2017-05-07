using UnityEngine;
using System.Collections;

public class WorldItemCollision : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		DraggableWorldItem item = this.transform.parent.gameObject.GetComponent<DraggableWorldItem> ();
		item.ItemIsOverObject (other.transform);

		switch (other.gameObject.tag) {
		case "Player":
			break;

		case "InteractiveObject":
			break;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		DraggableWorldItem item = this.transform.parent.gameObject.GetComponent<DraggableWorldItem> ();
		item.ItemHasBeenReleasedOverObject(other.transform);

		switch (other.gameObject.tag) {
		case "Player":
			break;

		case "InteractiveObject":
			break;
		}
	}
}
