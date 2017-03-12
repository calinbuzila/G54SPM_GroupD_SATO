using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class HealthTests {

	/// <summary>
	/// Checks that the player's health initialises to 100.
	/// </summary>
	[Test]
	public void HealthInitialisesToOneHundred() {
		
	}

	/// <summary>
	/// Checks that the player's health may not go into the negative through SetHealth.
	/// </summary>
	/// <param name="setToValue"></param>
	[TestCase(-10)]
	public void HealthNeverGoesNegativeThroughSetHealth(int setToValue)
	{
		
	}

	/// <summary>
	/// Checks that the player's health may not go into the negative through AddToHealth.
	/// </summary>
	/// <param name="startingHealth"></param>
	/// <param name="affectingValue"></param>
	[TestCase(100, -110)]
	public void HealthNeverGoesNegativeThroughAddToHealth(int startingHealth, int affectingValue)
	{
		
	}

	/// <summary>
	/// Checks that the player's health may not go above 100 through SetHealth.
	/// </summary>
	/// <param name="setToValue"></param>
	[TestCase(110)]
	public void HealthNeverExceedsLimitThroughSetHealth(int setToValue)
	{

	}

	/// <summary>
	/// Checks that the player's health may not go above 100 through AddToHealth. 
	/// </summary>
	/// <param name="startingHealth"></param>
	/// <param name="affectingValue"></param>
	[TestCase(90, 20)]
	public void HealthNeverExceedsLimitThroughAddToHealth(int startingHealth, int affectingValue)
	{
		
	}

}
