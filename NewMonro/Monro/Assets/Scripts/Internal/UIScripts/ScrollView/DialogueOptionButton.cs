using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionButton : MonoBehaviour
{
    public int optionIdx;

    public void SetDialogueOption(string option, int optionIdx)
    {
        this.transform.Find("Text").GetComponent<Text>().text = option;
        this.optionIdx = optionIdx;
    }

    public void ButtonTap()
    {
        DialogueUI dialogue = WorldObjectsHelper.getDialogueRunnerGO().GetComponent<DialogueUI>();
        dialogue.SetOption(this.optionIdx);
    }
}
