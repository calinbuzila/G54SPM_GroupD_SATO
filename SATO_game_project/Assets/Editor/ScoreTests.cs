using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ScoreTests {

	/// <summary>
	/// Checks that the score initialises to zero.
	/// </summary>
	[Test]
	public void ScoreInitialisesToZero() 
	{
		LevelController levelController = new LevelController();
		Assert.AreEqual(0, levelController.GetScore());
	}

	/// <summary>
	/// Checks that the score cannot ever go into the negative through LevelController.AddToScore(int);
	/// </summary>
	/// <param name="initialValue"></param>
	/// <param name="addedValue">
	[TestCase(0, -1)]
	public void ScoreNeverGoesNegative(int initialValue, int addedValue)
	{
		LevelController levelController = new LevelController ();

		levelController.SetScore (initialValue);
		levelController.AddToScore (addedValue);

		Assert.IsTrue (levelController.GetScore () >= 0);
	}
}
