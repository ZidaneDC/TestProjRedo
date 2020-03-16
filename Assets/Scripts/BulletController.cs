using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed); //x is 0 because bullet moves in one direction, speed is speed you enter in the unity editor
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>(); //looks for the Score text name and gets the text component
    }

    // Update is called once per frame
    void Update()
    {
        //check if bullet has gone beyond screen bounds, if it has, destroy it to save space
        if (Camera.main.WorldToViewportPoint(transform.position).y > 1)
        {
            //unity knows that if you only have one camera, that is your main camera
            //world to view point = what the camera sees, basically this is converting bullets position to position to camera, and if its too far, delete it
            //if a players bullet goes offscreen, before the bullet is deleted, they lose five points
            scoreText.GetComponent<ScoreController>().score -= 5;
            scoreText.GetComponent<ScoreController>().UpdateScore();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this method tells a on object that has collision, once it collides with anothe robject, to take in the objects collision information
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject.Destroy(this.gameObject); //destroys itself it collides with an enemy
            GameObject.Destroy(collision.gameObject); //destroys whats attached to what the bullet collides with
            scoreText.GetComponent<ScoreController>().score += 10;
            scoreText.GetComponent<ScoreController>().UpdateScore();
        }
    }
}
