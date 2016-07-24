using UnityEngine;
using System.Collections;
using Yarn.Unity;


public class IMTalkButton : IMActionButton {


	// Use this for initialization
	void Start () {
		OnStart();
	}

	// Update is called once per frame
	void Update () {
		OnUpdate();
	}

	public override void OnStart() {
		base.OnStart ();

	}

	public override void OnUpdate() {
		base.OnUpdate();
	}

	//interface methods

	override public void ExecuteAction() {
		Player playerComp = player.GetComponent<Player>();
		InteractiveObject theObj = interactiveObject.GetComponent<InteractiveObject>();
		playerComp.MoveToKeepDistance(theObj.transform);


		//FindObjectOfType<DialogueRunner> ().StartDialogue ("Intro");
	}
}
