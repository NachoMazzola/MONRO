using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;


public class IMActionButton : MonoBehaviour {

	public bool AddsTalkAction = false;
	public bool AddsUseAction = false;
	public bool AddsPickUpAction = false;
	public bool AddsLookAtAction = false;
		

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
		if (AddsTalkAction) {
			menu.gameObject.AddComponent<IMBTalkAction>();
			IMBTalkAction talkAction = menu.GetComponent<IMBTalkAction>();
			Transform buttoT = Resources.Load("/Prefabs/UIPrefabs/MenuPrefabs/IMBLookAt") as Transform;
			if (buttoT == null) {
				Debug.Log("PREFAB NOT FOUND BITCH");
			}
			talkAction.ButtonPrefab = Resources.Load("Prefabs/UIPrefabs/MenuPrefabs/IMBLookAt") as Transform;//  this.ButtonPrefab;
			menu.AddButton(talkAction);
		}
	}
}
