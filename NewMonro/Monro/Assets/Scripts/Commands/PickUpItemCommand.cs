using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemCommand : ICommand {

	public ItemDroppable itemDroppable;

	private MoveGameObjectCommand moveToCommand;

	public override void Prepare() {
		this.moveToCommand = new MoveGameObjectCommand(WorldObjectsHelper.getPlayerGO(), this.itemDroppable.gameObject.transform.position, 4);
		moveToCommand.Prepare();
	}

	public override void WillStart() {
		GameObject invObj = WorldObjectsHelper.getUIInventoryGO();
		UIInventory theInv = invObj.GetComponent<UIInventory>();
		//PlayerInventory pInventory = invObj.GetComponent<PlayerInventory>();

		//pInventory.AddItemById (itemToPickUp.GetComponent<InteractiveObject> ().Item.GetComponent<DBItemLoader> ().itemId);
		theInv.AddItemToInventory (this.itemDroppable.InventroyItem);

		GameObject.Destroy(this.itemDroppable.gameObject);

		finished = true;
	}

	public override void UpdateCommand () {
		if (this.moveToCommand != null) {
			this.moveToCommand.UpdateCommand();	

			if (this.moveToCommand.Finished() && this.finished == false) {
				this.WillStart();
			}
		}
	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.PickUpItemCommandType; 
	}
}
