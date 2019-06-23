using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DestroyGameObjectCommandParameters: ICommandParamters {
	public GameObject GameObjectToDestroy;

	public CommandType GetCommandType() {
		return CommandType.DestroyGameObjectCommandType;
	}
}


public class DestroyGameObjectCommand : ICommand {
	
	public GameObject GameObjectToDestroy;


	public DestroyGameObjectCommand() {
		
	}

	public DestroyGameObjectCommand(GameObject toDestroy) {
		this.GameObjectToDestroy = toDestroy;
	}

	public DestroyGameObjectCommand(ICommandParamters parameters) {
		DestroyGameObjectCommandParameters p = (DestroyGameObjectCommandParameters)parameters;
		this.GameObjectToDestroy = p.GameObjectToDestroy;
	}

	public override void Prepare() {

	}

	public override void WillStart() {
        this.isRunning = true;
	}


    public override void ExecuteOnce()
    {
        base.ExecuteOnce();
        if (this.isRunning)
        {
            if (this.GameObjectToDestroy != null)
            {
                GameObject.Destroy(this.GameObjectToDestroy);
                this.isRunning = false;
            }
        }
    }


    public override CommandType GetCommandType() { 
		return CommandType.DestroyGameObjectCommandType; 
	}
}
