using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the changing between scenes.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    private FadeInOut fade;

    public AudioClip transition;

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
        // call fade and play transition clip
        fade.FadeIn();
        AudioManager.instance.PlayBackground(transition);

        // load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
