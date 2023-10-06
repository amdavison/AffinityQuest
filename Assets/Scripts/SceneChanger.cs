using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the changing between scenes.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1.75f;

    /// <summary>
    /// Begins quest game and loads game scene level.
    /// </summary>
    public void StartQuest()
    {
        Debug.Log("Starting quest!");
        StartCoroutine(LoadLevel("GameScene"));
    }

    /// <summary>
    /// Quits application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    /// <summary>
    /// Controls fade out between levels.
    /// </summary>
    /// <param name="name">string name of level to load</param>
    /// <returns>IEnumerator transition time</returns>
    private IEnumerator LoadLevel(string name)
    {
        // play animation
        animator.SetTrigger("Start");
        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
