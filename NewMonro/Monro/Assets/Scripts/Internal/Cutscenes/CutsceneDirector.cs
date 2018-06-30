using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDirector: MonoBehaviour {

	private List<ICommand> commandQueue;
	private ICommand currentCutscene;
	private bool shouldAskForNextCommand = false;
	private int currentPlayingCommand = 0;

	void Awake() {
		ExecuteCommandReaction[] reactions = this.GetComponents<ExecuteCommandReaction>();
		foreach (ExecuteCommandReaction r in reactions) {
			r.Execute(null, null, null);
		}

		CommandManager.getComponent().ExecuteCurrentCommand();
		return;

		Debug.Log("SCREEN HEIGht: " + Screen.height);

		this.commandQueue = new List<ICommand>();
		Transform player = WorldObjectsHelper.getPlayerGO().transform;

		MoveCameraCommand moveCamera = new MoveCameraCommand(new Vector2(), ScreenHelpers.ScreenBounds().size, 0.6f);
		moveCamera.moveToTarget = player.position;


		AnimateCharacterCommand animateCommand = new AnimateCharacterCommand(player, "isWakingUp");


		List<GameObject> participants = new List<GameObject>();
		participants.Add(WorldObjectsHelper.getPlayerGO());
		TalkCommand dialogueCommand = new TalkCommand();
		dialogueCommand.conversationParticipants = participants;
		dialogueCommand.startingNode = "Monrjiall.Wakeup";


		ChangeSpriteCommand changeSpriteCommand = new ChangeSpriteCommand(true, WorldObjectsHelper.getPlayerGO());


		TalkCommand dialogueCommand2 = new TalkCommand();
		dialogueCommand2.conversationParticipants = participants;
		dialogueCommand2.startingNode = "Monrjiall.AfterMovement";
		//	dialogueCommand2.Prepare();


		//		GameObject testNPC = WorldObjectsHelper.getInteractiveObject("VK");
		//		Transform targetR = WorldObjectsHelper.getPlayerGO().transform;
		//		Vector3 targetP = targetR.position;//Camera.main.WorldToScreenPoint(targetR.position);
		//
		//		Vector2 testTargetPos = new Vector2(targetP.x - 10, WorldObjectsHelper.getPlayerGO().transform.position.y);
		//		MoveGameObjectCommand moveGoCommand = new MoveGameObjectCommand(WorldObjectsHelper.getPlayerGO(), testTargetPos, 4);
		//		moveGoCommand.Prepare();
		//

		//		this.QueueCutsceneCommand(moveCamera);
		//		this.QueueCutsceneCommand(animateCommand);
		//		this.QueueCutsceneCommand(dialogueCommand);
		//		this.QueueCutsceneCommand(changeSpriteCommand);
		//		this.QueueCutsceneCommand(dialogueCommand2);
		//this.QueueCutsceneCommand(moveGoCommand);

		CommandManager.getComponent().QueueCommand(moveCamera);
		CommandManager.getComponent().QueueCommand(animateCommand);
		CommandManager.getComponent().QueueCommand(dialogueCommand);
		CommandManager.getComponent().QueueCommand(changeSpriteCommand);
		CommandManager.getComponent().QueueCommand(dialogueCommand2);
		//CommandManager.getComponent().QueueCommand(movego);

		//CommandManager.getComponent().ExecuteCurrentCommand();

		CommandManager.getComponent().ExecuteCurrentCommand();
	}

	// Use this for initialization
	void Start () {
		
	}
}
