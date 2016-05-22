using UnityEngine;
using System.Collections;

public class OpenInventoryButton : MonoBehaviour {

	private Animator buttonAnimator;

	void Awake() {
		buttonAnimator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAddingItemToInventoryAnim() {
		buttonAnimator.SetBool("itemAdded", true);
	}

	public void AnimEventAddingItemAnimFinished() {
		buttonAnimator.SetBool("itemAdded", false);
	}
}
