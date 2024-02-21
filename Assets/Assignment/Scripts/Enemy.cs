using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody2D prb;
    Rigidbody2D rb;
    Animator animator;
    public float speed = 2;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Centre");
        prb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        // move the enemy towards the player and set appropriate animation parameter
        rb.MovePosition(rb.position + (prb.position - rb.position).normalized * speed * Time.deltaTime);
        animator.SetFloat("Velocity", (prb.position - rb.position).magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.name == "Player")
        {
            // if the colliding object is a player, have the player take damage and destroy the enemy immediately
            collision.gameObject.SendMessage("ChangeHealth", -1);
            Destroy(gameObject);
        } else if (collision.gameObject.name == "Bullet(Clone)")
        {
            // if the colliding object is a bullet, destroy the enemy and the bullet.
            animator.SetBool("Is Dead", true);
            isDead = true;
            Destroy(gameObject, 1);
            Destroy(collision.gameObject);
        }
    }
}
