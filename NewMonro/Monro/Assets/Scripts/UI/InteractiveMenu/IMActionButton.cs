using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;

public enum IMActionButtonType {
	Pickup,
	LookAt,
	Talk,
	Use,
	None
}


public class IMActionButton : MonoBehaviour {
	
	public IMActionButtonType AddsActionOnExecution = IMActionButtonType.None;

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

		DialogueRunner dialogRunner = FindObjectOfType<DialogueRunner> ();
		ExampleVariableStorage dialogueStorage = dialogRunner.variableStorage as ExampleVariableStorage;

		string propeVariable = "$"+dialogueReferVariable;

		dialogueStorage.SetValue(propeVariable, new Yarn.Value(true));

		AddActionOnfinish();
	}

	public virtual void AddActionOnfinish() {

		IMActionButton actionBtn = null;
		Transform actionBtnTransform = null;
		switch(AddsActionOnExecution) {
			case IMActionButtonType.Talk:

			IMBTalkAction talkComponent = menu.gameObject.GetComponent<IMBTalkAction>();
			if (talkComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBTalkAction>();	
				actionBtnTransform = (Resources.Load("Prefabs/UIPrefabs/MenuPrefabs/IMBLookAt") as GameObject).transform;
			}

			break;

			case IMActionButtonType.LookAt:

			IMActionButton lookAtComponent = menu.gameObject.GetComponent<IMBLookAtAction>();
			if (lookAtComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBLookAtAction>();
				actionBtnTransform = (Resources.Load("Prefabs/UIPrefabs/MenuPrefabs/IMBLookAt") as GameObject).transform;
			}
			break;

			case IMActionButtonType.Pickup:
			IMBPickUpAction pUpComponent = menu.gameObject.GetComponent<IMBPickUpAction>();
			if (pUpComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBPickUpAction>();
				actionBtnTransform = (Resources.Load("Prefabs/UIPrefabs/MenuPrefabs/IMBLookAt") as GameObject).transform;
			}

			break;

			case IMActionButtonType.Use:
			IMBUseAction useComponent = menu.gameObject.GetComponent<IMBUseAction>();
			if (useComponent == null) {
				actionBtn = menu.gameObject.AddComponent<IMBUseAction>();
				actionBtnTransform = (Resources.Load("Prefabs/UIPrefabs/MenuPrefabs/IMBLookAt") as GameObject).transform;
			}

			break;
		}
			
		if (actionBtn != null && actionBtnTransform != null) {
			actionBtn.ButtonPrefab = actionBtnTransform;
			menu.AddButton(actionBtn);
		}
		else {
			Debug.Log("PREFAB NOT FOUND BITCH");
		}
	}
}
