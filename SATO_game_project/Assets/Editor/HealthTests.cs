using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class HealthTests {

	/// <summary>
	/// Checks that the player's health initialises to the default.
	/// </summary>
	[Test]
	public void HealthInitialisesToDefault ()
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.Initialise ();
		Assert.AreEqual (levelController.GetHealth (), LevelController.DefaultHealth);
	}

	/// <summary>
	/// Checks that the player's health may not go into the negative through SetHealth.
	/// </summary>
	/// <param name="setToValue"></param>
	[TestCase(-10)]
	[TestCase(-420)]
	public void HealthNeverGoesNegativeThroughSetHealth (int setToValue)
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetHealth (setToValue);
		Assert.IsTrue (levelController.GetHealth () >= 0);
	}

	/// <summary>
	/// Checks that the player's health may not go into the negative through AddToHealth.
	/// </summary>
	/// <param name="startingHealth"></param>
	/// <param name="affectingValue"></param>
	[TestCase(LevelController.DefaultHealth, -(LevelController.DefaultHealth + 10))]
	[TestCase(10, -420)]
	public void HealthNeverGoesNegativeThroughAddToHealth (int startingHealth, int affectingValue)
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetHealth (startingHealth);
		levelController.AddToHealth (affectingValue);
		Assert.IsTrue (levelController.GetHealth () >= 0);
	}

	/// <summary>
	/// Checks that the player's health may not go above the limit through SetHealth.
	/// </summary>
	/// <param name="setToValue"></param>
	[TestCase(LevelController.HealthLimit + 10)]
	public void HealthNeverExceedsLimitThroughSetHealth (int setToValue)
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetHealth (setToValue);
		Assert.AreEqual (levelController.GetHealth (), LevelController.HealthLimit);
	}

	/// <summary>
	/// Checks that the player's health may not go above the limit through AddToHealth. 
	/// </summary>
	/// <param name="startingHealth"></param>
	/// <param name="affectingValue"></param>
	[TestCase(LevelController.HealthLimit - 10, 20)]
	public void HealthNeverExceedsLimitThroughAddToHealth (int startingHealth, int affectingValue)
	{
		LevelController levelController = GameObject.FindObjectOfType<LevelController> ();
		levelController.SetHealth (startingHealth);
		levelController.AddToHealth (affectingValue);
		Assert.AreEqual (levelController.GetHealth (), LevelController.HealthLimit);
	}

}
