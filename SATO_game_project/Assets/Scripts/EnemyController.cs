using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected ColourController colourController;
	protected LevelController levelController;
    MainController mainController;
    public bool isMoving;
    public float kamikazeSpeed;
    private int kamikazeRandomNumber;
	EnemySpawner enemySpawner;

    void Start()
    {
        colourController = GameObject.FindObjectOfType<ColourController>();
        mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
		levelController = GameObject.FindObjectOfType<LevelController> ();

        colourController.AssignColour(gameObject.GetComponent<Renderer>()  ,colourController.redMaterial);
        isMoving = false;
        kamikazeRandomNumber = Random.Range(0, 3);
    }
		
    void Update()
    {
        if (kamikazeRandomNumber == 2)
        {
            KamikazeAttack();
        }
    }

    /// <summary>
    /// Method called when an objects collision mesh collides with the meshes of other game objects.
    /// </summary>
    /// <param name="other">The collider of the other object that the object this script is attached to just hit</param>
    void OnTriggerEnter(Collider other)
    {
		if (other.GetComponent<Collider> ().name.Contains ("Player"))
		{
			levelController.AddToHealth (-20);
			Destroy (gameObject);
		}
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
