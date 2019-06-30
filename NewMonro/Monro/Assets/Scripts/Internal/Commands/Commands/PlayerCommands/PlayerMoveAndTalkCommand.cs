using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAndTalkCommand : ICommand, TalkableCommand
{
    private MoveGameObjectCommand moveGOCommand;
    private TalkCommand talkCommand;

    private Dictionary<CommandType, bool> commandSteps;

    public GameObject Target;
    private GameObject playerGO;

    public PlayerMoveAndTalkCommand(GameObject target, string startingNode, List<GameObject> participants, Vector2 talkPosition)
    { 
        moveGOCommand = new MoveGameObjectCommand();
        moveGOCommand.targetObject = WorldObjectsHelper.getPlayerGO();
        moveGOCommand.targetPosition = talkPosition;
        moveGOCommand.movementSpeed = WorldObjectsHelper.getPlayerGO().GetComponent<Moveable>().MovementSpeed;

        talkCommand = new TalkCommand();
        talkCommand.startingNode = startingNode;
        talkCommand.conversationParticipants = participants;

        this.commandSteps = new Dictionary<CommandType, bool>();
        this.commandSteps.Add(CommandType.MoveGameObjectCommandType, false);
        this.commandSteps.Add(CommandType.TalkCommandType, false);
    }

    public PlayerMoveAndTalkCommand(GameObject target, Vector2 talkPosition) {
        this.commandSteps = new Dictionary<CommandType, bool>();
        this.commandSteps.Add(CommandType.MoveGameObjectCommandType, false);
        this.commandSteps.Add(CommandType.TalkCommandType, false);


        this.Target = target;
        this.playerGO = WorldObjectsHelper.getPlayerGO();
        this.moveGOCommand = (MoveGameObjectCommand)CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, null, false);
        this.moveGOCommand.targetObject = WorldObjectsHelper.getPlayerGO();
        this.moveGOCommand.targetPosition = talkPosition;
        this.moveGOCommand.movementSpeed = WorldObjectsHelper.getPlayerGO().GetComponent<Moveable>().MovementSpeed;

        this.talkCommand = (TalkCommand)CommandFactory.CreateCommand(CommandType.TalkCommandType, this.Target, true);
    }

    public PlayerMoveAndTalkCommand(ICommandParamters moveGOCommandParameters, ICommandParamters talkParameters) {
        this.playerGO = WorldObjectsHelper.getPlayerGO();
        this.moveGOCommand = (MoveGameObjectCommand)CommandFactory.CreateCommand(CommandType.MoveGameObjectCommandType, this.playerGO, true, moveGOCommandParameters);
        this.talkCommand = (TalkCommand)CommandFactory.CreateCommand(CommandType.TalkCommandType, this.Target, true, talkParameters);
    }

    public PlayerMoveAndTalkCommand(MoveGameObjectCommand moveGOCommand, TalkCommand talkCommand) {
        this.playerGO = WorldObjectsHelper.getPlayerGO();
        this.moveGOCommand = moveGOCommand;
        this.talkCommand = talkCommand;
    }

    public override void Prepare() {
        this.moveGOCommand.Prepare();
    }

    public override void WillStart() {
        this.isRunning = true;
        this.moveGOCommand.WillStart();
    }

    public override void UpdateCommand() {
        if (!this.isRunning)
        {
            return;
        }
        this.CheckForCommandStepsStates();
        this.UpdateCommands();
    }

    public override CommandType GetCommandType() {
        return CommandType.PlayerMoveAndPickUpCommandType;
    }

    private void CheckForCommandStepsStates() {
        if (this.moveGOCommand.Finished()) {
            if (this.commandSteps[CommandType.MoveGameObjectCommandType] == false)
            {
                this.talkCommand.Prepare();
                this.talkCommand.WillStart();
            }
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

        if (moveGoStepFinished == false)
        {
            this.moveGOCommand.UpdateCommand();
        }

        if (moveGoStepFinished && !talkCommandStepFinished) {
            this.talkCommand.UpdateCommand();
        }

        this.isRunning = this.talkCommand.Finished();
    }

    public override void Stop() {
        this.moveGOCommand.Stop();
        this.talkCommand.Stop();
    }

    public void SetStartingNode(string startingNode)
    {
        this.talkCommand.SetStartingNode(startingNode);
    }

    public void SetDialogueParticipants(List<GameObject> conversationParticipants)
    {
        this.talkCommand.SetDialogueParticipants(conversationParticipants);
    }

    public bool IsRunning()
    {
        return this.isRunning;
    }

    public ICommand GetCommand()
    {
        return this;

    }
}