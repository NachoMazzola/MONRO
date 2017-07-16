using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PReaction: MonoBehaviour {

	public bool Executed;
	public bool IncrementSteps;
	public Transform ActionReceiver;
	public IMActionButtonType ActionTrigger;


	[HideInInspector]
	public Puzzle parent;

	abstract public void ExecuteReaction(IMActionButtonType action, Transform actionReceiver);
	abstract public void ActionFinished();
}
