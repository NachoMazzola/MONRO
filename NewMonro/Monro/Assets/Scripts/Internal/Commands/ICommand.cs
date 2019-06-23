﻿using System.Collections;
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
	CannotApplyTraitCommandCommandType,

	//Player commands
	PlayerMoveAndPickUpCommandType,
	PLayerMoveAndTalkCommandType,

	unknown
}
	
public interface ICommandParamters {
	CommandType GetCommandType();
}

public abstract class ICommand {

    public bool isRunning;

	public bool isInterrutable = true;

	public virtual void Prepare() {}
	public virtual void WillStart() {}
	public virtual void UpdateCommand() {}
	public virtual void Stop() {}
    public virtual void ExecuteOnce() {}

    public virtual bool Finished()
    {
        return !isRunning;
    }

    public virtual CommandType GetCommandType() { return CommandType.unknown; }
}
