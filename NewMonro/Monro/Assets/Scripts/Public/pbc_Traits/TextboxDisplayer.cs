using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class TextboxDisplayer : MonoBehaviour, TextBoxDelegate {

	public Transform Textbox;
	public Color TextColor = Color.black;
	public float TextSpeed = 0.0001f;
	public int TextSize = 30;
	public Font Font;



	[HideInInspector]
	public Transform instanciatedTextbox;
	[HideInInspector]
	public Lookable lookable;
	[HideInInspector]
	public bool hasFinishedCaptionDisplay;

	private SpriteRenderer spRenderer;


	// Use this for initialization
	void Start () {
		this.spRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		this.hasFinishedCaptionDisplay = false;
		if (this.spRenderer != null) {
			this.instanciatedTextbox = GameObject.Instantiate(this.Textbox, this.gameObject.transform.localPosition, Quaternion.identity);
			this.instanciatedTextbox.gameObject.SetActive(false);
			this.instanciatedTextbox.transform.SetParent(this.gameObject.transform, true);
			this.instanciatedTextbox.localPosition = this.gameObject.transform.position;
		}
		else {
			Debug.Log("TextboxContainer:: Cannot find SpriteRenderer in gameobject!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public bool ShowCaption() {
		if (this.lookable == null) {
			return false;
		}
		this.hasFinishedCaptionDisplay = false;
		StartCoroutine(AddActionOnFinishAfterCoroutine(this.ShowCaption(this.lookable.Caption)));

		return true;
	}

	public IEnumerator AddActionOnFinishAfterCoroutine(IEnumerator coroutineToWait) {
		yield return StartCoroutine(coroutineToWait);
		this.hasFinishedCaptionDisplay = true;
	}

	public IEnumerator ShowCaption(string caption, TextBox.DisappearMode removalMode = TextBox.DisappearMode.WaitInput) {
		this.instanciatedTextbox.gameObject.SetActive(true);

		TextBox pCaption = this.instanciatedTextbox.GetComponent<TextBox>();
		pCaption.tbDelegate = this;
		pCaption.TextColor = this.TextColor;
		pCaption.Font = this.Font;
		pCaption.TextSize = this.TextSize;
		return pCaption.ShowCaptionFromGameObject(caption, this.gameObject, true, removalMode);
	}

	public IEnumerator HideCaption(float afterSeconds) {
		Transform theCaption = this.instanciatedTextbox;
		TextBox pCaption = theCaption.GetComponent<TextBox>();

		return pCaption.RemoveCaptionAfterSeconds(afterSeconds, pCaption.gameObject);
	}

	public void startLine() {
		Talkable t = this.GetComponent<Talkable>();
		t.PlayAnimation();
	}

	public void finishedLine() {
		Talkable t = this.GetComponent<Talkable>();
		t.StopAnimation();
	}
}
