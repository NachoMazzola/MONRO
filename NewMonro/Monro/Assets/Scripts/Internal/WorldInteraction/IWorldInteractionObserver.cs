using UnityEngine;
using System.Collections;

public interface IWorldInteractionObserver {

	void IWOTapped(Vector2 tapPos, GameObject other);
	void IWOTapHold(Vector2 tapPos, GameObject other);
	void IWOInterruptInteractions();
	Transform IWOGetTransform();
}
