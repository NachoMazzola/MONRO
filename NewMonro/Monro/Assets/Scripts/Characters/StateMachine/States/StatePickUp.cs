using UnityEngine;
using System.Collections;

public class StatePickUp : State {

	private Vector2 targetPosition;
	private Transform itemToPickUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void OnAwake() {
		base.OnAwake();
	}

	override public void OnStart() {
		base.OnStart();

	}

	override public void OnUpdate() {
		base.OnUpdate();
	}


	public override void StateStart() {
		base.StateStart();
		Debug.Log("STATE PICKUP START");
		this.itemToPickUp = (stateCharacterOwner as Player).itemToPickUp;

		animStateMachine.SetState(PlayerStateMachine.PlayerStates.PlayePickUp);
	}

	public override void StateUpdate() {
	}

	public override void StateEnd() {

		Debug.Log("STATE PICKUP END");

		GameObject invObj = GameObject.Find ("UI-Inventory");
		UIInventory theInv = invObj.GetComponent<UIInventory> ();
		PlayerInventory pInventory = invObj.GetComponent<PlayerInventory>();

		pInventory.AddItemById (itemToPickUp.GetComponent<InteractiveObject> ().Item.GetComponent<DBItemLoader> ().itemId);
		theInv.AddItemToInventory (itemToPickUp.GetComponent<InteractiveObject> ().Item);

		GameObject.Destroy(itemToPickUp.gameObject);
		itemToPickUp = null;

		base.StateEnd();
	}
}

