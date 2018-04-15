using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles commands in a queue
*/
public class CommandManager : MonoBehaviour {

	private List<ICommand> commandQueue;

	private ICommand currentCommand;
	private bool shouldAskForNextCommand = false;
	private int currentPlayingCommand = 0;

	/**
	 * This property is only when commands are executed with a specific target, like
	 * for example commands that are triggered with the verbs panel
	*/
	[HideInInspector]
	public GameObject target;

	static public CommandManager getComponent ()
	{
		GameObject controller = WorldObjectsHelper.GetCommandManagerGO();
		if (controller) {
			return controller.GetComponent<CommandManager> ();
		}
		return null;
	}


	// Use this for initialization
	void Start () {
		this.commandQueue = new List<ICommand>();
	}

	public void QueueCommand(ICommand command, bool executeInmediately = false) {
		this.commandQueue.Add(command);
		command.Prepare();

		if (executeInmediately) {
			this.ExecuteCurrentCommand();	
		}
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
		this.currentCommand = this.GetNextCommand();
		if (this.currentCommand != null) {
			this.currentCommand.WillStart();
		}
	}


	// Update is called once per frame
	void Update () {
		if (this.shouldAskForNextCommand) {
			Debug.Log("COMMAND FINISHED: " + this.currentCommand);

			this.SetPuzzleAction(this.target);

			this.currentCommand = this.GetNextCommand();
			if (this.currentCommand != null) {
				this.currentCommand.WillStart();	
			}
			this.shouldAskForNextCommand = false;

			Debug.Log("COMMAND STARTS NEXT FRAME: " + this.currentCommand);
		}

		if (this.currentCommand != null && !this.shouldAskForNextCommand) {
			Debug.Log("UPDATING COMMAND: " + this.currentCommand);
			this.currentCommand.UpdateCommand();

			//we set this here just to let know that in the next frame, we 
			//should ask for the next command because the current one has finished.
			//We do it in the next frame so to let the current command finish in the
			//current frame
			this.shouldAskForNextCommand = this.currentCommand.Finished();
		}
	}

	private void SetPuzzleAction(GameObject target) {
		if (target == null) {
			return;
		}

		if (this.currentCommand.GetCommandType() == CommandType.LookAtCommandType) {
			PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.LookAt, target.transform);
		}
		else if (this.currentCommand.GetCommandType() == CommandType.TalkCommandType) {
			PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.Talk, target.transform);
		}
		else if (this.currentCommand.GetCommandType() == CommandType.PutItemInInventoryCommandType) {
			PuzzleManager.UpdatePuzzleWithAction(PuzzleActionType.PickUp, target.transform);
		}
		//TODO: USE COMMAND!

	}
}
