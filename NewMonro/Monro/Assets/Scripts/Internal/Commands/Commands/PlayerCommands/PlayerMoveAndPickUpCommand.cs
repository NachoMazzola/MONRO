using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndPickUpCommand: ICommand {

	private ICommand moveGOCommand;
	private ICommand putItemInInventoryCommand;
	private ICommand destroyGOCommand;

	private AnimationCoordinator playerAnimCoordinator;

	private Dictionary<CommandType, bool> commandSteps;

	public GameObject Target;

	public PlayerMoveAndPickUpCommand(GameObject target) {
		this.commandSteps = new Dictionary<CommandType, bool>();
		this.commandSteps.Add(CommandType.MoveGameObjectCommandType, false);
		this.commandSteps.Add(CommandType.PutItemInInventoryCommandType, false);
		this.commandSteps.Add(CommandType.DestroyGameObjectCommandType, false);


		this.Target = target;
		this.moveGOCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.Target, true);
		this.putItemInInventoryCommand = CommandFactory.CreateCommand(CommandType.PutItemInInventoryCommandType, this.Target, true);
		this.destroyGOCommand = CommandFactory.CreateCommand(CommandType.DestroyGameObjectCommandType, this.Target, true);
	}

	public PlayerMoveAndPickUpCommand(ICommandParamters moveGOCommandParameters, ICommandParamters pickUpItemParameters) {
		this.moveGOCommand = CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.Target, true, moveGOCommandParameters);
		this.putItemInInventoryCommand = CommandFactory.CreateCommand(CommandType.PutItemInInventoryCommandType, this.Target, true, pickUpItemParameters);
	}

	public PlayerMoveAndPickUpCommand(MoveGameObjectCommand moveGOCommand, PutItemInInventoryCommand putItemInInvCommand) {
		this.moveGOCommand = moveGOCommand;
		this.putItemInInventoryCommand = putItemInInvCommand;
	}

	public override void Prepare() {
		this.playerAnimCoordinator = WorldObjectsHelper.getPlayerGO().GetComponent<AnimationCoordinator>();
		this.playerAnimCoordinator.PlayAnimation(Animations.PlayerWalk, PlayerAnimations.animParamIsWalking);

		this.moveGOCommand.Prepare();
	}

	public override void WillStart() {
		this.moveGOCommand.WillStart();
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
		if (this.putItemInInventoryCommand.Finished()) {
			this.commandSteps[CommandType.PutItemInInventoryCommandType] = true;
		}
		if (this.destroyGOCommand.Finished()) {
			this.commandSteps[CommandType.DestroyGameObjectCommandType] = true;
		}
	}

	private void UpdateCommands() {
		bool moveGoStepFinished = false;
		this.commandSteps.TryGetValue(CommandType.MoveGameObjectCommandType, out moveGoStepFinished);
		bool putItemInInventoryStepFinished = false;
		this.commandSteps.TryGetValue(CommandType.PutItemInInventoryCommandType, out putItemInInventoryStepFinished);
		bool destroyGOStepFinished = false;
		this.commandSteps.TryGetValue(CommandType.DestroyGameObjectCommandType, out destroyGOStepFinished);

		if (moveGoStepFinished && !putItemInInventoryStepFinished && !destroyGOStepFinished) {
			this.putItemInInventoryCommand.Prepare();
			this.putItemInInventoryCommand.WillStart();
			this.putItemInInventoryCommand.UpdateCommand();

			this.playerAnimCoordinator.PlayAnimation(Animations.PlayePickUp, PlayerAnimations.animParamIsPickingUp);
		}

		if (putItemInInventoryStepFinished && moveGoStepFinished && !destroyGOStepFinished) {
			this.destroyGOCommand.Prepare();
			this.destroyGOCommand.WillStart();
		}

		if (destroyGOStepFinished) {
			this.finished = true;
		}
	}
}
