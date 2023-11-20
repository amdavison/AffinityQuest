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
    [SerializeField] [TextArea] string credit2;
    [SerializeField] [TextArea] string credit3;

    private string credit1;
    private FadeInOut fade;
    private float stats;
    private List<string> credits;

    private void Start()
    {
        fade = FindFirstObjectByType<FadeInOut>();
        credit1 = GameManager.correctCount switch
        {
            4 or 5 => "Great job!\nYou answered most of the questions correctly and have a good understanding of emotions.",
            0 or 1 or 2 or 3 => "Oh no!\nIt looks like you need a little more work in understanding emotions. Better luck next time.",
            _ => "Congratulations!\nYou completed your quest successfully and have a deeper understanding of emotions.",
        };
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

        yield return new WaitForSeconds(6);

        foreach (string credit in credits)
        {
            fade.FadeIn();
            yield return new WaitForSeconds(2);
            creditText.text = credit;
            fade.FadeOut();
            yield return new WaitForSeconds(4);
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
        GameManager.questionsAsked = 0;
        GameManager.ActiveNPC = null;
        GameManager.level = Level.Dark;
        GameManager.portalActivated = false;
        GameManager.SFXHasPlayed = false;
    }
}