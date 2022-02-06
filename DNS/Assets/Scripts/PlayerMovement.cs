using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Camera Cam;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform Firepoint;
    private Rigidbody2D body;//the thing that makes physics
    private BoxCollider2D box;

    public float maxdistance;// the distance the player can attack
    public float bulletspeed =20f;

    bool attackE;
    bool oldStateE;

    bool attackR;
    bool oldStateR;

    string lastdir ="r";

    Vector2 mousePos;

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

        //updates rotation of player
        mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - body.position;
        float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg-90f;

        body.rotation = angle;
        

        attackE = Input.GetKey(KeyCode.E);
        if(attackE != oldStateE)
        {
            if(attackE)
            {
                areaAttack();
                Debug.Log("Attack tried");
            }
            oldStateE = attackE;
        }

        attackR = Input.GetKey(KeyCode.Space);
        if (attackR != oldStateR)
        {
            if (attackR)
            {
                pewPew();
            
                Debug.Log("Pew Pew :)");
            }
            oldStateR = attackR;
        }

    } 
    

    public void pewPew()
    {
        Transform shot = Instantiate(Bullet.transform, new Vector3(Firepoint.position.x, Firepoint.position.y), Firepoint.rotation);
        Rigidbody2D rigbul = shot.gameObject.GetComponent<Rigidbody2D>();
        rigbul.AddForce(Firepoint.up * bulletspeed, ForceMode2D.Impulse);
    }

    public void areaAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");  //returns all enemy currently loaded
        foreach(GameObject e in enemies)
        {
            float distance = Vector3.Distance(this.transform.position, e.transform.position);
            if(distance < maxdistance)
            {
                e.GetComponent<enemy>().reducelive(GameManager.Instance.strength);
            }
        }
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
