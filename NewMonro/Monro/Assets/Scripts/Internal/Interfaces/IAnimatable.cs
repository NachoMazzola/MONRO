using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatable {
	Animator GetAnimator();
	Transform GetTransform();
}
