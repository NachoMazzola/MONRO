using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IMActionButton : MonoBehaviour {

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
	}
		
}
