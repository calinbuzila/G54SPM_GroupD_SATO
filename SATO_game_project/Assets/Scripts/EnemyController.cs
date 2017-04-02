﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MainController mainController;
    public bool isMoving;
    public float kamikazeSpeed;
    public float homingSpeed;
    public int homingTime;
    public float fireRate;
    public GameObject enemyBullet;
    public EnemySpawner enemySpawner;
    public Transform enemyTransform;
    public Transform enemyShotSpawn;
    public Transform playerTransform;
    private bool isHoming = true;

    protected float nextFire;
    protected ColourController colourController;
    protected LevelController levelController;
    protected enum Behaviours { IdleTarget, Shooter, RotatingShooter, Kamikaze, HomingKamikaze, StrongHomingKamikaze };
    static protected int NumBehaviours = (int)System.Enum.GetNames(typeof(Behaviours)).Length;
    protected int randomBehaviourNumber;

    void Start()
    {
        colourController = GameObject.FindObjectOfType<ColourController>();
        mainController = GameObject.FindObjectOfType(typeof(MainController)) as MainController;
        levelController = GameObject.FindObjectOfType<LevelController>();

        isMoving = false;
        colourController.AssignRandomColour(gameObject);
        randomBehaviourNumber = Random.Range(0, NumBehaviours);
    }

    //TODO Remove after subclass system is done
    //TODO Move ShootAttack and Kamikaze attack to respective sub classes later
    void Update()
    {
        //TODO 	Switch to subclass creation, constantly updating on
        //		a switch might get expensive later.
        //TODO 	Look into a more elegant solution than casting every case.
        switch (randomBehaviourNumber)
        {
            case (int)Behaviours.Shooter:
                ShootAttack();
                break;
            case (int)Behaviours.RotatingShooter:
                RotateToPlayer();
                ShootAttack();
                break;
            case (int)Behaviours.Kamikaze:
                KamikazeAttack();
                break;
            case (int)Behaviours.HomingKamikaze:
                if (isHoming)
                {
                    HomingKamikaze();
                    StartCoroutine(ResetHoming());
                }
                break;
            case (int)Behaviours.StrongHomingKamikaze:
                HomingKamikaze();
                break;
        }
    }

    /// <summary>
    /// Method called when an objects collision mesh collides with the meshes of other game objects.
    /// </summary>
    /// <param name="other">The collider of the other object that the object this script is attached to just hit</param>
    //TODO Move to kamikaze subclass later
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().name.Contains("Player"))
        {
            //var playerIsRespawning = other.GetComponent<Renderer>().enabled;
            if (!LevelController.playerIsRespawning)
            {
                levelController.AddToHealth(-20);
                Destroy(gameObject);
            }
            else
            {
                this.isHoming = false;
                RestoreRotation();
                KamikazeAttack();
            }
        }
    }

    protected IEnumerator ResetHoming()
    {
        yield return new WaitForSeconds(homingTime);
        this.isHoming = false;
        // need to use Euler Angles to set up the rotation of the object
        RestoreRotation();
        KamikazeAttack();

    }
    private void RestoreRotation()
    {
        Vector3 eulerAngles = new Vector3(0, 0, 90);
        enemyTransform.rotation = Quaternion.Euler(eulerAngles);
    }

    protected void KamikazeAttack()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10.0f, 0.0f, transform.position.z), kamikazeSpeed * Time.deltaTime);
    }

    protected void RotateToPlayer()
    {
        var player = GameObject.Find("Player(Clone)");
        if (player == null) return;
        playerTransform = player.GetComponent<Transform>();

        enemyTransform.LookAt(playerTransform);
        enemyTransform.Rotate(Vector3.right, 90);
    }

    protected void ShootAttack()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet, enemyShotSpawn.position, enemyShotSpawn.rotation);
        }
    }

    protected void HomingKamikaze()
    {
        var player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        if (player != null)
        {
            var playerTransform = player.GetComponent<Transform>();
            enemyTransform.LookAt(playerTransform);
            enemyTransform.Rotate(Vector3.right, 90);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), homingSpeed * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        Enemy.NrOfEnemies -= 1;
        Debug.Log("nrenemies:" + Enemy.NrOfEnemies);
        if (Enemy.NrOfEnemies == 0 && !MainController.CoroutineIsRunning)
        {
            enemySpawner = GameObject.FindObjectOfType(typeof(EnemySpawner)) as EnemySpawner;
            Enemy.NrOfEnemies = 0;
            if (enemySpawner != null && mainController != null)
            {
                enemySpawner.SpawnPointCoroutine();
                mainController.StartFromExternalSourceCoroutine();
            }
        }
    }
}
