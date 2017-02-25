using UnityEngine;
using System.Collections;

public class WorldItemCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other && other.gameObject.tag == "Player") {
			WorldItem item = this.transform.parent.gameObject.GetComponent<WorldItem>();
			item.ItemIsOverObject(other.transform);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other && other.gameObject.tag == "Player") {
			WorldItem item = this.transform.parent.gameObject.GetComponent<WorldItem>();
			item.ItemHasBeenReleasedOverObject(other.transform);
		}
	}
}
