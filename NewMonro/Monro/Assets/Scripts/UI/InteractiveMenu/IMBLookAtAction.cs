using UnityEngine;
using System.Collections;

public class IMBLookAtAction : IMActionButton {



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
		base.ExecuteAction();

		Player thePlayer = player.GetComponent<Player>();
		InteractiveObject theObj = interactiveObject.GetComponent<InteractiveObject>();
		thePlayer.ShowCaption(theObj.Caption);

	}
}
