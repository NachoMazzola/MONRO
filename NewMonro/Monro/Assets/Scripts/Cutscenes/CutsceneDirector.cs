using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDirector: MonoBehaviour {

	private List<ICommand> commandQueue;
	private ICommand currentCutscene;
	private bool shouldAskForNextCommand = false;
	private int currentPlayingCommand = 0;

	// Use this for initialization
	void Start () {
		this.commandQueue = new List<ICommand>();
		Transform player = WorldObjectsHelper.getPlayerGO().transform;

		MoveCameraCommand moveCamera = new MoveCameraCommand(new Vector2(), ScreenHelpers.ScreenBounds().size, 0.6f);
		moveCamera.Prepare();
		moveCamera.moveToTarget = player.position;

		this.currentCutscene = moveCamera;


		AnimateCharacterCommand animateCommand = new AnimateCharacterCommand(player.GetComponent<Player>(), "isWakingUp");
		animateCommand.Prepare();

		ArrayList participants = new ArrayList();
		participants.Add(WorldObjectsHelper.getPlayerGO().transform);
		StartDialogueCommand dialogueCommand = new StartDialogueCommand(participants, "Monrjiall.Wakeup");
		dialogueCommand.Prepare();

		ChangeSpriteCommand changeSpriteCommand = new ChangeSpriteCommand(true, WorldObjectsHelper.getPlayerGO().GetComponent<SpriteRenderer>());


		StartDialogueCommand dialogueCommand2 = new StartDialogueCommand(participants, "Monrjiall.AfterMovement");
		dialogueCommand2.Prepare();

		this.QueueCutsceneCommand(moveCamera);
		this.QueueCutsceneCommand(animateCommand);
		this.QueueCutsceneCommand(dialogueCommand);
		this.QueueCutsceneCommand(changeSpriteCommand);
		this.QueueCutsceneCommand(dialogueCommand2);
	}

	public void QueueCutsceneCommand(ICommand command) {
		this.commandQueue.Add(command);
	}

	public ICommand GetNextCommand() {
		if (this.commandQueue.Count == 0) {
			return null;
		}
			
			
		ICommand theCommand = this.commandQueue[0];
		this.commandQueue.RemoveAt(0);
		//this.currentPlayingCommand++;
		return theCommand;
	}


	public void ExecuteCurrentCommand() {
		this.currentCutscene = this.GetNextCommand();
		if (this.currentCutscene != null) {
			this.currentCutscene.Prepare();
		}
	}


	// Update is called once per frame
	void Update () {
		if (this.shouldAskForNextCommand) {
			Debug.Log("COMMAND FINISHED: " + this.currentCutscene);

			this.currentCutscene = this.GetNextCommand();
			if (this.currentCutscene != null) {
				this.currentCutscene.WillStart();	
			}
			this.shouldAskForNextCommand = false;

			Debug.Log("COMMAND STARTS NEXT FRAME: " + this.currentCutscene);
		}

		if (this.currentCutscene != null && !this.shouldAskForNextCommand) {
			Debug.Log("UPDATING COMMAND: " + this.currentCutscene);
			this.currentCutscene.UpdateCommand();

			//we set this here just to let know that in the next frame, we 
			//should ask for the next command because the current one has finished.
			//We do it in the next frame so to let the current command finish in the
			//current frame
			this.shouldAskForNextCommand = this.currentCutscene.Finished();
		}
	}
}
