using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the changing between scenes.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    private FadeInOut fade;

    private void Start()
    {
        fade = FindFirstObjectByType<FadeInOut>();
    }

    /// <summary>
    /// Loads new level.
    /// </summary>
    /// <param name="name">string name of level to load</param>
    public void LoadScene(string name)
    {
        StartCoroutine(LoadAsyncScene(name));
    }

    /// <summary>
    /// Controls fade out between levels.
    /// </summary>
    /// <param name="name">string name of level to load</param>
    /// <returns>IEnumerator transition time</returns>
    private IEnumerator LoadAsyncScene(string name)
    {
        // call fade
        fade.FadeIn();

        // wait
        //yield return new WaitForSeconds(1);

        // load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
