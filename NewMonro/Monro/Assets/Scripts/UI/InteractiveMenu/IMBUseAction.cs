using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IMBUseAction : IMActionButton {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void ExecuteAction() {
		Debug.Log("USE BUTTON");
		base.ExecuteAction();
	}


}
