using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private float timerBullet; //variables for determining how fast the enemy fires
    private float maxTimerBullet;
    public GameObject bullet;

    public float timerMin = 5f;
    public float timerMax = 12f;
    public bool canFireBullets = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed); //for the rigid body, which handles objects physics, set the velocity
        //enemy ship always moves down, not side to side, so the x value is 0, speed is plugged in in the unity editor

        timerBullet = 0;
        maxTimerBullet = Random.Range(timerMin, timerMax);

        if (canFireBullets)
            StartCoroutine("FireBullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
            Destroy(this.gameObject);
    }

    void SpawnBullet()
    {
        Vector3 spawnPoint = transform.position;
        //make bullet look like its firing from the front of the ship
        //take bullets height and divide it by 2, then get size of the enemy and divide its height by 2
        //make the spawn point of that bullet move that combined distance forward so it moves out of the front of the head of the ship and moves ahead of it
        spawnPoint.y -= (bullet.GetComponent<Renderer>().bounds.size.y / 2) + (GetComponent<Renderer>().bounds.size.y / 2);
        GameObject.Instantiate(bullet, spawnPoint, transform.rotation); //there is no rotation because we crated a spawn point, want the enemies and their bullet's to have the same rotation
    }
    IEnumerator FireBullet()
    {
        while (true)
        {
            if (timerBullet >= maxTimerBullet)
            {
                // Spawn a bullet
                SpawnBullet();
                timerBullet = 0;
                maxTimerBullet = Random.Range(timerMin, timerMax);
            }

            timerBullet += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
