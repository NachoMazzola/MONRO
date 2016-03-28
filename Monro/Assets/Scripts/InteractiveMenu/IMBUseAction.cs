using UnityEngine;
using System.Collections;

public class IMBUseAction : MonoBehaviour, IIMButtonInterface {

	public Transform ButtonPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//interface methods

	public void ExecuteAction() {
		
	}

	public Transform getPrefab() {
		return ButtonPrefab;
	}
}
