using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Image[] hearts;    // Make sure to put hearts in reverse order
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private float timeLimit = 600f;

    private bool isPaused;
    private float timer;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }

        timer -= Time.deltaTime;
        timerText.text = "Timer: " + (int)timer;
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 1f : 0f;
        pauseScreen.SetActive(isPaused ? false : true);
    }

    public void TakeDamage(int health)
    {
        for (int i = 0; i < 5 - health; i++)
        {
            hearts[i].color = Color.black;
        }
    }

    public void Victory(float lostHealth)
    {
        Time.timeScale = 0f;
        finalScoreText.text = "Score: " + (timer * (0.2f * lostHealth));
        victoryScreen.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }
}
