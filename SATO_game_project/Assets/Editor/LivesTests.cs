using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class LivesTests {

	/// <summary>
	/// Checks that the player's lives initialise to the default amount.
	/// </summary>
	[Test]
	public void LivesInitialiseToDefault() {
		
	}

	/// <summary>
	/// Checks that the player's lives may be decremented correctly.
	/// </summary>
	/// <param name="initialValue"></param>
	[TestCase(3)]
	[TestCase(2)]
	[TestCase(1)]
	[TestCase(20)]
	public void LivesDecrementCorrectly(int initialValue)
	{

	}

	/// <summary>
	/// Checks that the player's lives may not go into the negative.
	/// </summary>
	[Test]
	public void LivesNeverGoNegativeThroughDecrementation()
	{

	}

	/// <summary>
	/// Checks like the player's lives may be incremented correctly.
	/// </summary>
	/// <param name="initialValue"></param>
	[TestCase(3)]
	[TestCase(20)]
	public void LivesIncrementCorrectly(int initialValue)
	{

	}

	/// <summary>
	/// Lives may not increment above the set limit.
	/// </summary>
	[Test]
	public void LivesNeverExceedLimitThroughIncrementation()
	{

	}
}
