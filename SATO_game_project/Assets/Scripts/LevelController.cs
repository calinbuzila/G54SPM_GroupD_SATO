using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
	//TODO Add healthText and livesText displays.
	public Text scoreText;

	void Start()
	{
		Initialise ();
	}

	public void Initialise()
	{
		SetScore (DefaultScore);
		SetHealth (DefaultHealth);
		SetLives (DefaultLives);
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
		CheckHealthNotExceeding ();
		//TODO UpdateHealthDisplay ();
	}

	public void AddToLives(int value)
	{
		PlayerLives += value;
		CheckNotNegative (ref PlayerLives);
		CheckLivesNotExceeding ();
		//TODO UpdateLivesDisplay ();
	}

	public int GetHealth()
	{
		return PlayerHealth;
	}

	public void SetHealth(int value)
	{
		PlayerHealth = value;
		CheckNotNegative (ref PlayerHealth);
		CheckHealthNotExceeding ();
		//TODO UpdateHealthDisplay ();
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
		// TODO UpdateLivesDisplay ();
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

	protected void UpdateScoreDisplay()
	{
		scoreText.text = "Score: " + PlayerScore.ToString ();
	}
}
