using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPReaction {
	bool Execute(Transform actionReceiver, Puzzle puzzle, PAction theAction);
}
