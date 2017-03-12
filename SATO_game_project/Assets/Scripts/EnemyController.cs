using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    MainController mainController;
    public bool isMoving;
    public float kamikazeSpeed;
    private int kamikazeRandomNumber;
	EnemySpawner enemySpawner;

    // Use this for initialization
    void Start()
    {
        // get the main controller object
        mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
        isMoving = false;
        kamikazeRandomNumber = Random.Range(0, 3);

    }

    // Update is called once per frame
    void Update()
    {
        if (kamikazeRandomNumber == 2)
        {
            KamikazeAttack();
        }
    }

    /// <summary>
    /// Method called when an objects rigidbody collides with other rigidbodies of other game objects
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
       
    }

    void KamikazeAttack()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10.0f, 0.0f, transform.position.z), kamikazeSpeed * Time.deltaTime);
    }

	void OnDestroy() 
	{
		
		Enemy.NrOfEnemies -= 1;
		Debug.Log ("nrenemies:" + Enemy.NrOfEnemies);
		if (Enemy.NrOfEnemies == 0)
		{
			mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
			enemySpawner = GameObject.FindObjectOfType(typeof(EnemySpawner)) as EnemySpawner;
            Enemy.NrOfEnemies = 0;
            enemySpawner.SpawnPointCouroutine();
			mainController.StartFromExternalSourceCouroutine();

		}
	}
}
