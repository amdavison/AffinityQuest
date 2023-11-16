using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the changing between scenes.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    private FadeInOut fade;

    public static SceneChanger instance;

    private void Start()
    {
        instance = this;
        fade = FindFirstObjectByType<FadeInOut>();
    }

    /// <summary>
    /// Loads new level.
    /// </summary>
    /// <param sceneName="sceneName">string name of level to load</param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    /// <summary>
    /// Controls fade out between levels.
    /// </summary>
    /// <param sceneName="sceneName">string name of level to load</param>
    /// <returns>IEnumerator transition time</returns>
    private IEnumerator LoadAsyncScene(string sceneName)
    {
        // call fade and play transition clip
        AudioManager.instance.PlayBackground(AudioManager.instance.transition);
        fade.FadeIn();

        // load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
