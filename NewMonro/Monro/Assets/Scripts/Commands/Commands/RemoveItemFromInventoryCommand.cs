using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemFromInventoryCommand : ICommand {

	public List<string> ItemsIdsToRemove;

	public RemoveItemFromInventoryCommand() {
		
	}

	public RemoveItemFromInventoryCommand(List<string> itemsIdsToRemove) {
		this.ItemsIdsToRemove = itemsIdsToRemove;
	}

	public override void Prepare() {

	}

	public override void WillStart() {
		if (this.ItemsIdsToRemove == null) {
			return;
		}

		GameObject invObj = WorldObjectsHelper.getUIInventoryPanelContentGO();
		InventoryPanelHandler theInv = invObj.GetComponent<InventoryPanelHandler>();

		theInv.RemoveItems(this.ItemsIdsToRemove);

		invObj.GetComponent<PlayerInventory>().MarkItemsAsUsed(this.ItemsIdsToRemove);
	}

	public override void UpdateCommand () {

	}

	public override bool Finished() {
		return finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.RemoveItemFromInventoryCommandType; 
	}
}
