using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PReaction: MonoBehaviour {
	Transform c;
	abstract public void ExecuteReaction(IMActionButtonType action, Transform actionReceiver);
}
