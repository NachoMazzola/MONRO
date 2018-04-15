using UnityEngine;
using System.Collections;

public interface IState {

	void StateStart();
	void StateUpdate();
	void StateEnd();

	Character GetCharacterOwner();
	void SetCharacterOwner(Character owner);
}
