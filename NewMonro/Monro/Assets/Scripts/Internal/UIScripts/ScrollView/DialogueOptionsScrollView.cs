using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionsScrollView : MonoBehaviour {
    public GameObject ButtonTemplate;
    public GameObject moreOptionsIndicator;

    private List<GameObject> buttons;
    private List<int> deactivatedButtons;

    private void Awake() {
        this.buttons = new List<GameObject>();
        this.deactivatedButtons = new List<int>();
    }

    public void AddDialogueOptionButton(int optionIdx, string option) {
        if (this.deactivatedButtons.Contains(optionIdx)) {
            return;
        }

        GameObject go = Instantiate(ButtonTemplate) as GameObject;
        go.SetActive(true);

        DialogueOptionButton btn = go.GetComponent<DialogueOptionButton>();
        btn.SetDialogueOption(option, optionIdx);
        go.transform.SetParent(ButtonTemplate.transform.parent);
        
        go.transform.localScale = new Vector3(1, 1, 1);

        RectTransform scrollViewRect = (RectTransform)this.transform;
        float standardButtonHeight = ((RectTransform)go.transform).rect.height;
        this.moreOptionsIndicator.SetActive(buttons.Count * standardButtonHeight > scrollViewRect.rect.height);
        
        buttons.Add(go);
    }

    public void CleanButtonList() {
        foreach (GameObject g in this.buttons) {
            Destroy(g);
        }
        this.buttons.RemoveRange(0, this.buttons.Count);
    }

    public void MarkOptionButtonAsSelected(int index) {
        GameObject goB = this.buttons[index];
        goB.SetActive(false);

        this.deactivatedButtons.Add(index);
    }

    public void DeactivateAllButtons(bool deactivate) {
        if (deactivate) {
            foreach (GameObject go in this.buttons) {
                go.SetActive(false);
            }
        }
        else {
            for (int i = 0; i > this.buttons.Count; i++) {
                if (this.deactivatedButtons.Contains(i)) {
                    continue;
                }
                GameObject go = this.buttons[i];
                go.SetActive(true);
            }
        }
    }
}
