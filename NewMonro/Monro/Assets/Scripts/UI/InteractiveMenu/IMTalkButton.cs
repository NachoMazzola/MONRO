using UnityEngine;
using System.Collections;
using Yarn.Unity;


public class IMTalkButton : IMActionButton {

	private bool startDialogue;
	private Player playerComp;
	private NPC theNPC;

	// Use this for initialization
	void OnAwake() {
		playerComp = player.GetComponent<Player>();
		startDialogue = false;
	}

	void Start () {
		OnStart();


	}

	// Update is called once per frame
	void Update () {
		OnUpdate();
	}

	public override void OnStart() {
		base.OnStart ();

	}

	public override void OnUpdate() {
		base.OnUpdate();
		if (playerComp) {
			if (playerComp.animStateMachine.GetCurrentState() == PlayerStateMachine.PlayerStates.PlayerTalk) {
				if (startDialogue) {
					startDialogue = false;

					FindObjectOfType<DialogueRunner> ().StartDialogue (theNPC.ConversationNode);
				}
			}	
		}

	}

	//interface methods

	override public void ExecuteAction() {
		
		InteractiveObject theObj = interactiveObject.GetComponent<InteractiveObject>();
		theNPC = theObj.GetComponent<NPC>();
		playerComp.GoTalkToNPC(theObj.transform);

		DialogueRunner dialogRunner = FindObjectOfType<DialogueRunner> ();
		DialogueUI theConversationUI = dialogRunner.dialogueUI as DialogueUI;

		theConversationUI.AddParticipant(player.transform);
		theConversationUI.AddParticipant(theObj.transform);

		startDialogue = true;
	}
}
