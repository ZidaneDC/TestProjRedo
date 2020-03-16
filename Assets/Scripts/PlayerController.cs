using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //allows to access players physics 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundMovement();
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //returns true the moment the user presses down on the chosen button
        {
            GameObject.Instantiate(bullet, transform.position, transform.rotation); //if button is pressed create an instance of the bullet object at the player position
        }
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); //these are 0 or 1 values, yes or no, 0 not moving and not pressing a button, 1 is pressing WASD and moving
        float y = Input.GetAxisRaw("Vertical");

        float moveX = x * speed;
        float moveY = y * speed;

        rb.velocity = new Vector2(moveX, moveY); //speed is applied as a velocity to their physics
    }
    void BoundMovement()
    {
        float dist = (this.transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        Vector3 playerSize = GetComponent<Renderer>().bounds.size;

        this.transform.position = new Vector3(
        Mathf.Clamp(this.transform.position.x, leftBorder + playerSize.x / 2, rightBorder - playerSize.x / 2),
        Mathf.Clamp(this.transform.position.y, topBorder + playerSize.y / 2, bottomBorder - playerSize.y / 2),
        this.transform.position.z
        );
    }
}
