using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private GameObject UI;
    private Rigidbody2D body;//the thing that makes physics
    private BoxCollider2D box;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //binds the camera to the player
       // UI.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -11f);
       

        //Checks if you press right or left
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Updates your movementspeed
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        body.velocity = new Vector2(body.velocity.x,verticalInput * speed);

    }    

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Coin"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.coins++;
            GameManager.Instance.updateUI();
            //FindObjectOfType<AudioManager>().Play("pickup");
        }
    }

    
}
