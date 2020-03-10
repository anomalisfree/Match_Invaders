using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text yourScoreText;
    [SerializeField] private Text bestScoreText;

    public void CheckScore(int score)
    {
        int.TryParse(File.ReadAllText(Application.dataPath + "/Settings/score.dat"), out var bestScore);

        yourScoreText.text = "Your score: " + score;

        if (score > bestScore)
        {
            bestScoreText.text = "new record";
            File.WriteAllText(Application.dataPath + "/Settings/score.dat", score.ToString());
        }
        else
        {
            bestScoreText.text = "Best score: " + bestScore;
        }
        
        gameObject.SetActive(true);
    }
}