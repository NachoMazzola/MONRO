using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PutItemInInventoryCommandParameters: ICommandParamters {
	public string itemId;
	public GameObject itemTransform;

	public CommandType GetCommandType() {
		return CommandType.PutItemInInventoryCommandType;
	}
}

public class PutItemInInventoryCommand : ICommand {

	public string itemId;
	public GameObject itemTransform;

	private ItemDroppable iDroppable;

	public PutItemInInventoryCommand() {
		
	}

	public PutItemInInventoryCommand(ICommandParamters parameters) {
		PutItemInInventoryCommandParameters p = (PutItemInInventoryCommandParameters)parameters;
		this.itemId = p.itemId;
		this.itemTransform = p.itemTransform;

		this.iDroppable = this.itemTransform.GetComponent<ItemDroppable>();
	}

	public PutItemInInventoryCommand(string itemId) {
		this.itemId = itemId;
	}

	public PutItemInInventoryCommand(GameObject itemTransform) {
		this.itemTransform = itemTransform;
		this.iDroppable = this.itemTransform.GetComponent<ItemDroppable>();
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

			ItemDroppable iDp = this.itemTransform.GetComponent<ItemDroppable>();
			Transform instanciatedItem = GameObject.Instantiate(iDp.InventroyItem);
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
