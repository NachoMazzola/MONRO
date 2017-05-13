using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBItemLoader : MonoBehaviour {

	public string itemId;

	[HideInInspector]
	public DBItem itemModel;

	void Awake() {
		itemModel = DBAccess.getComponent().itemsDataBase.GetItemById(itemId);
		if (itemModel == null) {
			Debug.LogError("No hay ningun item en la base con Id = " + itemId);
		}
	}
}
