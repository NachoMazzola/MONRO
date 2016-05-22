using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour {

	private List<Item> items;

	void Awake() {
		items = new List<Item>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddItem(Item theItem) {
		items.Add(theItem);
	}

}
