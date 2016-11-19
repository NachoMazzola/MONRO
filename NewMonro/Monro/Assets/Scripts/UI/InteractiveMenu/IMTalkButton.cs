using UnityEngine;
using System.Collections;
using Yarn.Unity;


public class IMTalkButton : IMActionButton {

	// Use this for initialization
	void Awake() {
		OnAwake();
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
	}

	//interface methods

	override public void ExecuteAction() {
		Player playerComp = player.GetComponent<Player>();

		InteractiveObject theObj = interactiveObject.GetComponent<InteractiveObject>();
		theObj.allowInteraction = false;
		InteractiveMenu theIntMenu = theObj.GetComponent<InteractiveMenu>();
		if (theIntMenu != null) {
			theIntMenu.ToggleMenu();
		}

		DialogueRunner dialogRunner = FindObjectOfType<DialogueRunner> ();
		DialogueUI theConversationUI = dialogRunner.dialogueUI as DialogueUI;

		theConversationUI.dialogRunner.AddParticipant(playerComp.transform);
		theConversationUI.dialogRunner.AddParticipant(theObj.transform);

		playerComp.stateTransitionData = new StateTransitionData(theObj.GetComponent<NPC>());

		playerComp.ChangeToState(PlayerStateMachine.PlayerStates.PlayerWalk);
		(playerComp.currentState as StateWalk).SetupState(GetCorrectTalkingPosition(theObj.transform), true, PlayerStateMachine.PlayerStates.PlayerTalk);
	}

	public Vector2 GetCorrectTalkingPosition(Transform moveToObj) {
		Player playerComp = player.GetComponent<Player>();
		CircleCollider2D theCollider = moveToObj.GetComponent<CircleCollider2D>();
		int dirChange = 1; 
		Character talkTo = moveToObj.gameObject.GetComponent<Character>();
		if (talkTo.currentFacingDirection == Character.MovingDirection.MovingLeft) {
			dirChange = -1;
		}

		float newX = moveToObj.position.x + (theCollider.radius+0.5f)*dirChange; 

		return new Vector2(newX, moveToObj.position.y);
	}
}
