using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private float loadingDelay = 1f;
    [SerializeField] private TMP_Text timerText = null;
    [SerializeField] private Animator loadingScreen = null;
    [SerializeField] private Animator gameOverScreen = null;
    [SerializeField] private TMP_Text gameOverScoreText = null;
    [SerializeField] private TMP_Text gameOverHighScoreText = null;

    private void Update()
    {
        if (gameManager.IsSetActive)
            UpdateTimer();
    }

    private void UpdateTimer()
    {
        float sec = gameManager.GameTime % 60;
        float min = Mathf.Floor(gameManager.GameTime / 60);
        timerText.text = $"{min:00}:{sec:00}";
    }

    public void StartGame()
    {
        gameManager.StartLevel();
    }

    public void StartLoading()
    {
        loadingScreen.SetTrigger("FadeIn");
    }

    public void StartGameOver()
    {
        gameOverScreen.SetTrigger("FadeIn");

        gameOverScoreText.text = timerText.text;

        float highScore = PlayerPrefs.GetFloat(gameManager.LevelName + "HighScore", 0f);
        gameOverHighScoreText.text = $"{Mathf.Floor(highScore / 60):00}:{highScore % 60:00}";
    }

    public void RestartGame()
    {
        StartCoroutine(LoadSceneDelayed(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadSceneDelayed(0));
    }

    IEnumerator LoadSceneDelayed(int _scene)
    {
        loadingScreen.SetTrigger("FadeIn");
        yield return new WaitForSecondsRealtime(loadingDelay);
        SceneManager.LoadScene(_scene);
    }

    public void StartLoadingFinished()
    {

    }
}
