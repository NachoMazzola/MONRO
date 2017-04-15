using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAction : MonoBehaviour {

	public Transform puzzleGameObject;
	public PGOState transitionGPOToState;  

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Execute() {
		
	}

	public virtual void UpdateGPOState() {
		PuzzleGameObject pgo = puzzleGameObject.GetComponent<PuzzleGameObject>();
		PGOState prevState = pgo.puzzleState;
		pgo.puzzleState = transitionGPOToState;

		PuzzleNotificationManager.getComponent().NotifyPGOChangedState(transitionGPOToState, prevState, pgo.PGOId);
	}
}
