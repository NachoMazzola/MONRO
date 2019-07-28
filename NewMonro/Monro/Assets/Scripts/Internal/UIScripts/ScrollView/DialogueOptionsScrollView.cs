using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOptionsScrollView : MonoBehaviour {
    public GameObject ButtonTemplate;

    private List<GameObject> buttons;
    private List<int> deactivatedButtons;

    private void Start() {
        this.buttons = new List<GameObject>();
        this.deactivatedButtons = new List<int>();
    }

    public void AddDialogueOptionButton(int optionIdx, string option) {
        GameObject go = Instantiate(ButtonTemplate) as GameObject;
        go.SetActive(true);

        DialogueOptionButton btn = go.GetComponent<DialogueOptionButton>();
        btn.SetDialogueOption(option, optionIdx);
        go.transform.SetParent(ButtonTemplate.transform.parent);
        
        go.transform.localScale = new Vector3(1, 1, 1);

        buttons.Add(go);
    }

    public void MarkOptionButtonAsSelected(int index) {
        GameObject goB = this.buttons[index];
        goB.SetActive(false);

        this.deactivatedButtons.Add(index);
    }
}
