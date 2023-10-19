using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        scoreText.text = "Score: " + score.ToString();

        if (score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
        }

        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();
    }

    public void restart()
    {
        SceneManager.LoadScene(1);
    }

    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
