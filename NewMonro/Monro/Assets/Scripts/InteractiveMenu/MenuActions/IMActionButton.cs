using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;
using System.Collections;

public enum IMActionButtonType {
	Pickup,
	LookAt,
	Talk,
	Use,
	None
}


public class IMActionButton : MonoBehaviour {
	
	[HideInInspector]
	public IMActionButtonType buttonType;

	[HideInInspector]
	public InteractiveMenu menu;

	public string dialogueReferVariable; 
	public Transform ButtonPrefab;

	protected GameObject player;
	protected GameObject interactiveObject;

	void Awake() {
		OnAwake();
	}

	// Use this for initialization
	void Start () {
		OnStart();
	}
	
	// Update is called once per frame
	void Update () {
		OnUpdate();
	}

	public virtual void OnAwake() {
		player = GameObject.Find("PlayerViking");
		interactiveObject = this.transform.gameObject;
	}

	public virtual void OnStart() {
		
	}

	public virtual void OnUpdate() {
		
	}
		
	public virtual void ExecuteAction() {
		Debug.Log("ACTION BUTTON");

		PuzzleActionType puzzleType = PuzzleManager.TransformActionButtonTypeToPuzzleActionType(this.buttonType);
		PuzzleManager.UpdatePuzzleWithAction(puzzleType, this.transform);

		ExecutePuzzleAction();
	}

	public IEnumerator AddActionOnFinishAfterCoroutine(IEnumerator coroutineToWait) {
		yield return StartCoroutine(coroutineToWait);
	}
		

	void ExecutePuzzleAction() {
		InteractivePuzzleAction intPA = this.gameObject.GetComponent<InteractivePuzzleAction>();
		if (intPA != null) {
			intPA.Execute();
		}
	}
}
