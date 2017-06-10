using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryObserver {
	void OnInventoryAddedItem();
	void OnInventoryRemovedItem();
	void OnInventoryOpened();
	void OnInventoryClosed();
}
