using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RemoveItemFromInventoryCommandParameters: ICommandParamters {
	public List<string> ItemsIdsToRemove;

	public int itemCount;

	public CommandType GetCommandType() {
		return CommandType.RemoveItemFromInventoryCommandType;
	}
}


public class RemoveItemFromInventoryCommand : ICommand {

	public List<string> ItemsIdsToRemove;

	public RemoveItemFromInventoryCommand() {
		
	}

	public RemoveItemFromInventoryCommand(ICommandParamters parameters) {
		RemoveItemFromInventoryCommandParameters p = (RemoveItemFromInventoryCommandParameters)parameters;
		this.ItemsIdsToRemove = p.ItemsIdsToRemove;
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

	public override CommandType GetCommandType() { 
		return CommandType.RemoveItemFromInventoryCommandType; 
	}
}
