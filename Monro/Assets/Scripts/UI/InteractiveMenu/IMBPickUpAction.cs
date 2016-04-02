using UnityEngine;
using System.Collections;

public class IMBPickUpAction : IMActionButton {

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


	override public void ExecuteAction() {
		Player playerComp = player.GetComponent<Player>();
		playerComp.MoveToAndPickUp(this.transform.position, interactiveObject.transform);


	}



}
