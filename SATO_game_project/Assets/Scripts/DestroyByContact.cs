using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
    MainController mainController;
	LevelController levelController;

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Collider>().name == "Enemy") {

            Destroy(gameObject);

            levelController = GameObject.FindObjectOfType(typeof(LevelController)) as LevelController;

            if (gameObject.CompareTag("RedBullet"))
            {
                levelController.IncrementScore();
                Destroy(other.gameObject);
            }
            else if (gameObject.CompareTag("YellowBullet"))
            {
                levelController.DecrementScore();
            } 
			

            Enemy enemyModel = new Enemy();
            enemyModel.decreaseEnemies();
            //Debug.Log(Enemy.NrOfEnemies);
            // if the number of enemies reaches 0 then recall the routine responsible for spawning another wave of enemies
            if (Enemy.NrOfEnemies == 0)
            {
                mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
                mainController.StartFromExternalSourceCouroutine();
            }

		}
	}
}
