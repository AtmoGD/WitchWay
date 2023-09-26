using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private TMP_Text timerText = null;
    [SerializeField] private Animator loadingScreen = null;

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

    public void StartLoadingFinished()
    {

    }
}
