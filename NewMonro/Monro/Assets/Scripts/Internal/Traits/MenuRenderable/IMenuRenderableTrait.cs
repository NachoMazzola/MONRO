﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMenuRenderableTrait: Trait {
	public Transform MenuIconPrefab;

	[HideInInspector]
	public CommandType AssociatedMenuCommandType;
}
