using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour, IWorldInteractionObserver {

	public string ConversationName;
	public Color CharacterTalkColor;

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	virtual public void OnAwake() {
		
	}

	virtual public void OnStart() {
		WorldInteractionController.getComponent().AddObserver(this);
	}

	virtual public void OnUpdate() {
	}

	virtual public Transform GetConversationCaptionCanvas() {
		Transform theCaption = this.transform.FindChild("TextCaption");

		return theCaption;
	}

	virtual public void ResetState() {
		
	}

	virtual public void IWOTapped(Vector2 tapPos, GameObject other) {
		
	}

	virtual public void IWOTapHold(Vector2 tapPos, GameObject other) {
		
	}
}
