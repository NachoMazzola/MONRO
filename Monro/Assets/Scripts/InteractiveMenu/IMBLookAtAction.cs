using UnityEngine;
using System.Collections;

public class IMBLookAtAction : IMActionButton {

	public string caption;


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
		PlayerCaption thePlayerCap = player.GetComponent<PlayerCaption>();
		thePlayerCap.ShowCaption(caption);
	}
}
