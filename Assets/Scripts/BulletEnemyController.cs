using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed); //x is 0 because bullet moves in one direction, speed is speed you enter in the unity editor
    }

    // Update is called once per frame
    void Update()
    {
        //check if bullet has gone beyond screen bounds, if it has, destroy it to save space
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0) 
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this method tells a on object that has collision, once it collides with anothe robject, to take in the objects collision information
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(this.gameObject); //destroys itself it collides with an enemy
            GameObject.Destroy(collision.gameObject); //destroys whats attached to what the bullet collides with
        }
    }
}
