using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCaption : MonoBehaviour {

	public Transform PlayerCaptionPrefab;
	public float CaptionDurationUntilFade = 3.0f;
	public float CaptionFadeDuration = 1.5f;


	private SpriteRenderer playerSprite;

	// Use this for initialization
	void Start () {
		playerSprite = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowCaption(string caption) {

		Vector2 sprite_size = playerSprite.sprite.rect.size;
		Vector2 local_sprite_size = sprite_size / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

		Vector2 captionPos = new Vector2(this.transform.position.x, this.transform.position.y + local_sprite_size.y/2);


		Transform captionTransf = Instantiate(PlayerCaptionPrefab, captionPos, Quaternion.identity) as Transform;
		captionTransf.SetParent(this.transform);

		Text theText = captionTransf.gameObject.GetComponentInChildren<Text>();
		theText.text = caption;

		StartCoroutine(hideTalkUI(captionTransf.gameObject, CaptionDurationUntilFade, theText));
	}

	IEnumerator hideTalkUI (GameObject guiParentCanvas, float secondsToWait, Text textToFade)
	{
		yield return new WaitForSeconds (secondsToWait);

		textToFade.CrossFadeColor(Color.black, CaptionFadeDuration, false, false);
		textToFade.CrossFadeAlpha(0.0f, CaptionFadeDuration, false); 

		StartCoroutine(RemoveCaptionAfterSeconds(1.5f, guiParentCanvas));

	}

	IEnumerator RemoveCaptionAfterSeconds(float secondsToWait, GameObject guiParentCanvas) {
		yield return new WaitForSeconds (secondsToWait);
		guiParentCanvas.SetActive (false);
	}
}
