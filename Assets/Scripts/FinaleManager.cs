using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages Final Menu.
/// </summary>
public class FinaleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private GameObject btnPanel;
    [SerializeField] [TextArea] string credit1;
    [SerializeField] [TextArea] string credit2;
    [SerializeField] [TextArea] string credit3;

    private FadeInOut fade;
    private float stats;
    private List<string> credits;

    private void Start()
    {
        fade = FindFirstObjectByType<FadeInOut>();
        StartCoroutine(PlayCredits());
    }

    /// <summary>
    /// Plays finale credits.
    /// </summary>
    /// <returns>IEnumerator wait time</returns>
    private IEnumerator PlayCredits()
    {
        stats = GameManager.correctCount * 100f / GameManager.questionsAsked;
        credits = new List<string>() { credit2, credit3 };
        creditText.text = credit1 + "\nGame Percentage: " + stats.ToString("0.00") + "%";

        yield return new WaitForSeconds(5);

        fade.FadeOut();
        AudioManager.instance.PlayBackground(AudioManager.instance.finale);

        yield return new WaitForSeconds(4);

        foreach (string credit in credits)
        {
            fade.FadeIn();
            yield return new WaitForSeconds(2);
            creditText.text = credit;
            fade.FadeOut();
            yield return new WaitForSeconds(2);
        }

        btnPanel.SetActive(true);
    }

    /// <summary>
    /// Resets game for a new quest.
    /// </summary>
    public void ReplayQuest()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        ResetGame();
        SceneChanger.instance.LoadScene("GameScene");
    }

    /// <summary>
    /// Quits application.
    /// </summary>
    public void QuitGame()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        Application.Quit();
    }

    /// <summary>
    /// Resets GameManager values.
    /// </summary>
    private void ResetGame()
    {
        GameManager.interactionCount = 0;
        GameManager.correctCount = 0;
        GameManager.ActiveNPC = null;
        GameManager.isInactive = false;
        GameManager.level = Level.Dark;
        GameManager.portalActivated = false;
        GameManager.SFXHasPlayed = false;
    }
}
