using UnityEngine;
using System.Collections;

public interface IIMButtonInterface {

	void ExecuteAction();
	Transform GetPrefab();
	void EnableComponent(bool enable);
	bool IsEnabled();

}
