using UnityEngine;
using System.Collections;

public interface IDraggable {
	void IDraggableStartedDrag();
	void IDraggableIsDragging();
	void IDraggableFinishedDragging();
}
