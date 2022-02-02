/*This is enemy Controller Script used for Enemy Movement and Collisions
 * Enemy gives damage to player when collides
 * enemy is destroyed when collides with border
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth = 150;
    public float speed = 5;

    private int enemyCollisonDamage = 10;
    private SpawnManager spawner;
    private GameManager manager;
    private GameObject player;
    private Vector3 spawnPos;
    private Vector3 Distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        transform.LookAt(player.transform);
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (manager.isGameOn)
        {
            EnemyMovement();
            DestroyingEnemy();
        }
      
       

    }

    /*this method is used for checking the health of enemy
     * health is lower then 0 then it destroys gameobject
     * sends the spawnPos to spawner's method spawn enemy to spawn another enemy
     */
    void DestroyingEnemy()
    {
        if (enemyHealth <= 0 && player !=null)
        {
            manager.EnemyKilledCounter(1);
            spawner.SpawnEnemy(spawnPos);
            Destroy(gameObject);
        }
    }
    /*this method tracks player and calculates the distance 
     * the enemy moves towards the player
     */
    void EnemyMovement()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
            Distance = (transform.position - player.transform.position).normalized;
            transform.Translate(Distance * speed * Time.deltaTime);
        }
    }

    /*
     * this method is used to apply damage to enemy 
     * but it is called in another scripts
     */
    public void damageEnemy(int damage)
    {
        enemyHealth -= damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            manager.EnemyKilledCounter(1);
            spawner.SpawnEnemy(spawnPos);
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().damageHealth(enemyCollisonDamage);
            spawner.SpawnEnemy(spawnPos);
            manager.EnemyKilledCounter(1);
            Destroy(gameObject);
        }
    }
}
