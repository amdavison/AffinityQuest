using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] [TextArea] string credit1;
    [SerializeField] [TextArea] string credit2;
    [SerializeField] [TextArea] string credit3;

    private FadeInOut fade;
    private List<string> credits;

    private void Start()
    {
        fade = FindFirstObjectByType<FadeInOut>();
        credits = new List<string>() { credit1, credit2, credit3};
        StartCoroutine(PlayDemoCredits());
    }

    /// <summary>
    /// Plays demo credits.
    /// </summary>
    /// <returns>IEnumerator wait time</returns>
    private IEnumerator PlayDemoCredits()
    {
        yield return new WaitForSeconds(2);
        foreach (string credit in credits)
        {
            fade.FadeIn();
            yield return new WaitForSeconds(2);
            creditText.text = credit;
            fade.FadeOut();
            yield return new WaitForSeconds(10);
        }

        SceneChanger.instance.LoadScene("GameScene");
    }
}
