using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private int Mindistance;
    [SerializeField] private float Speed;
    [SerializeField] private float Waittime;

    [SerializeField] GameObject player;

    Rigidbody2D rigidbody;

    float startTime;

    public int lives;

    bool waited = false;


    private void Awake()
    {
        player = GameObject.Find("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Waittime > 0)
        {
            Waittime -= Time.deltaTime;
            return;
        }

        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < Mindistance && Waittime <= 0)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, Speed);
            }
        }
    }

    /*public void areaAttack()
    {
        player.GetComponent<PlayerMovement>().attackE
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < GameManager.Instance.reach)
            {
            reducelive();
            }
        
    }*/

    public void reducelive(int damage)
    {
        lives-=damage;
        if(lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.Contains("Player"))
        {
            if (GameManager.Instance.lives == 1)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                GameManager.Instance.lives--;
                GameManager.Instance.updateUI();
            }
        }

    }



    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta) * Speed, Mathf.Sin(theta) * Speed); // Trig is fun
    }
}
