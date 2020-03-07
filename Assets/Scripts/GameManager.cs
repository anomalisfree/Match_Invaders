using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameScene;

    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(sceneName);
    }
}