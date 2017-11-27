using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICommand {
	protected bool finished;

	public virtual void Prepare() {}
	public virtual void UpdateCommand() {}
	public virtual bool Finished() { return false; }
}
