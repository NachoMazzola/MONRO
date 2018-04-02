using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReactionExecuteCommand  : IPReaction
{

	public CommandType CommandType = CommandType.unknown;
	public bool ExecuteInmediately;

	[HideInInspector]
	public ICommandParamters currentParameters;

	[HideInInspector]
	public ICommand commandToExecute;


	override public bool Execute (Transform actionReceiver, Puzzle puzzle, PAction theAction)
	{
		this.commandToExecute = CommandFactory.CreateCommand (this.CommandType, null, false, this.currentParameters);
		CommandManager.getComponent ().QueueCommand (this.commandToExecute, this.ExecuteInmediately);

		return true;
	}

	void Update ()
	{
		if (this.commandToExecute != null && !this.commandToExecute.Finished ()) {
			this.commandToExecute.UpdateCommand ();
		}
	}


}

[CustomEditor (typeof(ReactionExecuteCommand))]
public class MyScriptEditor : Editor
{
	

	override public void OnInspectorGUI ()
	{
		ReactionExecuteCommand theScript = target as ReactionExecuteCommand;
		EditorGUILayout.BeginVertical ();

		theScript.CommandType = (CommandType)EditorGUILayout.EnumPopup ("Command", theScript.CommandType);
		theScript.ExecuteInmediately = GUILayout.Toggle (theScript.ExecuteInmediately, "Execute Inmediately");

		switch (theScript.CommandType) {
		case CommandType.AnimateCharacterCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new AnimateCharacterCommandParameters ();
			}

			AnimateCharacterCommandParameters p = (AnimateCharacterCommandParameters)theScript.currentParameters;
			p.Trigger = EditorGUILayout.TextField ("Animation Trigger", p.Trigger);
			p.TheAnimator = (Animator)EditorGUILayout.ObjectField ("Animator", p.TheAnimator, typeof(Animator), true);

			theScript.currentParameters = p;

			break;

		case CommandType.ChangeSpriteCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new ChangeSpriteCommandParamters ();
			}

			ChangeSpriteCommandParamters s = (ChangeSpriteCommandParamters)theScript.currentParameters;
			s.target = (GameObject)EditorGUILayout.ObjectField ("GameObject", s.target, typeof(GameObject), true);
			s.replacement = (Sprite)EditorGUILayout.ObjectField ("New Sprite", s.target, typeof(Sprite), true);
			s.onlyFlip = GUILayout.Toggle (s.onlyFlip, "Flip");

			theScript.currentParameters = s;

			break;
		

		case CommandType.LookAtCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new LookAtCommandParameters ();
			}

			LookAtCommandParameters l = (LookAtCommandParameters)theScript.currentParameters;
			l.lookable = (GameObject)EditorGUILayout.ObjectField ("Lookable", l.lookable, typeof(GameObject), true);
			l.whoLooks = (GameObject)EditorGUILayout.ObjectField ("Text Box Displayer", l.whoLooks, typeof(GameObject), true);

			theScript.currentParameters = l;

			break;

		case CommandType.MoveCameraCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new MoveCameraCommandParameters ();
			}

			MoveCameraCommandParameters mc = (MoveCameraCommandParameters)theScript.currentParameters;
			mc.moveToTarget = EditorGUILayout.Vector3Field ("To", mc.moveToTarget);
			mc.fromPosition = EditorGUILayout.Vector3Field ("From", mc.fromPosition);
			mc.movementSpeed = EditorGUILayout.FloatField ("Speed", mc.movementSpeed);

			theScript.currentParameters = mc;

			break;

		case CommandType.MoveGameObjectCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new MoveGameObjectCommandParameters ();
			}

			MoveGameObjectCommandParameters mgo = (MoveGameObjectCommandParameters)theScript.currentParameters;
			mgo.targetObject = (GameObject)EditorGUILayout.ObjectField ("Target", mgo.targetObject, typeof(GameObject), true);
			mgo.targetPosition = EditorGUILayout.Vector3Field ("To", mgo.targetPosition);
			mgo.movementSpeed = EditorGUILayout.FloatField ("Speed", mgo.movementSpeed);

			theScript.currentParameters = mgo;

			break;

		case CommandType.PutItemInInventoryCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new PutItemInInventoryCommandParameters ();
			}

			PutItemInInventoryCommandParameters pInv = (PutItemInInventoryCommandParameters)theScript.currentParameters;
			pInv.itemId = EditorGUILayout.TextField ("Item Id", pInv.itemId);
			pInv.itemTransform = (GameObject)EditorGUILayout.ObjectField ("Item Prefab", pInv.itemTransform, typeof(GameObject), true);

			theScript.currentParameters = pInv;

			break;

		case CommandType.RemoveItemFromInventoryCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new RemoveItemFromInventoryCommandParameters ();
			}

			RemoveItemFromInventoryCommandParameters rInv = (RemoveItemFromInventoryCommandParameters)theScript.currentParameters;
			rInv.itemCount = EditorGUILayout.IntField ("Count", rInv.itemCount);
			if (rInv.ItemsIdsToRemove == null) {
				rInv.ItemsIdsToRemove = new List<string>(rInv.itemCount);
			}
			else {
				if (rInv.itemCount != rInv.ItemsIdsToRemove.Count) {
					while(rInv.itemCount > rInv.ItemsIdsToRemove.Count) {
						rInv.ItemsIdsToRemove.Add("");
					}
					while(rInv.itemCount < rInv.ItemsIdsToRemove.Count) {
						string last = rInv.ItemsIdsToRemove[rInv.ItemsIdsToRemove.Count-1];
						rInv.ItemsIdsToRemove.Remove(last);
					}
				}
			}

			if (rInv.itemCount > 0) {
				for (int i = 0; i < rInv.ItemsIdsToRemove.Count; i++) {
					rInv.ItemsIdsToRemove[i] = EditorGUILayout.TextField("To Remove Id", rInv.ItemsIdsToRemove[i]);
				}	
			}

			theScript.currentParameters = rInv;

			break;

		case CommandType.TalkCommandType:
			if (theScript.currentParameters == null || theScript.currentParameters.GetCommandType () != theScript.CommandType) {
				theScript.currentParameters = new TalkCommandParameters ();
			}

			TalkCommandParameters t = (TalkCommandParameters)theScript.currentParameters;
			t.participantsCount = EditorGUILayout.IntField ("Participants", t.participantsCount);
			if (t.conversationParticipants == null) {
				t.conversationParticipants = new List<GameObject>(t.participantsCount);
			}
			else {
				if (t.participantsCount != t.conversationParticipants.Count) {
					while(t.participantsCount > t.conversationParticipants.Count) {
						t.conversationParticipants.Add(null);
					}
					while(t.participantsCount < t.conversationParticipants.Count) {
						GameObject last = t.conversationParticipants[t.conversationParticipants.Count-1];
						t.conversationParticipants.Remove(last);
					}
				}
			}

			if (t.participantsCount > 0) {
				for (int i = 0; i < t.conversationParticipants.Count; i++) {
					t.conversationParticipants[i] = (GameObject)EditorGUILayout.ObjectField ("Participant", t.conversationParticipants[i], typeof(GameObject), true);
				}	
			}


			theScript.currentParameters = t;

			break;
		}	
	
		EditorGUILayout.EndVertical ();
	}
}
