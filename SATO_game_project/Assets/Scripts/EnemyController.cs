using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MainController mainController;
    public bool isMoving;
    public float kamikazeSpeed;
	public float fireRate;
	public GameObject enemyBullet;
	public EnemySpawner enemySpawner;
	public Transform enemyShotSpawn;

	protected int nextFire;
	protected ColourController colourController;
	protected LevelController levelController;
	protected enum Behaviours {IdleTarget, Shooter, Kamikaze};
	static protected int NumBehaviours = (int)System.Enum.GetNames(typeof(Behaviours)).Length;
	protected int randomBehaviourNumber;

    void Start()
    {
        colourController = GameObject.FindObjectOfType<ColourController>();
        mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
		levelController = GameObject.FindObjectOfType<LevelController> ();

        isMoving = false;
        colourController.AssignRandomColour(gameObject, false);
		randomBehaviourNumber = Random.Range(0, NumBehaviours);
    }
		
    void Update()
    {
		//TODO 	Switch to subclass creation, constantly updating on
		//		a switch might get expensive later.
		//TODO 	Look into a more elegant solution than casting every case.
		switch (randomBehaviourNumber) 
		{
		case (int)Behaviours.Shooter:
			ShootAttack ();
			break;
		case (int)Behaviours.Kamikaze:
			KamikazeAttack ();
			break;
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

    protected void KamikazeAttack()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10.0f, 0.0f, transform.position.z), kamikazeSpeed * Time.deltaTime);
    }

	protected void ShootAttack()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (enemyBullet, enemyShotSpawn.position, enemyShotSpawn.rotation);
		}
	}

	void OnDestroy() 
	{
		
		Enemy.NrOfEnemies -= 1;
		Debug.Log ("nrenemies:" + Enemy.NrOfEnemies);
        if (Enemy.NrOfEnemies == 0 && !MainController.CouroutineIsRunning)
		{
			enemySpawner = GameObject.FindObjectOfType(typeof(EnemySpawner)) as EnemySpawner;
            Enemy.NrOfEnemies = 0;
            enemySpawner.SpawnPointCouroutine();
			mainController.StartFromExternalSourceCouroutine();

		}
	}
}
