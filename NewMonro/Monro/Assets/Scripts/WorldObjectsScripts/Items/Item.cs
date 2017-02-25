using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

	public Transform ItemWorldRepTransform;

	protected string itemName;
	public string itemId;
	protected string itemDescription;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void ActivateInventoryItem() {
		this.gameObject.SetActive(true);
	}
		
}
