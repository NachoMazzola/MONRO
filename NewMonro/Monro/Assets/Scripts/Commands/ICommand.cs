using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType {
	AnimateCharacterCommandType,
	ChangeSpriteCommandType, 
	LookAtCommandType,
	MoveCameraCommandType,
	MoveGameObjectCommandType,
	TalkCommandType,
	PutItemInInventoryCommandType,
	RemoveItemFromInventoryCommandType,
	DestroyGameObjectCommandType,
	unknown
}
	
public interface ICommandParamters {
	CommandType GetCommandType();
}

public abstract class ICommand {

	protected bool finished;

	public virtual void Prepare() {}
	public virtual void WillStart() {}
	public virtual void UpdateCommand() {}
	public virtual bool Finished() { return false; }

	public virtual CommandType GetCommandType() { return CommandType.unknown; }
}
