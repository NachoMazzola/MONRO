﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutItemInInventoryCommand : ICommand {

	public string itemId;
	public ItemDroppable itemTransform;

	public PutItemInInventoryCommand() {
		
	}

	public PutItemInInventoryCommand(string itemId) {
		this.itemId = itemId;
	}

	public PutItemInInventoryCommand(ItemDroppable itemTransform) {
		this.itemTransform = itemTransform;
	}


	public override void Prepare() {

	}

	public override void WillStart() {
		if (this.itemId != null) {
			GameObject invObj = WorldObjectsHelper.getUIInventoryPanelContentGO();
			InventoryPanelHandler theInv = invObj.GetComponent<InventoryPanelHandler>();

			PlayerInventory inventory = invObj.gameObject.GetComponent<PlayerInventory>();

			DBItem itemExists = DBAccess.getComponent().itemsDataBase.GetItemById(this.itemId);
			if (itemExists != null) {
				inventory.AddItem(itemExists);

				GameObject pPrefab = Resources.Load(itemExists.ItemInventoryPrefab) as GameObject;
				theInv.AddItem(GameObject.Instantiate(pPrefab).transform);

				finished = true;
			}
		}
		else if (this.itemTransform != null) {
			GameObject invObj = WorldObjectsHelper.getUIInventoryPanelContentGO();
			InventoryPanelHandler theInv = invObj.GetComponent<InventoryPanelHandler>();

			Transform instanciatedItem = GameObject.Instantiate(this.itemTransform.InventroyItem);
			theInv.AddItem (instanciatedItem);

			GameObject.Destroy(this.itemTransform.gameObject);

			finished = true;
		}

	}

	public override void UpdateCommand () {
		
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.PutItemInInventoryCommandType; 
	}
}
