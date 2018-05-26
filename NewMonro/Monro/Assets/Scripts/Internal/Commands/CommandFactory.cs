using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFactory {

	public static bool SetDefaultValues = true;

	public static ICommand CreateCommand(CommandType type, GameObject target, bool setDefaultValues = true, ICommandParamters parameters = null) {
		CommandType currentCommandType = type;
		switch (currentCommandType) {
		case CommandType.LookAtCommandType: {
				LookAtCommand lookAtCommand = new LookAtCommand();
				if (setDefaultValues) {
					lookAtCommand.lookable = target;
					lookAtCommand.whoLooks = WorldObjectsHelper.getPlayerGO ();
				}
				else if (parameters != null) {
					return new LookAtCommand(parameters);
				}
				return lookAtCommand;
			}
				
		case CommandType.TalkCommandType: {
				TalkCommand talkCommand = new TalkCommand();
				if (setDefaultValues) {
					talkCommand.conversationParticipants.Add(target);
					talkCommand.conversationParticipants.Add(WorldObjectsHelper.getPlayerGO());
					talkCommand.startingNode = target.GetComponent<Talkable>().StartingNode;
				}
				else if (parameters != null) {
					return new TalkCommand(parameters);
				}
				return talkCommand;
			}

		case CommandType.MoveGameObjectCommandType:
			MoveGameObjectCommand moveGOCommand = new MoveGameObjectCommand();
			if (setDefaultValues) {
				moveGOCommand.targetObject = WorldObjectsHelper.getPlayerGO();
				moveGOCommand.targetPosition = target.transform.position;
				moveGOCommand.movementSpeed = WorldObjectsHelper.getPlayerGO().GetComponent<Moveable>().MovementSpeed;
			}
			else if (parameters != null) {
				return new MoveGameObjectCommand(parameters);
			}
			return moveGOCommand;

		case CommandType.PutItemInInventoryCommandType:
			PutItemInInventoryCommand pInvCommand = new PutItemInInventoryCommand();
			if (setDefaultValues) {
				pInvCommand.itemTransform = target.GetComponent<ItemDroppable>().InventroyItem.gameObject;
			}
			else if (parameters != null) {
				return new PutItemInInventoryCommand(parameters);
			}
			return pInvCommand;

		case CommandType.AnimateCharacterCommandType:
			if (parameters != null) {
				return new AnimateCharacterCommand(parameters);
			}
			return new AnimateCharacterCommand();

		case CommandType.ChangeSpriteCommandType:
			if (parameters != null) {
				return new ChangeSpriteCommand(parameters);
			}
			return new ChangeSpriteCommand();

		case CommandType.MoveCameraCommandType:
			if (parameters != null) {
				return new MoveCameraCommand(parameters);
			}
			return new MoveCameraCommand();

		case CommandType.RemoveItemFromInventoryCommandType:
			if (parameters != null) {
				return new RemoveItemFromInventoryCommand(parameters);
			}
			return new RemoveItemFromInventoryCommand();

		case CommandType.DestroyGameObjectCommandType:
			if (parameters != null) {
				return new DestroyGameObjectCommand(parameters);
			}
			return new DestroyGameObjectCommand(target);


		case CommandType.PlayerMoveAndPickUpCommandType:
			return new PlayerMoveAndPickUpCommand(target);

		case CommandType.PLayerMoveAndTalkCommandType:
			return new PlayerMoveAndTalkCommand(target);

		case CommandType.unknown:
			return null;
		}

		return null;
	}
}
