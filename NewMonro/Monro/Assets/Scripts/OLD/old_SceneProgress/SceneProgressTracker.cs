using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Yarn.Unity;

public class SceneProgressTracker : MonoBehaviour {

	public Dictionary<String, bool> sceneProgressFlags;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	//TODO: Agregar mas funciones que devuelvan validaciones Ej: Abrio X + Tiene X item + Hablo con X

	[YarnCommand("move")]
	public void caca(string pepon) {
		Debug.Log("ME CAGO EN LA MIERDA!");
	}
}
