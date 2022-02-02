/*This the playerController Script 
 *Used for the Movement of player
 * player equips the different type of guns
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   
    private float horizontalInput;
    private float verticalInput;
    private Vector3 cameraBackVectors = new Vector3(0,2.8f,-4.5f);
    private GameManager manager;
    //private Vector3 cameraRotationOffset = new Vector3(0, 2.8f, -3.5f);

    //gunoffset[0] has rocket launcher position; gunoffset[1]has assualt position; gunoffset[2] has pistol position;
    public GameObject[] bulletPrefab;
    public Vector3[] gunsOffset;  
    public GameObject[] guns;
    public GameObject cameraMove;
    public GameObject equippedGun;
    public float speed;
    public float rotationSpeed;
    public int playerHealth = 100;



    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isGameOn)
        {
            Movement();
            CameraFollowsPlayer();
            EquipGun();
            FireGun();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
    }

    //Player Movement
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);
    }
    //Camera Follows Player
    void CameraFollowsPlayer()
    {
        cameraMove.transform.position = transform.position + cameraBackVectors;
       
    }


    //Player equips gun using this method
    void EquipGun()
    {
        Vector3 spawnPosition;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equippedGun != null)
                Destroy(equippedGun);
            spawnPosition = transform.position + gunsOffset[0];
            equippedGun = Instantiate(guns[0],spawnPosition, transform.rotation, transform);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (equippedGun != null)
                Destroy(equippedGun);
            spawnPosition = transform.position + gunsOffset[1];
            equippedGun = Instantiate(guns[1], spawnPosition,transform.rotation, transform);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (equippedGun != null)
                Destroy(equippedGun);
            spawnPosition = transform.position + gunsOffset[2];
            equippedGun = Instantiate(guns[2], spawnPosition, transform.rotation, transform);
        }
    }

    //Player uses this method to fire
    void FireGun()
    {
        if(Input.GetKeyDown(KeyCode.Space)
            && equippedGun != null 
            &&equippedGun.name.StartsWith("Pistol"))
        {
            Instantiate(bulletPrefab[0], equippedGun.transform.position
                + new Vector3(0, 0, 1)
                , equippedGun.transform.rotation);
        } 
        else if(Input.GetKeyDown(KeyCode.Space)
            && equippedGun != null
            && equippedGun.name.StartsWith("Assault"))
        {
            Instantiate(bulletPrefab[1],equippedGun.transform.position 
                + new Vector3(0, 0, 1),
                  equippedGun.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Space)
            && equippedGun != null
            && equippedGun.name.StartsWith("Rocket"))
        {
            Instantiate(bulletPrefab[2], equippedGun.transform.position
                + new Vector3(0,0,1),
                equippedGun.transform.rotation);
        }
    }

    //this method gets damage here; used in another scirpts to  damage player;
    public void damageHealth(int damage)
    {
        playerHealth -= damage;
    }
}
