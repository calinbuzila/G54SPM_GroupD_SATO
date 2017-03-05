using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelController : MonoBehaviour {

	//TODO Implement health/ lives mechanic(s)
	protected static int PlayerScore;
    public Text scoreText;

	void Start()
	{
		SetScore (0);
	}

    public void IncrementScore()
	{
		AddToScore (1);
        UpdateScoreDisplay();
    }

    public void DecrementScore()
	{
		AddToScore (-1);
        UpdateScoreDisplay();
    }

    public void AddToScore(int value)
	{
		PlayerScore += value;
		CheckNotNegative ();
        UpdateScoreDisplay();
    }

    public int GetScore()
	{
		return PlayerScore;
	}

	public void SetScore(int value)
	{
		PlayerScore = value;
		CheckNotNegative ();
        UpdateScoreDisplay ();
    }

    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + PlayerScore.ToString ();
    }

    protected void CheckNotNegative()
	{
		if (PlayerScore < 0)
		{
			PlayerScore = 0;
		}
	}
}
