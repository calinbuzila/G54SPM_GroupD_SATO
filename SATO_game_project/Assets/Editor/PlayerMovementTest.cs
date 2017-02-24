using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PlayerMovementTest
{
	[Test]
	public void PlayerIsWithinSetBoundary()
    {
        PlayerController playerController = new PlayerController();

        Assert.That(playerController.Player_rb.position.x, Is.InRange(playerController.PlayerBoundary.xMin, playerController.PlayerBoundary.xMax));
        Assert.That(playerController.Player_rb.position.z, Is.InRange(playerController.PlayerBoundary.zMin, playerController.PlayerBoundary.zMax));
        //Assert.That(playerController.Player_rb.position.y, Is.EqualTo(0.0f));
    }
}
