using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using SQLite4Unity3d;

public class Item : MonoBehaviour {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	public string itemId;

	[HideInInspector]
	public string itemName {get; set;}
	[HideInInspector]
	public string combinesWithId;
	[HideInInspector]
	public string description {get; set;}
	[HideInInspector]
	public string resolvesPuzzleId {get; set;}
	[HideInInspector]
	public bool ItemHasBeenUsed {get; set;}
	[HideInInspector]
	public string combinedItemResultId {get; set;}

}
