﻿using UnityEngine;
using System.Collections;

public class NPC : Character {

	public string ConversationNode;

	void Awake() {
		characterType = CharacterType.NPC;
	}

//	//EL PROBLEMA ESTA ACA!! AL PARECER, AL LLAMAR AL ONAWAKE DE CHARACTER ALGO SE CARAJEA
//	//Y NO MUESTRA EL MENU
//	void Awake (){
//		OnAwake();
//	}

	// Use this for initialization
	void Start () {
		OnStart();
	}

	// Update is called once per frame
	void Update () {
		OnUpdate();
	}

	override public void OnAwake() {
		base.OnAwake();
		characterType = CharacterType.NPC;
	}

	override public void OnStart() {
		base.OnStart();

	}

	override public void OnUpdate() {
		base.OnUpdate();
	}

	override public void ResetState() {
		InteractiveObject intObj = this.GetComponent<InteractiveObject>();
		intObj.allowInteraction = true;

	}

}
