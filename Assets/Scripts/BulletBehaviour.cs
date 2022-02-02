/*This Script is only used for bullet behaviour
 * different bullets giving different damages and applying forces
 * maximum force is used for pistol bullets;  damage is lower
 * medium force is used for assault bullets; damage is medium
 * maimum force is used for rocket bullets; damage is higher
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;

    private float minimumForce = 300;
    private float mediumForce = 500;
    private float maximumForce = 800;
    private GameObject equippedGun;

    // Start is called before the first frame update
    void Start()
    {
        equippedGun = GameObject.Find("Player").GetComponent<PlayerController>().equippedGun;
        transform.Translate(Vector3.forward * bulletSpeed*Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
       if(GameObject.Find("Player") != null)
        equippedGun = GameObject.Find("Player").GetComponent<PlayerController>().equippedGun;
    }

    /**
     * on collision method the it checks the enemy first and the name of the equipped gun to apply the required damage and force;
     * in this place we use the damageEnemy method that we made in EnemyController
     */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && equippedGun.name.StartsWith("Rocket"))
        {
            collision.gameObject.GetComponent<EnemyController>().damageEnemy(120);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * minimumForce *Time.deltaTime, ForceMode.Impulse);
            Destroy(gameObject);
            
        }
        else if (collision.gameObject.CompareTag("Enemy") && equippedGun.name.StartsWith("Assault"))
        {
            collision.gameObject.GetComponent<EnemyController>().damageEnemy(80);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * mediumForce * Time.deltaTime, ForceMode.Impulse);
           Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && equippedGun.name.StartsWith("Pistol"))
        {
            collision.gameObject.GetComponent<EnemyController>().damageEnemy(40);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * maximumForce * Time.deltaTime, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
