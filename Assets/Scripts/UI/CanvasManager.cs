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

    [SerializeField] private AudioClip victoryTheme;
    [SerializeField] private AudioClip gameOverTheme;

    [SerializeField] private Image[] hearts;    // Make sure to put hearts in reverse order
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private float timeLimit = 600f;

    private bool isPaused;
    private float timer;
    private float countDownTimer;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        countDownTimer = timeLimit;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }

        countDownTimer -= Time.deltaTime;
        Mathf.Clamp(countDownTimer, 0f, 1000f);
        timer += Time.deltaTime;
        timerText.text = "Timer: " + (int)countDownTimer;
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
        float finalScore = ((int)timeLimit - (int)timer) * (0.2f * lostHealth);
        Mathf.Clamp(finalScore, 0, 700);
        finalScoreText.text = "Score: " + finalScore;
        finalTimeText.text = "Time: " + (int)timer;
        audioSource.PlayOneShot(victoryTheme);
        victoryScreen.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        audioSource.PlayOneShot(gameOverTheme);
        gameOverScreen.SetActive(true);
    }
}
