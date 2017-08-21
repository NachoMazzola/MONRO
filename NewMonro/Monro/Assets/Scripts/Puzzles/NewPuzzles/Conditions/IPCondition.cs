using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPCondition {
	bool ConditionApplies(Puzzle inPuzzle);
}
