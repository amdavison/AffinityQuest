using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SceneChanger sceneChanger;

    /// <summary>
    /// Begins quest game and loads game scene level.
    /// </summary>
    public void StartQuest()
    {
        Debug.Log("Starting quest!");
        sceneChanger.LoadScene("GameScene");
    }

    /// <summary>
    /// Quits application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting application!");
        Application.Quit();
    }
}
