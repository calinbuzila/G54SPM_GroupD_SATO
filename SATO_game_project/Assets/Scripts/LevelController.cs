using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class LevelController : MonoBehaviour
{
    public const int DefaultHealth = 100;
    public const int DefaultLives = 3;
    private string PlayerHighScores = "HighScores";
    public const int DefaultScore = 0;
    protected bool isGameOver = false;
    // control variable used for erasing all the data from the user prefs files.
    public bool RemoveUserScores;
    public const int HealthLimit = 100;
    public const int LivesLimit = int.MaxValue;
    public const int ScoreLimit = int.MaxValue;
    protected static int PlayerScore;
    protected static int PlayerHealth;
    protected static int PlayerLives;
    public static bool playerIsRespawning = false;

    public Text scoreText;
    public Text healthText;
    public Text lifeText;
    public Text highScoreText;
    public GameObject endPanel;
    protected RespawnPointController respawnPointController;
    protected PlayerController playerController;
    protected MainController mainController;

    void Start()
    {
        // is used for removing user preferences if an error occurs
        if (RemoveUserScores)
        {
            PlayerPrefs.DeleteKey(PlayerHighScores);
        }
        Initialise();
    }

    public void Initialise()
    {
        SetScore(DefaultScore);
        SetHealth(DefaultHealth);
        SetLives(DefaultLives);
        respawnPointController = GameObject.FindObjectOfType<RespawnPointController>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        mainController = GameObject.FindObjectOfType<MainController>();
    }

    public void IncrementScore()
    {
        if (PlayerScore < int.MaxValue)
        {
            AddToScore(1);
        }
    }

    public void IncrementLives()
    {
        if (PlayerLives < LivesLimit)
        {
            AddToLives(1);
        }
    }

    public void DecrementScore()
    {
        AddToScore(-1);
    }

    public void DecrementLives()
    {
        AddToLives(-1);
        CheckLivesRemain();
    }

    public void AddToScore(int value)
    {
        PlayerScore += value;
        CheckNotNegative(ref PlayerScore);
        CheckScoreNotExceeding();
        UpdateScoreDisplay();
    }

    public void AddToHealth(int value)
    {
        PlayerHealth += value;
        CheckNotNegative(ref PlayerHealth);
        ResetHealthIfZero();
        CheckHealthNotExceeding();
        UpdateHealthDisplay();
    }

    public void AddToLives(int value)
    {
        PlayerLives += value;
        CheckNotNegative(ref PlayerLives);
        CheckLivesNotExceeding();
        UpdateLifeDisplay();
    }

    public int GetHealth()
    {
        return PlayerHealth;
    }

    public void SetHealth(int value)
    {
        PlayerHealth = value;
        CheckNotNegative(ref PlayerHealth);
        ResetHealthIfZero();
        CheckHealthNotExceeding();
        if (healthText != null) 
		{ 
        	UpdateHealthDisplay();
        }
    }

    public int GetLives()
    {
        return PlayerLives;
    }

    public void SetLives(int value)
    {
        PlayerLives = value;
        CheckNotNegative(ref PlayerLives);
        CheckLivesNotExceeding();
        if (lifeText != null) 
		{ 
        	UpdateLifeDisplay();
        }
    }

    public int GetScore()
    {
        return PlayerScore;
    }

    public void SetScore(int value)
    {
        PlayerScore = value;
        CheckNotNegative(ref PlayerScore);
        CheckScoreNotExceeding();
        if (scoreText != null) 
        { 
        	UpdateScoreDisplay();
        }
    }

    protected void CheckNotNegative(ref int value)
    {
        if (value < 0)
        {
            value = 0;
        }
    }

    protected void ResetHealthIfZero()
    {
        if (PlayerHealth == 0)
        {
			if (playerController == null)
			{
				return;
			}
            float RespawnDelay = playerController.RespawnDelay;
            playerController.KillPlayer();
            StartCoroutine(RespawnTimer(RespawnDelay));
        }
    }

    protected void CheckLivesRemain()
    {
        if (PlayerLives == 0)
        {
            // Restarts the scene if the player runs out of lives.
            SaveScore(PlayerScore);
            mainController.DestroyAllEnemies();
            mainController.StopAllCoroutines();
            isGameOver = true;
            DisplayEndPanel();
        }
    }

    protected void CheckHealthNotExceeding()
    {
        if (PlayerHealth > HealthLimit)
        {
            PlayerHealth = HealthLimit;
        }
    }

    protected void CheckLivesNotExceeding()
    {
        if (PlayerLives > LivesLimit)
        {
            PlayerLives = LivesLimit;
        }
    }

    protected void CheckScoreNotExceeding()
    {
        if (PlayerScore > ScoreLimit)
        {
            PlayerScore = ScoreLimit;
        }
    }

    protected void UpdateHealthTextColour()
    {
        if (PlayerHealth > 75)
        {
            healthText.color = Color.green;
        }
        else if (PlayerHealth <= 75 && PlayerHealth > 50)
        {
            healthText.color = Color.yellow;
        }
        else if (PlayerHealth <= 50 && PlayerHealth > 25)
        {
            // Orange.
            healthText.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
        }
        else if (PlayerHealth <= 25)
        {
            healthText.color = Color.red;
        }
    }

    protected void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + PlayerScore.ToString();
    }

    protected void UpdateHealthDisplay()
    {
        healthText.text = "Health: " + PlayerHealth.ToString();
        UpdateHealthTextColour();
    }

    protected void UpdateLifeDisplay()
    {
        lifeText.text = "Lives: " + PlayerLives.ToString();
    }

    IEnumerator RespawnTimer(float NumberOfSeconds)
    {
        yield return new WaitForSeconds(NumberOfSeconds);
        DecrementLives();
        if (PlayerLives != 0)
        {
            SetHealth(DefaultHealth);
            respawnPointController.Respawn();
            playerController = GameObject.FindObjectOfType<PlayerController>();
            StartCoroutine(InvulnerabilityFlasher());
        }
    }

    IEnumerator InvulnerabilityFlasher()
    {
        playerController.GetComponent<Collider>().enabled = false;
        playerIsRespawning = true;
        for (int i = 0; i < playerController.InvulnerabilityFlashAmount; i++)
        {
            playerController.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(playerController.InvulnerabilityFlashSpeed);
            playerController.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(playerController.InvulnerabilityFlashSpeed);
        }

        playerController.GetComponent<Collider>().enabled = true;
        playerIsRespawning = false;
    }

    protected void SaveScore(int playerScore)
    {
        if (PlayerPrefs.HasKey(PlayerHighScores))
        {
            var playerScores = PlayerPrefs.GetString(PlayerHighScores).ToString().Split(',');
            List<int> scores;
            scores = new List<int>();
            foreach (var score in playerScores)
            {
                int scoreToAdd;

                if (Int32.TryParse(score, out scoreToAdd))
                {
                    scores.Add(scoreToAdd);
                }
            }
            scores.Add(PlayerScore);
            var topFiveScores = scores.OrderByDescending(scoreValue => scoreValue).Take(5);
            if (topFiveScores.Any())
            {
                var newListOfScores = String.Join(",", topFiveScores.Select(scoreValue => scoreValue.ToString()).ToArray());
                PlayerPrefs.SetString(PlayerHighScores, newListOfScores);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString(PlayerHighScores, playerScore.ToString());
            PlayerPrefs.Save();
        }
    }


    // when getting high scores need to check if it is an empty string: !String.IsNullOrEmpty(value)
    public string GetHighScores()
    {
        if (PlayerPrefs.HasKey(PlayerHighScores))
        {
            var playerScores = PlayerPrefs.GetString(PlayerHighScores).ToString().Split(',');
            var newListOfScores = String.Join("\n", playerScores.Select(scoreValue => scoreValue.ToString()).ToArray());
            return newListOfScores;
        }
        return "";
    }

    protected void UpdateHighScoreDisplay()
    {
        highScoreText.text = "Highscores:\n" + GetHighScores();
    }

    protected void DisplayEndPanel()
    {
        UpdateHighScoreDisplay();
        endPanel.SetActive(true);
    }

    public bool GetGameOverStatus()
    {
        return isGameOver;
    }
}
