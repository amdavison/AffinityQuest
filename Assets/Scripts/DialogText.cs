using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogText : MonoBehaviour
{
    [Header("Collectables")]
    [SerializeField] private GameObject sad;
    [SerializeField] private GameObject angry;
    [SerializeField] private GameObject scared;
    [SerializeField] private GameObject happy;
    [SerializeField] private GameObject strong;
    [SerializeField] private GameObject calm;

    public float waitTime = 0.2f;
    public GameObject dialogPanel;
    public GameObject btnPanel;
    public List<Button> btns;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI portalText;

    private NPCData npcData;

    private void Start()
    {
        dialogText.text = string.Empty;
    }

    private void Update()
    {
        portalText.gameObject.SetActive(GameManager.portalActivated);
    }

    /// <summary>
    /// Begins interaction with NPC.
    /// </summary>
    /// <param name="data">NPCData dialog data for NPC</param>
    public void StartInteraction(NPCData data)
    {
        GameManager.interactionStarted = true;
        GameManager.questionsAsked++;
        hintText.gameObject.SetActive(false);
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
        GameManager.interactionStarted = false;
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
        hintText.gameObject.SetActive(true);
        StartCoroutine(FlashHint());
    }

    /// <summary>
    /// Writes welcome and question dialog to screen.
    /// </summary>
    /// <param name="data">NPCData dialog data for NPC</param>
    /// <returns>IEnumerator wait time</returns>
    private IEnumerator WriteDialog(NPCData data)
    {
        //yield return new WaitForSeconds(1);

        foreach (char textChar in data.greeting)
        {
            dialogText.text += textChar;
            //AudioManager.instance.PlaySFX(AudioManager.instance.dialog);
            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitForSeconds(2);

        AudioManager.instance.PlaySFX(AudioManager.instance.npc);
        dialogText.text = string.Empty;

        foreach (char textChar in data.dialog)
        {
            dialogText.text += textChar;
            //AudioManager.instance.PlaySFX(AudioManager.instance.dialog);
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

        // get the active NPC from GameManager
        NPC npc = GameManager.ActiveNPC;

        if (btnText.text == "Continue Quest")
        {
            StopAllCoroutines();
            EndInteraction();
            return;
        }
        else if (btnText.text == npcData.opt1)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.correct);
            ShowCollectable(npcData.npcType);
            GameManager.correctCount++;
            StartCoroutine(DisplayDialog(npcData.correctDialog));
            npc.gameObject.SetActive(false);
            GameManager.ActiveNPC = null;
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.incorrect);
            StartCoroutine(DisplayDialog(npcData.incorrectDialog));
            npc.gameObject.SetActive(false);
        }

        GameManager.portalActivated = GameManager.interactionCount == GameManager.instance.TotalInteractions;

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
            //AudioManager.instance.PlaySFX(AudioManager.instance.dialog);
            yield return new WaitForSeconds(waitTime);
        }
        btns[0].interactable = true;
        //yield return null;
    }

    /// <summary>
    /// Toggles button interactability and activates quest continuation.
    /// </summary>
    private void ToggleButtons()
    {
        btns[0].GetComponentInChildren<TextMeshProUGUI>().text = "Continue Quest";
        btns[0].interactable = false;

        for (int i = 1; i < btns.Count; i++)
        {
            btns[i].GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
            btns[i].interactable = false;
        }
    }

    /// <summary>
    /// Flashes hint text.
    /// </summary>
    /// <returns>IEnumerator wait time</returns>
    private IEnumerator FlashHint()
    {
        for (int i = 0; i < 3; i++)
        {
            hintText.alpha = 0.0f;
            yield return new WaitForSeconds(0.3f);
            hintText.alpha = 1.0f;
            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// Shows collectable that belongs to npcType.
    /// </summary>
    /// <param name="npcType">NPCType type of NPC</param>
    private void ShowCollectable(NPCType npcType)
    {
        switch (npcType)
        {
            case NPCType.Sad:
                sad.SetActive(true);
                break;
            case NPCType.Angry:
                angry.SetActive(true);
                break;
            case NPCType.Scared:
                scared.SetActive(true);
                break;
            case NPCType.Happy:
                happy.SetActive(true);
                break;
            case NPCType.Strong:
                strong.SetActive(true);
                break;
            case NPCType.Calm:
                calm.SetActive(true);
                break;
            default:
                break;
        }
    }
}