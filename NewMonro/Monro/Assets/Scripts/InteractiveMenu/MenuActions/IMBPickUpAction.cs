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

	public override void OnAwake() {
		base.OnAwake();
		buttonType = IMActionButtonType.Pickup;
	}

	public override void OnStart() {
		base.OnStart ();

	}

	public override void OnUpdate() {
		base.OnUpdate();
	}


	override public void ExecuteAction() {
		base.ExecuteAction();

		Player playerComp = player.GetComponent<Player>();

		playerComp.itemToPickUp = interactiveObject.transform;
		playerComp.ChangeToState(PlayerStateMachine.PlayerStates.PlayerWalk);
		(playerComp.currentState as StateWalk).SetupState(this.transform, true, PlayerStateMachine.PlayerStates.PlayePickUp);


		//playerComp.MoveToAndPickUp(this.transform.position, interactiveObject.transform);
	}



}
