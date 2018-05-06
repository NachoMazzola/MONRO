using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class StateTalk : State
{

	private Player playerComp;
	private Character theNPC;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		//animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerIdle);
	}

	override public void OnAwake ()
	{
		base.OnAwake ();
	}

	override public void OnStart ()
	{
		base.OnStart ();
	}

	override public void OnUpdate ()
	{
		base.OnUpdate ();
	}


	public override void StateStart ()
	{
		base.StateStart ();

		animStateMachine.SetState (PlayerStateMachine.PlayerStates.PlayerTalk);

		playerComp = stateCharacterOwner as Player;
		theNPC = playerComp.stateTransitionData.otherCharacter;
		WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueRunner>().StartDialogue((theNPC as NPC).ConversationNode);
	}

	public override void StateUpdate ()
	{
	}

	public override void StateEnd ()
	{
		base.StateEnd ();
	}
}
