using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemContainerCreator : MonoBehaviour {

	public Transform itemContainer;

	public Transform createContainerWithItemImage(Transform itemImage) {
		Transform container = Instantiate(itemContainer, new Vector3(), Quaternion.identity) as Transform;
		container.SetParent(this.transform);
		container.localScale = new Vector3(1,1,1);

		itemImage.SetParent(container);
		((RectTransform)itemImage.transform).anchoredPosition = new Vector3();
		((RectTransform)itemImage.transform).localScale = new Vector3(1,1,1);

		return container;
	}
}
