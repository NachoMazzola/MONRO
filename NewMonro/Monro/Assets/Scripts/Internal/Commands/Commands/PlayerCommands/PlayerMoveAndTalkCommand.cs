using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndTalkCommand : ICommand {
	private ICommand moveGOCommand;
	private ICommand talkCommand;

	private Dictionary<CommandType, bool> commandSteps;

	public GameObject Target;
	private GameObject playerGO;

	public PlayerMoveAndTalkCommand(GameObject target) {
		this.commandSteps = new Dictionary<CommandType, bool>();
		this.commandSteps.Add(CommandType.MoveGameObjectCommandType, false);
		this.commandSteps.Add(CommandType.TalkCommandType, false);


		this.Target = target;
		this.playerGO = WorldObjectsHelper.getPlayerGO();
		this.moveGOCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, null, false);
		((MoveGameObjectCommand)this.moveGOCommand).targetObject = WorldObjectsHelper.getPlayerGO();
		((MoveGameObjectCommand)this.moveGOCommand).targetPosition = this.Target.transform.position;
		((MoveGameObjectCommand)this.moveGOCommand).movementSpeed = WorldObjectsHelper.getPlayerGO().GetComponent<Moveable>().MovementSpeed;

		this.talkCommand = CommandFactory.CreateCommand(CommandType.TalkCommandType, this.Target, true);
	}

	public PlayerMoveAndTalkCommand(ICommandParamters moveGOCommandParameters, ICommandParamters talkParameters) {
		this.playerGO = WorldObjectsHelper.getPlayerGO();
		this.moveGOCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.playerGO, true, moveGOCommandParameters);
		this.talkCommand = CommandFactory.CreateCommand(CommandType.TalkCommandType, this.Target, true, talkParameters);
	}

	public PlayerMoveAndTalkCommand(MoveGameObjectCommand moveGOCommand, TalkCommand talkCommand) {
		this.playerGO = WorldObjectsHelper.getPlayerGO();
		this.moveGOCommand = moveGOCommand;
		this.talkCommand = talkCommand;
	}

	public override void Prepare() {
		this.moveGOCommand.Prepare();
		this.talkCommand.Prepare();
	}

	public override void WillStart() {
		this.moveGOCommand.WillStart();
		this.talkCommand.WillStart();
	}

	public override void UpdateCommand () {
		this.moveGOCommand.UpdateCommand();
		this.CheckForCommandStepsStates();
		this.UpdateCommands();
	}

	public override bool Finished() {
		return this.finished;
	}

	public override CommandType GetCommandType() { 
		return CommandType.PlayerMoveAndPickUpCommandType; 
	}

	private void CheckForCommandStepsStates() {
		if (this.moveGOCommand.Finished()) {
			this.commandSteps[CommandType.MoveGameObjectCommandType] = true;
		}
		if (this.talkCommand.Finished()) {
			this.commandSteps[CommandType.TalkCommandType] = true;
		}
	}

	private void UpdateCommands() {
		bool moveGoStepFinished = false;
		this.commandSteps.TryGetValue(CommandType.MoveGameObjectCommandType, out moveGoStepFinished);
		bool talkCommandStepFinished = false;
		this.commandSteps.TryGetValue(CommandType.TalkCommandType, out talkCommandStepFinished);

		if (moveGoStepFinished && !talkCommandStepFinished) {
			this.talkCommand.UpdateCommand();
		}
			
		this.finished = talkCommand.Finished();
	}
}
