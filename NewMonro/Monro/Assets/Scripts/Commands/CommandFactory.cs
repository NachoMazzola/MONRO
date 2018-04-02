using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFactory {

	public static bool SetDefaultValues = true;

	public static ICommand CreateCommand(CommandType type, GameObject target, bool setDefaultValues = true) {
		CommandType currentCommandType = type;
		switch (currentCommandType) {
		case CommandType.LookAtCommandType: {
				LookAtCommand lookAtCommand = new LookAtCommand();
				if (setDefaultValues) {
					lookAtCommand.lookable = target.GetComponent<Lookable>();
					lookAtCommand.whoLooks = WorldObjectsHelper.getPlayerGO ().GetComponent<TextboxDisplayer> ();		
				}
				return lookAtCommand;
			}
				
		case CommandType.TalkCommandType: {
				TalkCommand talkCommand = new TalkCommand();
				if (setDefaultValues) {
					talkCommand.conversationParticipants.Add(target.transform);
					talkCommand.conversationParticipants.Add(WorldObjectsHelper.getPlayerGO().transform);
					talkCommand.startingNode = target.GetComponent<Talkable>().StartingNode;
				}
				return talkCommand;
			}

		case CommandType.MoveGameObjectCommandType:
			MoveGameObjectCommand moveGOCommand = new MoveGameObjectCommand();
			if (setDefaultValues) {
				moveGOCommand.targetObject = WorldObjectsHelper.getPlayerGO();
				moveGOCommand.targetPosition = target.transform.position;
				moveGOCommand.movementSpeed = 4;
			}
			return moveGOCommand;

		case CommandType.PutItemInInventoryCommandType:
			PutItemInInventoryCommand pInvCommand = new PutItemInInventoryCommand();
			if (setDefaultValues) {
				pInvCommand.itemTransform = target.GetComponent<ItemDroppable>();
			}
			return pInvCommand;

		case CommandType.AnimateCharacterCommandType:
			return new AnimateCharacterCommand();

		case CommandType.ChangeSpriteCommandType:
			return new ChangeSpriteCommand();

		case CommandType.MoveCameraCommandType:
			return new MoveCameraCommand();

		case CommandType.RemoveItemFromInventoryCommandType:
			return new RemoveItemFromInventoryCommand();

		case CommandType.unknown:
			return null;
		}

		return null;
	}
}
