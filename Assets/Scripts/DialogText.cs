using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogText : MonoBehaviour
{
    public float waitTime = 0.2f;

    public GameObject dialogPanel;

    public GameObject btnPanel;

    public List<Button> btns;

    public TextMeshProUGUI dialogText;

    private NPCData npcData;

    private void Start()
    {
        dialogText.text = string.Empty;
    }

    /// <summary>
    /// Begins interaction with NPC.
    /// </summary>
    /// <param name="data">NPCData dialog data for NPC</param>
    public void StartInteraction(NPCData data)
    {
        dialogPanel.SetActive(true);
        dialogText.text = string.Empty;
        npcData = data;
        List<string> options = new() { data.opt1, data.opt2, data.opt3 };
        GameManager.instance.Shuffle(options);

        for (int i = 0; i < btns.Count; i++)
        {
            TextMeshProUGUI btnText = btns[i].GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = options[i];
        }

        StartCoroutine(WriteDialog(data));
    }

    /// <summary>
    /// Completes interaction with NPC.
    /// </summary>
    public void EndInteraction()
    {
        foreach (Button btn in btns)
        {
            btn.interactable = true;
        }
        dialogPanel.SetActive(false);
        btnPanel.SetActive(false);
        Player.instance.canMove = true;
        if (GameManager.ActiveNPC != null) { GameManager.isInactive = true; }
    }

    /// <summary>
    /// Displays movement/interaction hint for user.
    /// </summary>
    public void ShowHint()
    {
        // TODO: Add functionality for displaying hint to user.
        Debug.Log("ShowHint...");
    }

    /// <summary>
    /// Writes welcome and question dialog to screen.
    /// </summary>
    /// <param name="data">NPCData dialog data for NPC</param>
    /// <returns>IEnumerator wait time</returns>
    private IEnumerator WriteDialog(NPCData data)
    {
        yield return new WaitForSeconds(1);

        foreach (char textChar in data.greeting)
        {
            dialogText.text += textChar;
            AudioManager.instance.PlaySFX(AudioManager.instance.dialog);
            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitForSeconds(2);

        AudioManager.instance.PlaySFX(AudioManager.instance.npc);
        dialogText.text = string.Empty;

        foreach (char textChar in data.dialog)
        {
            dialogText.text += textChar;
            AudioManager.instance.PlaySFX(AudioManager.instance.dialog);
            yield return new WaitForSeconds(waitTime);
        }

        btnPanel.SetActive(true);
    }

    /// <summary>
    /// Handles button functionality for interactions.
    /// </summary>
    /// <param name="btnText">TextMeshProUGUI button text</param>
    public void ButtonSelected(TextMeshProUGUI btnText)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        NPC npc = GameManager.ActiveNPC;

        if (btnText.text == "Continue Quest")
        {
            Debug.Log("Continuing quest...");
            EndInteraction();
            return;
        }
        else if (btnText.text == npcData.opt1)
        {
            Debug.Log("Correct answer");
            GameManager.correctCount++;
            StartCoroutine(DisplayDialog(npcData.correctDialog));
            npc.gameObject.SetActive(false);
            GameManager.ActiveNPC = null;
        }
        else
        {
            Debug.Log("Incorrect answer");
            StartCoroutine(DisplayDialog(npcData.incorrectDialog));
            npc.gameObject.SetActive(false);
        }
        ToggleButtons();
    }

    /// <summary>
    /// Displays resulting dialog to user.
    /// </summary>
    /// <param name="dialog">string dialog to be written to screen</param>
    /// <returns>IEnumerator wait time</returns>
    private IEnumerator DisplayDialog(string dialog)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.npc);

        dialogText.text = string.Empty;

        foreach (char textChar in dialog)
        {
            dialogText.text += textChar;
            AudioManager.instance.PlaySFX(AudioManager.instance.dialog);
            yield return new WaitForSeconds(waitTime);
        }
        yield return null;
    }

    /// <summary>
    /// Toggles button interactability and activates quest continuation.
    /// </summary>
    private void ToggleButtons()
    {
        foreach (Button btn in btns)
        {
            TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText.text == npcData.opt1)
            {
                btnText.text = "Continue Quest";
            }
            else
            {
                btnText.text = string.Empty;
                btn.interactable = false;
            }
        }
    }
}
