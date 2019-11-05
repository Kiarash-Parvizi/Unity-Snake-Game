using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    static int score = 0;
    public Text scoreText;
    static Text scoreT;

    public GameObject gameOverTab;
    static GameObject gameOverT;

    void Start() {
        scoreT = scoreText;
        gameOverT = gameOverTab;
        scoreText.text = "0";
    }

    public static void ADD () {
        score++;
        scoreT.text = score.ToString();
    }

    public static void GameOver() {
        Debug.Log("GameOver");
        gameOverT.SetActive(true);
        Time.timeScale = 0;
        Dis();
    }
    static void Dis() {
        Env_Generator.sprites.Clear();
    }

    public void Retry() {
        score = -1; ADD();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
