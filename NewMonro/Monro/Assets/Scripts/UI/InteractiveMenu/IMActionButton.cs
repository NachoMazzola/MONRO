using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;


public class IMActionButton : MonoBehaviour {

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
	}
		
}
