using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCaption : MonoBehaviour {

	public Transform PlayerCaptionPrefab;
	public float CaptionDurationUntilFade = 3.0f;
	public float CaptionFadeDuration = 1.5f;


	private SpriteRenderer playerSprite;
	private Transform instantiatedCaption;
	private bool showingCaption;
	private Vector2 originalScale;

	private IEnumerator hideUICoroutine;
	private IEnumerator removeCaptionCoroutine;

	void Awake() {
		playerSprite = GetComponent<SpriteRenderer>();

		originalScale = new Vector2(0.02f, 0.01f);
	}

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowCaption(string caption) {
		if (showingCaption == true) {
			Text showingText = SetCaptionText(caption);

			showingText.CrossFadeAlpha(1.0f, 0, false);
			showingText.CrossFadeColor(Color.white, 0, false, false);

			StopCoroutine(hideUICoroutine);
			StopCoroutine(removeCaptionCoroutine);


			StartCoroutine(HideTalkUI(instantiatedCaption.gameObject, CaptionDurationUntilFade, showingText));

			return;
		}


		showingCaption = true;

		InstantiateCaption();

		Text theText = SetCaptionText(caption);

		hideUICoroutine = HideTalkUI(instantiatedCaption.gameObject, CaptionDurationUntilFade, theText);
		StartCoroutine(hideUICoroutine);
	}

	Vector2 GetCaptionPosition() {
		Vector2 sprite_size = playerSprite.sprite.rect.size;
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

		return new Vector2(this.transform.position.x, this.transform.position.y + local_sprite_size.y/2);
	}

	void InstantiateCaption() {
		instantiatedCaption = Instantiate(PlayerCaptionPrefab, GetCaptionPosition(), Quaternion.identity) as Transform;
		instantiatedCaption.SetParent(this.transform);

		PreserveOriginalScale(this.transform.localScale.x);
	}

	Text SetCaptionText(string caption) {
		Text theText = instantiatedCaption.gameObject.GetComponentInChildren<Text>();
		theText.text = caption;

		return theText;
	}

	IEnumerator HideTalkUI (GameObject guiParentCanvas, float secondsToWait, Text textToFade)
	{
		yield return new WaitForSeconds (secondsToWait);

		textToFade.CrossFadeColor(Color.black, CaptionFadeDuration, false, false);
		textToFade.CrossFadeAlpha(0.0f, CaptionFadeDuration, false); 

		removeCaptionCoroutine = RemoveCaptionAfterSeconds(1.5f, guiParentCanvas);

		StartCoroutine(removeCaptionCoroutine);

	}

	IEnumerator RemoveCaptionAfterSeconds(float secondsToWait, GameObject guiParentCanvas) {
		yield return new WaitForSeconds (secondsToWait);
		guiParentCanvas.SetActive (false);

		Destroy(instantiatedCaption.gameObject);
		instantiatedCaption = null;

		showingCaption = false;
	}

	public bool CaptionIsBeingShown() {
		return showingCaption;
	}

	public void PreserveOriginalScale(float playerXScale) {
		if (instantiatedCaption != null) {
			if (playerXScale < 0) {
				Vector2 captionScale = originalScale;
				captionScale.x *= -1;
				instantiatedCaption.localScale = captionScale;
			}
			else {
				instantiatedCaption.localScale = originalScale;
			}


		}	
	}
}
