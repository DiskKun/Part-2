using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    public float speed = 3;
    Rigidbody2D rb;
    Animator animator;
    bool clickingOnSelf = false;
    public float health;
    public float maxHealth = 5;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("Health"))
        {
            health = PlayerPrefs.GetFloat("Health");
            SendMessage("SetHealth", health);
        } else
        {
            health = maxHealth;
        }
        PlayerPrefs.SetFloat("Health", health);
        TakeDamage(0);

    }

    private void FixedUpdate()
    {
        if (isDead) return;
        movement = destination - (Vector2)transform.position;
        if (movement.magnitude < 0.1)
        {
            movement = Vector2.zero;
        }
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        if (Input.GetMouseButtonDown(0) && !clickingOnSelf && !EventSystem.current.IsPointerOverGameObject())
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack");
        }
        animator.SetFloat("Movement", movement.magnitude);
    }

    private void OnMouseDown()
    {
        if (isDead) return;
        clickingOnSelf = true;
        SendMessage("TakeDamage", 1);
    }
    

    private void OnMouseUp()
    {
        clickingOnSelf = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        PlayerPrefs.SetFloat("Health", health);
        if (health == 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
        } else
        {
            isDead = false;
            if (damage != 0)
            {
                animator.SetTrigger("TakeDamage");
            }
            
        }
    }
}
