using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDropHandler {

	protected string itemName;
	protected string itemId;
	protected string itemDescription;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnDrop(PointerEventData eventData) {
		
	}
}
