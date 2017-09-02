using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDialogueOptionSelected : MonoBehaviour, IPCondition {

	public List<string> dialogueOptions;

	public bool ConditionApplies(Puzzle inPuzzle) {
		ExampleVariableStorage dialogueStorage = GameObject.Find("Dialogue").GetComponent<ExampleVariableStorage>();
		int dialogueOptionsFound = 0;
		if (dialogueStorage != null) {
			foreach (string option in dialogueOptions) {
				if (dialogueStorage.GetValue(option) != null) {
					dialogueOptionsFound++;
				}
			}
		}
		return dialogueOptionsFound > 0;
	}
}

