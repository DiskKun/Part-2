using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    public float speed = 3;
    Rigidbody2D rb;
    bool clickingOnSelf = false;
    public float gunDist = 1;
    Animator animator;

    public GameObject gun;
    public GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // on click:
        if (Input.GetMouseButtonDown(0) && !clickingOnSelf)
        {
            // set new movement destination
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // use the destination vector to set the position of the gun
            gun.transform.localPosition = destination.normalized;
        }
        // set the velocity float for the animator
        animator.SetFloat("Velocity", movement.magnitude);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // when space pressed, instantiate a new bullet at the position of the gun.
            Instantiate(bullet, new Vector3(transform.position.x + destination.normalized.x, transform.position.y + destination.normalized.y, 0), Quaternion.identity);
        }

    }

    private void OnMouseDown()
    {
        clickingOnSelf = true;
    }

    private void OnMouseUp()
    {
        clickingOnSelf = false;
    }

    private void FixedUpdate()
    {
        // the amount to move by in total
        movement = destination - (Vector2)transform.position;
        if (movement.magnitude < 0.1)
        {
            movement = Vector2.zero;
        }
        // moving in the direction of the target by the speed variable
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
        
    }
}
