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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other == null) {
			return;
		}

		UIInventory inv = GameObject.Find("UIInventory").GetComponent<UIInventory>();
		inv.OpenInventory();
	}
}
