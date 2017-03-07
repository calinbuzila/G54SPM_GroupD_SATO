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
        //if (col.gameObject.name == "Enemy")
        //{
        //    // if the colliding object is an enemy and if the enemy is not moving => the first spawn of another enemy object
        //    // then the colliding enemy object will be destroyed and recreated and put into the scene on another position by calling the SpawnEnemy method from MainController
        //    if ((col.gameObject.transform.position.z > this.transform.position.z) || (col.gameObject.transform.position.x > this.transform.position.x))
        //    {
        //        var enemyState = col.gameObject.GetComponent<EnemyController>().isMoving;
        //        if (!enemyState)
        //        {
        //            Debug.Log("Colliding enemy");
        //            Enemy enemyModel = new Enemy();
        //            enemyModel.decreaseEnemies();
        //            Destroy(col.gameObject);
        //            mainController.SpawnEnemy();
        //        }

        //    }

        //}
    }

    void KamikazeAttack()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-10.0f, 0.0f, transform.position.z), kamikazeSpeed * Time.deltaTime);
    }
}
