using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Fade startFade;

    private void Start()
    {
        int.TryParse(File.ReadAllText(Application.dataPath + "/Settings/score.dat"), out var bestScore);
        bestScoreText.text = "Best score: " + bestScore;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        startFade.gameObject.SetActive(true);
        startFade.targetAlpha = 1;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}