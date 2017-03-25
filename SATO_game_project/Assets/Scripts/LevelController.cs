using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public const int DefaultHealth = 100;
	public const int DefaultLives = 3;
	public const int DefaultScore = 0;
	public const int HealthLimit = 100;
	public const int LivesLimit = int.MaxValue;
	public const int ScoreLimit = int.MaxValue;
	//TODO Consider branching score, lives, etc into their own sub-classes.
	protected static int PlayerScore;
	protected static int PlayerHealth;
	protected static int PlayerLives;

    public Text scoreText;
    public Text healthText;
    public Text lifeText;
	protected RespawnPointController respawnPointController;
	protected PlayerController playerController;

	void Start()
	{
		Initialise ();
	}

	public void Initialise()
	{
		SetScore (DefaultScore);
		SetHealth (DefaultHealth);
		SetLives (DefaultLives);
		respawnPointController = GameObject.FindObjectOfType<RespawnPointController> ();
		playerController = GameObject.FindObjectOfType<PlayerController> ();
	}

    public void IncrementScore()
	{
		if (PlayerScore < int.MaxValue) 
		{
			AddToScore (1);
		}
	}

	public void IncrementLives()
	{
		if (PlayerLives < LivesLimit)
		{
			AddToLives (1);
		}
	}

    public void DecrementScore()
	{
		AddToScore (-1);
    }

	public void DecrementLives()
	{
		AddToLives (-1);
		CheckLivesRemain ();
	}

    public void AddToScore(int value)
	{
		PlayerScore += value;
		CheckNotNegative (ref PlayerScore);
		CheckScoreNotExceeding ();
        UpdateScoreDisplay ();
    }

	public void AddToHealth(int value)
	{
		PlayerHealth += value;
		CheckNotNegative (ref PlayerHealth);
        ResetHealthIfZero();
        CheckHealthNotExceeding();
        UpdateHealthDisplay ();
	}

	public void AddToLives(int value)
	{
		PlayerLives += value;
		CheckNotNegative (ref PlayerLives);
		CheckLivesNotExceeding ();
        UpdateLifeDisplay ();
    }

	public int GetHealth()
	{
		return PlayerHealth;
	}

	public void SetHealth(int value)
	{
		PlayerHealth = value;
		CheckNotNegative (ref PlayerHealth);
        ResetHealthIfZero();
        CheckHealthNotExceeding();
        UpdateHealthDisplay ();
	}

	public int GetLives()
	{
		return PlayerLives;
	}

	public void SetLives(int value)
	{
		PlayerLives = value;
		CheckNotNegative (ref PlayerLives);
		CheckLivesNotExceeding ();
        UpdateLifeDisplay ();
    }

    public int GetScore()
	{
		return PlayerScore;
	}

	public void SetScore(int value)
	{
		PlayerScore = value;
		CheckNotNegative (ref PlayerScore);
		CheckScoreNotExceeding ();
        UpdateScoreDisplay ();
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
            float RespawnDelay = playerController.RespawnDelay;
			UpdateLifeDisplay ();
			playerController.KillPlayer ();
            StartCoroutine(RespawnTimer(RespawnDelay));
        }
    }

	protected void CheckLivesRemain()
	{
		if (PlayerLives == 0)
		{
			// Restarts the scene if the player runs out of lives.
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
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
		scoreText.text = "Score: " + PlayerScore.ToString ();
	}

    protected void UpdateHealthDisplay()
    {
        healthText.text = "Health: " + PlayerHealth.ToString();
        UpdateHealthTextColour ();
    }

    protected void UpdateLifeDisplay()
    {
        lifeText.text = "Lives: " + PlayerLives.ToString();
    }

    IEnumerator RespawnTimer(float NumberOfSeconds)
    {
        yield return new WaitForSeconds(NumberOfSeconds);
        respawnPointController.Respawn();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        PlayerHealth = DefaultHealth;
        DecrementLives();
        StartCoroutine(InvulnerabilityFlasher());
    }

    IEnumerator InvulnerabilityFlasher()
    {
        playerController.GetComponent<Collider>().enabled = false;

        for (int i=0; i<playerController.InvulnerabilityFlashAmount; i++)
        {
            playerController.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(playerController.InvulnerabilityFlashSpeed);
            playerController.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(playerController.InvulnerabilityFlashSpeed);
        }

        playerController.GetComponent<Collider>().enabled = true;
    }
}
