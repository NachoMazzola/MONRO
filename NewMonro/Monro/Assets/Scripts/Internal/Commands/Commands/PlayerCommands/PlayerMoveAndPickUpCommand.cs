using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveAndPickUpCommand: ICommand {

	private MoveGameObjectCommand moveGOCommand;
	private PutItemInInventoryCommand putItemInInventoryCommand;
	private DestroyGameObjectCommand destroyGOCommand;

    private ICommand currentCommand;

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
        this.moveGOCommand = new MoveGameObjectCommand(this.playerGO, this.Target.transform.position, this.playerGO.GetComponent<Moveable>().MovementSpeed);

        this.putItemInInventoryCommand = new PutItemInInventoryCommand(target);
		this.destroyGOCommand = new DestroyGameObjectCommand(target);

        this.currentCommand = this.moveGOCommand;
    }

	public PlayerMoveAndPickUpCommand(ICommandParamters moveGOCommandParameters, ICommandParamters pickUpItemParameters) {
        this.moveGOCommand = new MoveGameObjectCommand(moveGOCommandParameters);
        this.putItemInInventoryCommand = new PutItemInInventoryCommand(pickUpItemParameters);
	}

	public PlayerMoveAndPickUpCommand(MoveGameObjectCommand moveGOCommand, PutItemInInventoryCommand putItemInInvCommand) {
		this.moveGOCommand = moveGOCommand;
		this.putItemInInventoryCommand = putItemInInvCommand;
	}

	public override void Prepare() {
        this.currentCommand.Prepare();

    }

	public override void WillStart() {
        this.isRunning = true;
		this.currentCommand.WillStart();
	}

	public override void UpdateCommand () {
        if (!isRunning || this.currentCommand == null)
        {
            return;
        }

		this.currentCommand.UpdateCommand();
		//this.CheckForCommandStepsStates();
		this.UpdateCommands();
	}

	public override CommandType GetCommandType() { 
		return CommandType.PlayerMoveAndPickUpCommandType; 
	}

	private void CheckForCommandStepsStates() {
		if (this.moveGOCommand.Finished()) {
			this.commandSteps[CommandType.MoveGameObjectCommandType] = true;
		}
	}

	private void UpdateCommands() {
		if (this.moveGOCommand.Finished())
        {
			this.putItemInInventoryCommand.Prepare();
			this.putItemInInventoryCommand.WillStart();
            this.putItemInInventoryCommand.ExecuteOnce();

            GameEntity ge = playerGO.GetComponent<GameEntity>();
            this.playerGO.GetComponent<AnimationsCoordinatorHub>().PlayAnimation(Animations.PickUp, ge);

            this.destroyGOCommand.Prepare();
            this.destroyGOCommand.WillStart();
            this.destroyGOCommand.ExecuteOnce();


            this.currentCommand = null;
            this.isRunning = false;
        }
	}
}
