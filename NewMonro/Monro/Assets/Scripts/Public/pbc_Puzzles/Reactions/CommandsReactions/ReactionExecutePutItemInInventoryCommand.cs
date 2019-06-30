using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReactionExecutePutItemInInventoryCommand : ExecuteCommandReaction {
	public PutItemInInventoryCommandParameters parameters;

	public bool ById;

	void Awake() {
		if (parameters.itemId.Length > 0) {
			parameters.itemTransform = null;
		}
		else if (parameters.itemTransform != null) {
			parameters.itemId = null;
		}

        this.theCommand = new PutItemInInventoryCommand(this.parameters);
	}
}

[CustomEditor(typeof(ReactionExecutePutItemInInventoryCommand))]
public class ReactionExecutePutItemInInventoryCommandEditorScript: Editor {

	public override void OnInspectorGUI() {
		var myScript = target as ReactionExecutePutItemInInventoryCommand;
		EditorGUILayout.BeginVertical();

		myScript.ById = GUILayout.Toggle(myScript.ById, "ById");
		if (myScript.ById) {
			myScript.parameters.itemTransform = null;
			myScript.parameters.itemId = EditorGUILayout.TextField("Item Id", myScript.parameters.itemId);
		}
		else {
			myScript.parameters.itemId = null;
			myScript.parameters.itemTransform = (GameObject)EditorGUILayout.ObjectField("Item Prefab", myScript.parameters.itemTransform, typeof(GameObject), true);
		}

		EditorGUILayout.EndVertical();
	}
}
