using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerCreator : MonoBehaviour {

	public Transform itemContainer;


	public Transform createContainerWithItemImage(Transform itemImage) {
		Transform container = Instantiate(itemContainer, new Vector2(), Quaternion.identity) as Transform;
		container.SetParent(this.transform);
		container.localScale = new Vector2(1,1);

		itemImage.SetParent(container);
		((RectTransform)itemImage.transform).anchoredPosition = new Vector2();
		((RectTransform)itemImage.transform).localScale = new Vector2(1,1);

		return container;
	}
}
