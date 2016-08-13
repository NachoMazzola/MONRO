using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	public string ConversationName;
	public Color CharacterTalkColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	virtual public Transform GetConversationCaptionCanvas() {
		Transform theCaption = this.transform.FindChild("TextCaption");

		return theCaption;
	}

	virtual public void ResetState() {
		
	}
}
