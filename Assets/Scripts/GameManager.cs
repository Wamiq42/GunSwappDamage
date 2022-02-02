/*This script is used for Tracking the score and time 
 * after sixty seconds the game starts to decide whether player lost or won the game.
 * if player kills more than 15 than player has won, if player kills less than 15 then player loses
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOn = true;

    private int enemyKilled = 0;
    private float timer = 0;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(WinGameOrLose());
    }

    private void Update()
    {
        if (isGameOn && player!=null)
        {
            timer += Time.deltaTime;
            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.RoundToInt(timer % 60);

            Debug.Log("Seconds = " + seconds + " Enemies Killed = " + enemyKilled);
        }
       
    }
    //Decides the game win or lost
    IEnumerator WinGameOrLose()
    {
        yield return new WaitForSeconds(60);
        isGameOn = false;

        if((enemyKilled < 15 || enemyKilled == 0) && player != null)
        {
            Debug.Log("you Lost");
        }
        else if(enemyKilled >= 15 && player !=null)
        {
            Debug.Log("You Won");
        }


    }
    //used to call in another script to find the total enemykilled;
    public void EnemyKilledCounter(int enemyKilled)
    {
        this.enemyKilled += enemyKilled;
    }

   
}
