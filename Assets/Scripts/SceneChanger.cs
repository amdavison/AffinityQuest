using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.75f;

    public void StartQuest()
    {
        Debug.Log("Starting quest!");
        StartCoroutine(LoadLevel("GameScene"));
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private IEnumerator LoadLevel(string name)
    {
        // play animation
        transition.SetTrigger("Start");
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
