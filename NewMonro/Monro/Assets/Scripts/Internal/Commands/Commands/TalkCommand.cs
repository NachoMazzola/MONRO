using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

[System.Serializable]
public struct TalkCommandParameters : ICommandParamters
{
    //public List<GameObject> conversationParticipants;
    public string startingNode;

    public int participantsCount;

    public CommandType GetCommandType()
    {
        return CommandType.TalkCommandType;
    }
}

public interface TalkableCommand
{
    void SetStartingNode(string startingNode);
    void SetDialogueParticipants(List<GameObject> conversationParticipants);
    bool IsRunning();
    ICommand GetCommand();
}


public class TalkCommand : ICommand, TalkableCommand
{

    public string startingNode;
    public List<GameObject> conversationParticipants;

    private DialogueRunner dialogueRunner;
    private DialogueUI dialogueUI;

    public TalkCommand()
    {
        this.conversationParticipants = new List<GameObject>();
    }

    public TalkCommand(ICommandParamters parameters)
    {
        TalkCommandParameters t = (TalkCommandParameters)parameters;
        this.conversationParticipants = new List<GameObject>();
        this.startingNode = t.startingNode;
    }

    public TalkCommand(string startingNode)
    {
        this.startingNode = startingNode;
        this.conversationParticipants = new List<GameObject>();
    }

    public TalkCommand(string startingnode, List<GameObject> participants)
    {
        this.startingNode = startingnode;
        this.conversationParticipants = participants;
    }

    public override void Prepare()
    {
        this.isInterrutable = false;
        dialogueRunner = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>();

        foreach (GameObject g in this.conversationParticipants)
        {
            Talkable t = g.GetComponent<Talkable>();
            if (t != null)
            {
                this.dialogueRunner.AddParticipant(t);
            }
            else
            {
                Debug.Log("WARNING ::: " + g + " IS NOT TALKABLE!!!");
            }
        }




        //Talkable[] participants = GameObject.FindObjectsOfType<Talkable>();
        //if (participants != null && participants.Length > 0) {
        //	foreach (Talkable t in participants) {
        //		this.dialogueRunner.AddParticipant(t);	
        //	}	
        //}
    }

    public override void WillStart()
    {
        this.dialogueRunner.StartDialogue(this.startingNode);
        this.isRunning = true;
    }

    public override void UpdateCommand()
    {
        if (this.dialogueRunner != null && this.isRunning)
        {
            this.isRunning = this.dialogueRunner.isDialogueRunning;
        }

    }

    public override bool Finished()
    {
        if (this.isRunning)
        {
            foreach (GameObject t in this.conversationParticipants)
            {
                Talkable talkable = t.GetComponent<Talkable>();
                if (talkable != null)
                {
                    talkable.StopAnimation();
                }
            }
        }

        return !this.isRunning;
    }

    public override CommandType GetCommandType()
    {
        return CommandType.TalkCommandType;
    }

    public void SetStartingNode(string startingNode)
    {
        this.startingNode = startingNode;
    }

    public void SetDialogueParticipants(List<GameObject> conversationParticipants)
    {
        this.conversationParticipants = conversationParticipants;
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
