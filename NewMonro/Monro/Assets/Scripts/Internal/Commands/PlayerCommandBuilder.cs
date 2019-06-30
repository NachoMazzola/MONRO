using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 *
 *  DEPRECATED!!!!
 * 
 */


/**
 * This class focuses on building commands exclusively applied to the PJ
 * when player interacts with a UI (like verbs panel or radial menu)
*/

public enum UIType {
	VerbsPanel,
	RadialMenu
}

public class PlayerCommandBuilder {
	public UIType uiType = UIType.VerbsPanel;
	public GameObject target;

	public void CreateLookAtCommand() {
		
		if (this.uiType == UIType.VerbsPanel) {
			List<CommandType> commands = new List<CommandType>();
			commands.Add(CommandType.LookAtCommandType);

			WorldInteractionController.getComponent().commandQueue = commands;	
		}
		else {
			ICommand lookAt = CommandFactory.CreateCommand(CommandType.LookAtCommandType, this.target);
			CommandManager.getComponent().QueueCommand(lookAt, true);
		}
	}

	public void CreateTalkToCommand() {
		
		if (this.uiType == UIType.VerbsPanel) {
			List<CommandType> commands = new List<CommandType>();
			commands.Add(CommandType.PLayerMoveAndTalkCommandType);
			WorldInteractionController.getComponent().commandQueue = commands;	
		}
		else {
			ICommand moveGOCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.target);
			ICommand talkTo = CommandFactory.CreateCommand(CommandType.TalkCommandType, this.target);
			CommandManager.getComponent().QueueCommand(moveGOCommand);
			CommandManager.getComponent().QueueCommand(talkTo, true);
		}
	}

	public void CreatePickUpCommand() {
		if (this.uiType == UIType.VerbsPanel) {
			List<CommandType> commands = new List<CommandType>();
			commands.Add(CommandType.PlayerMoveAndPickUpCommandType);
			WorldInteractionController.getComponent().commandQueue = commands;	
		}
		else {
			ICommand moveGOCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.target);	
			ICommand putInInv = CommandFactory.CreateCommand(CommandType.PutItemInInventoryCommandType, this.target);
			ICommand destroy = CommandFactory.CreateCommand(CommandType.DestroyGameObjectCommandType, this.target);

			CommandManager.getComponent().QueueCommand(moveGOCommand);
			CommandManager.getComponent().QueueCommand(putInInv);
			CommandManager.getComponent().QueueCommand(destroy, true);

		}
	}

}
