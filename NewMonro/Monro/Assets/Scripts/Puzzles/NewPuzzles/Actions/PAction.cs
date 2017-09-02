using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleActionType {
	Talk,
	PickUp,
	LookAt,
	Use,
	CombineItems,
	DropItemOver,
	None
}


public abstract class PAction: MonoBehaviour {

	public bool Executed = false;
	public bool IncrementSteps = true;

	[HideInInspector]
	public Puzzle parent;
	[HideInInspector]
	public IPReaction theReaction;

	protected List<IPReaction> reactions;

	void Start() {
		reactions = new List<IPReaction>();
		IPReaction[] comps = this.transform.GetComponentsInChildren<IPReaction>();
		foreach (IPReaction p in comps) {
			reactions.Add(p);
		}
	}

	public abstract void ExecuteAction(PuzzleActionType action, Transform actionReceiver = null, Dictionary<string, object> extraData = null);

	public virtual void SetPuzzleParent(Puzzle pParent) {
		parent = pParent;
	}

	public void ActionFinished() {
		if (IncrementSteps) {
			parent.IncrementPuzzleStep();	
		}	

		Executed = true;
	}

	protected bool ExecuteAllReactions(Transform actionReceiver = null) {
		int actionExecutedCount = 0;
		foreach (IPReaction r in reactions) {
			if (r.AllConditionsApply(this.parent)) {
				if (r.Execute(actionReceiver, parent, this)) {
					actionExecutedCount++;
				}	
			}
		}
		return actionExecutedCount == reactions.Count;
	} 

	protected Transform GetTransformFromId(string id) {
		InteractiveObject[] intsObjs = GameObject.FindObjectsOfType<InteractiveObject>();
		foreach (InteractiveObject iObj in intsObjs) {
			if (iObj.Id == id) {
				return iObj.transform;
			}
		}

		return null;
	}
}
