using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDirector: MonoBehaviour {

	private List<ICommand> commandQueue;
	private ICommand currentCutscene;
	private bool shouldAskForNextCommand = false;

	// Use this for initialization
	void Start () {
		this.commandQueue = new List<ICommand>();
		Transform player = GameObject.Find("PlayerViking").transform;

		MoveCameraCommand moveCamera = new MoveCameraCommand(new Vector2(), ScreenHelpers.ScreenBounds().size, 0.6f);
		moveCamera.Prepare();
		moveCamera.moveToTarget = player.position;

		this.currentCutscene = moveCamera;


		AnimateCharacterCommand animateCommand = new AnimateCharacterCommand(player.GetComponent<Player>(), "shouldWakeUp");


		this.QueueCutsceneCommand(moveCamera);
		this.QueueCutsceneCommand(animateCommand);
	}

	public void QueueCutsceneCommand(ICommand command) {
		this.commandQueue.Add(command);
	}

	public ICommand GetNextCommand() {
		if (this.commandQueue.Count == 0) {
			return null;
		}

		return this.commandQueue[0];
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
			this.currentCutscene = this.GetNextCommand();
			this.shouldAskForNextCommand = false;

			Debug.Log("COMMAND FINISHED");
		}

		if (this.currentCutscene != null) {
			this.currentCutscene.UpdateCommand();

			//we set this here just to let know that in the next frame, we 
			//should ask for the next command because the current one has finished.
			//We do it in the next frame so to let the current command finish in the
			//current frame
			this.shouldAskForNextCommand = this.currentCutscene.Finished();
		}
	}
}
