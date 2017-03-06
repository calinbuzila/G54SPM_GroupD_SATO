﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelController : MonoBehaviour {

	//TODO Consider branching score, lives, etc into their own sub-classes.
	//TODO Implement health/ lives mechanic(s).
	protected static int PlayerScore;
    public Text scoreText;

	void Start()
	{
		Initialise ();
	}

	public void Initialise()
	{
		SetScore (0);
	}

    public void IncrementScore()
	{
		if (PlayerScore < int.MaxValue) 
		{
			AddToScore (1);
		}
	}

    public void DecrementScore()
	{
		AddToScore (-1);
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

    protected void CheckNotNegative()
	{
		if (PlayerScore < 0)
		{
			PlayerScore = 0;
		}
	}

	protected void UpdateScoreDisplay()
	{
		scoreText.text = "Score: " + PlayerScore.ToString ();
	}
}
