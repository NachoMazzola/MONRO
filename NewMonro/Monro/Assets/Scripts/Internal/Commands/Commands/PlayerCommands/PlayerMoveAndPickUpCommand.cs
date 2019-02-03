using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveAndPickUpCommand: ICommand {

	private MoveGameObjectCommand moveGOCommand;
	private PutItemInInventoryCommand putItemInInventoryCommand;
	private DestroyGameObjectCommand destroyGOCommand;

	private Dictionary<CommandType, bool> commandSteps;

	public GameObject Target;
	private GameObject playerGO;

	public PlayerMoveAndPickUpCommand(GameObject target) {
		this.playerGO = WorldObjectsHelper.getPlayerGO();

		this.commandSteps = new Dictionary<CommandType, bool>();
		this.commandSteps.Add(CommandType.MoveGameObjectCommandType, false);
		this.commandSteps.Add(CommandType.PutItemInInventoryCommandType, false);
		this.commandSteps.Add(CommandType.DestroyGameObjectCommandType, false);

		this.Target = target;
		this.moveGOCommand = (MoveGameObjectCommand)CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, null, false);
		this.moveGOCommand.targetObject = this.playerGO;
		this.moveGOCommand.targetPosition = this.Target.transform.position;
		this.moveGOCommand.movementSpeed = this.playerGO.GetComponent<Moveable>().MovementSpeed;

		this.putItemInInventoryCommand = (PutItemInInventoryCommand)CommandFactory.CreateCommand(CommandType.PutItemInInventoryCommandType, this.Target, true);
		this.putItemInInventoryCommand.itemTransform = target;
		this.destroyGOCommand = (DestroyGameObjectCommand)CommandFactory.CreateCommand(CommandType.DestroyGameObjectCommandType, this.Target, true);
	}

	public PlayerMoveAndPickUpCommand(ICommandParamters moveGOCommandParameters, ICommandParamters pickUpItemParameters) {
		this.moveGOCommand = (MoveGameObjectCommand)CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.Target, true, moveGOCommandParameters);
		this.putItemInInventoryCommand = (PutItemInInventoryCommand)CommandFactory.CreateCommand(CommandType.PutItemInInventoryCommandType, this.Target, true, pickUpItemParameters);
	}

	public PlayerMoveAndPickUpCommand(MoveGameObjectCommand moveGOCommand, PutItemInInventoryCommand putItemInInvCommand) {
		this.moveGOCommand = moveGOCommand;
		this.putItemInInventoryCommand = putItemInInvCommand;
	}

	public override void Prepare() {
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

			GameEntity ge = playerGO.GetComponent<GameEntity>();
			this.playerGO.GetComponent<AnimationsCoordinatorHub>().PlayAnimation(Animations.PickUp, ge);
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
