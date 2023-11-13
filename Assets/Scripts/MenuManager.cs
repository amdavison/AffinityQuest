using UnityEngine;

/// <summary>
/// Manages Main menu.
/// </summary>
public class MenuManager : MonoBehaviour
{
    public SceneChanger sceneChanger;

    /// <summary>
    /// Begins quest game and loads game scene level.
    /// </summary>
    public void StartQuest()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        sceneChanger.LoadScene("GameScene");
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
