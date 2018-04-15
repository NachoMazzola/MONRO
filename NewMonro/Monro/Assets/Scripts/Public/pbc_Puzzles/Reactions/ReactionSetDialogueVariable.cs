using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ReactionSetDialogueVariable : IPReaction {

	public string DialogueVariable;
	public bool setAsTrue = true;

	override public bool Execute(Transform actionReceiver, Puzzle puzzle, PAction theAction) {
		DialogueRunner dialogRunner = FindObjectOfType<DialogueRunner> ();
		ExampleVariableStorage dialogueStorage = dialogRunner.variableStorage as ExampleVariableStorage;

		string propeVariable = "$"+DialogueVariable;

		dialogueStorage.SetValue(propeVariable, new Yarn.Value(setAsTrue));

		return true;
	}
}
