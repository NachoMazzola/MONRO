using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReactionExecuteCommand  : IPReaction {

	public CommandType CommandType = CommandType.unknown;
	public bool flag;

	[HideInInspector]
	public ICommand commandToExecute;

	/// <summary>
	/// AnimateCharacterCommand params
	/// </summary>
	public string Trigger;
	public Animator TheAnimator;


	/// <summary>
	/// LookAtCommand params
	/// </summary>
	public GameObject Lookable;
	public GameObject WhoLooks;



	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		switch(this.CommandType) {
		case CommandType.AnimateCharacterCommandType:
			AnimateCharacterCommand animCharCommand = (AnimateCharacterCommand)CommandFactory.CreateCommand(CommandType.AnimateCharacterCommandType, null);
			animCharCommand.Trigger = this.Trigger;
			animCharCommand.TheAnimator = this.TheAnimator;
			animCharCommand.Prepare();
			animCharCommand.WillStart();

			break;

		case CommandType.LookAtCommandType:
			break;
			
		}





		return true;
	}

	void Update() {
		if (this.commandToExecute != null && !this.commandToExecute.Finished()) {
			this.commandToExecute.UpdateCommand();
		}
	}
}

[CustomEditor(typeof(ReactionExecuteCommand))]
public class MyScriptEditor : Editor
{
	override public void OnInspectorGUI() {
		ReactionExecuteCommand theScript = target as ReactionExecuteCommand;
		EditorGUILayout.BeginVertical();

		theScript.CommandType = (CommandType)EditorGUILayout.EnumPopup("Command", theScript.CommandType);

		switch(theScript.CommandType) {
		case CommandType.AnimateCharacterCommandType:
			theScript.Trigger = EditorGUILayout.TextField("Animation Trigger", theScript.Trigger);
			theScript.TheAnimator = (Animator)EditorGUILayout.ObjectField("Animator", theScript.TheAnimator, typeof(Animator), true);
			break;


		case CommandType.LookAtCommandType:
			theScript.WhoLooks = (GameObject)EditorGUILayout.ObjectField("Who Looks", theScript.WhoLooks, typeof(GameObject), true);



			break;

		case CommandType.ChangeSpriteCommandType:
			break;
		}

		EditorGUILayout.EndVertical();
	}
}
