using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFactory {

	public static bool SetDefaultValues = true;

	public static ICommand CreateCommand(CommandType type, GameObject target) {
		CommandType currentCommandType = type;
		switch (currentCommandType) {
		case CommandType.LookAtCommandType: {
				LookAtCommand lookAtCommand = new LookAtCommand();
				if (CommandFactory.SetDefaultValues) {
					lookAtCommand.lookable = target.GetComponent<Lookable>();
					lookAtCommand.whoLooks = WorldObjectsHelper.getPlayerGO ().GetComponent<TextboxDisplayer> ();
					lookAtCommand.WillStart ();		
				}
				return lookAtCommand;
			}

		case CommandType.PickUpItemCommandType: {
				PickUpItemCommand pickUpCommand = new PickUpItemCommand();
				if (CommandFactory.SetDefaultValues) {
					pickUpCommand.itemDroppable = target.GetComponent<ItemDroppable>();
					pickUpCommand.Prepare();	
				}
				return pickUpCommand;
			}

		case CommandType.TalkCommandType: {
				TalkCommand talkCommand = new TalkCommand();
				if (CommandFactory.SetDefaultValues) {
					talkCommand.conversationParticipants.Add(target.transform);
					talkCommand.conversationParticipants.Add(WorldObjectsHelper.getPlayerGO().transform);
					talkCommand.startingNode = target.GetComponent<Talkable>().StartingNode;
					talkCommand.Prepare();
					talkCommand.WillStart();	
				}
				return talkCommand;
			}
		}

		return null;
	}
}
