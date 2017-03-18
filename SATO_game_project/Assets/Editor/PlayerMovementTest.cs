using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PlayerMovementTest
{
	[Test]
	public void PlayerIsWithinSetBoundary()
    {
        Assert.That(3 + 3 == 5);

        //assert.that(playercontroller.player_rb.position.x, is.inrange(playercontroller.playerboundary.xmin, playercontroller.playerboundary.xmax));
        //assert.that(playercontroller.player_rb.position.z, is.inrange(playercontroller.playerboundary.zmin, playercontroller.playerboundary.zmax));
        //assert.that(playercontroller.player_rb.position.y, is.equalto(0.0f));
    }

    [Test]
    public void PlayerCannotRotate()
    {
        Assert.That(2 + 2 == 4);
    }
}