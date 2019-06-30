using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Adds click/tap interaction to a GameObject
*/
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(BoxCollider2DSizeFitter))]
public class Tappable : Trait {

	[HideInInspector]
	public ArrayList colliderList;

    private float doubleClickTimeLimit = 0.25f;
    
    public override void OnAwake () {
		base.OnAwake();

		BoxCollider2D[] boxColliders = this.GetComponents<BoxCollider2D>();
		this.colliderList = new ArrayList();
		this.colliderList.AddRange(boxColliders);

		foreach (BoxCollider2D box in this.colliderList) {
			box.isTrigger = true;
		}
	}

    protected void Start()
    {
        StartCoroutine(InputListener());
    }

    // Update is called once per frame
    private IEnumerator InputListener()
    {
        while (enabled)
        { //Run as long as this is activ

            if (Input.GetMouseButtonDown(0) && WorldInteractionController.getComponent().enableInteractions)
                yield return ClickEvent();

            yield return null;
        }
    }

    private IEnumerator ClickEvent()
    {
        //pause a frame so you don't pick up the same mouse down event.
        yield return new WaitForEndOfFrame();

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(pos);

        float count = 0f;
        while (count < doubleClickTimeLimit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitCollider != null && hitCollider.gameObject == this.gameObject)
                {
                    DoubleClick();
                }
                yield break;
            }
            count += Time.deltaTime;// increment counter by change in time between frames
            yield return null; // wait for the next frame
        }
        if (hitCollider != null && hitCollider.gameObject == this.gameObject)
        {
            SingleClick();
        }
    }

    public virtual void SingleClick()
    {
        Debug.Log("Single Click");
    }

    public virtual void DoubleClick()
    {
        Debug.Log("Double Click");
    }


    //public override void OnUpdate()
    //{

    //}

    ////public override void OnUpdate()
    ////{
    ////    base.OnUpdate();
    ////    //if (enableInteractions == false)
    ////    //{
    ////    //    return;
    ////    //}

    ////    if (Input.GetMouseButtonUp(0))
    ////    {
    ////        mouseIsPressed = false;
    ////        return;
    ////    }

    ////    if (Input.GetMouseButtonDown(0) && !mouseIsPressed)
    ////    {
    ////        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    ////        Collider2D hitCollider = Physics2D.OverlapPoint(pos);
    ////        if (hitCollider != null)
    ////        {
    ////            Tap();
    ////        }


    ////        //Tap (GetTappedGameObject ());
    ////        mouseIsPressed = true;
    ////        return;
    ////    }


    ////    /**
    ////     * If doubleTap
    ////     *  self.DoubleTap();
    ////    */


    ////    //      if (Input.GetMouseButton (0) && !mouseIsPressed) {
    ////    //      
    ////    //          HoldTap (GetTappedGameObject ());   
    ////    //          mouseIsPressed = true;
    ////    //          return;
    ////    //      }
    ////}


    //public virtual void Tap()
    //{
    //    Debug.Log("TAP EN TAPPABLE!!");
    //}

    //public virtual void DoubleTap()
    //{

    //}
}
