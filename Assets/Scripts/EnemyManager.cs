using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    public GameObject enemy;

    public float timerMin = 5f;
    public float timerMax = 12f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        maxTimer = Random.Range(timerMin, timerMax); //max timer is set to random value between 5 and 12 seconds
        StartCoroutine("SpawnEnemyTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float y = 1.25f; //spawns them somewhere just above camera so enemies dont just pop into existence
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), y, 0)); //assigns spawn point range x is between 0 and one, y is already established at 1.25, z is 0 because its 2d
        //viewport to world point is conversion method for the positions of things like the camera and the player
        spawnPoint.z = 0;

        //Adjust x axis position so enemies are not partially offscreen
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        Vector3 enemySize = enemy.GetComponent<Renderer>().bounds.size; //gets enemy width and height
        spawnPoint.x = Mathf.Clamp(spawnPoint.x, leftBorder + enemySize.x / 2, rightBorder - enemySize.x / 2);

        GameObject.Instantiate(enemy, spawnPoint, new Quaternion(0, 0, 0, 0)); //quaternion has to do with rotation, setting the values to 0 means the object will not rotate

    }
   
    IEnumerator SpawnEnemyTimer()
    {
        while (true)
        {
            if (timer >= maxTimer)
            {
                //if timer is the same as or great than max timer
                //Spawn an enemy, reset timer and maxTimer values
                SpawnEnemy();
                timer = 0;
                maxTimer = Random.Range(timerMin, timerMax);
            }

            timer += 0.1f;
            yield return new WaitForSeconds(0.1f); //the yield has to do with the fact that this is a CoRoutine, which runs on its own as the rest of the program is going
            //unlike other methods, which must be fully executed before the rest of the code continues
            //pauses for .1 while timer goes up by .1
        }
    }
}
