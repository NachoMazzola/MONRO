using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMenuRenderableTrait: MonoBehaviour {
	public Transform prefab;

	[HideInInspector]
	public CommandType AssociatedMenuCommandType;
}
