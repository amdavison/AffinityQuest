using UnityEngine;

/// <summary>
/// Manages Main menu.
/// </summary>
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Begins quest game and loads game scene level.
    /// </summary>
    public void StartQuest()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
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
}
