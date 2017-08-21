using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPReaction: MonoBehaviour {

	protected List<IPCondition> conditions;

	public abstract bool Execute(Transform actionReceiver, Puzzle puzzle, PAction theAction);

	void Start() {
		conditions = new List<IPCondition>();
		IPCondition[] comps = this.transform.GetComponentsInChildren<IPCondition>();
		foreach (IPCondition c in comps) {
			conditions.Add(c);
		}
	}

	public bool AllConditionsApply(Puzzle inPuzzle) {
		int acceptedConditions = 0;
		foreach (IPCondition c in conditions) {
			if (c.ConditionApplies(inPuzzle)) {
				acceptedConditions++;
			}
		}

		return acceptedConditions == conditions.Count;
	}
}
