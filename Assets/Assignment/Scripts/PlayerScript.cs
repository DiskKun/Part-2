using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement;
    Vector2 startPosition;
    public float speed = 3;
    Rigidbody2D rb;
    bool clickingOnSelf = false;
    public float gunDist = 1;
    public float spawnDist = 3;
    public float health = 5;
    float enemyTimer = 0;
    Animator animator;
    bool isDead = false;

    public Slider slider;
    public GameObject gun;
    public GameObject bullet;
    public GameObject enemy;

    public AnimationCurve moveCurve;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        // on click:
        if (Input.GetMouseButtonDown(0) && !clickingOnSelf && !EventSystem.current.IsPointerOverGameObject())
        {
            // set new movement destination
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosition = rb.position;

            // use the destination vector to set the position of the gun
            

        }
        if (movement.normalized.magnitude >= 1)
        {
            gun.transform.localPosition = movement.normalized;

        }
        // set the velocity float for the animator
        animator.SetFloat("Velocity", movement.magnitude);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // when space pressed, instantiate a new bullet at the position of the gun.
            Instantiate(bullet, new Vector3(transform.position.x + destination.normalized.x, transform.position.y + destination.normalized.y, 0), Quaternion.identity);
        }
        enemyTimer += Time.deltaTime;
        if (enemyTimer > 1)
        {
            enemyTimer = 0;
            SpawnEnemy();
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
        if (isDead) return;

        // the amount to move by in total
        movement = destination - (Vector2)transform.position;
        float total = (destination - startPosition).magnitude;
        if (movement.magnitude < 0.1)
        {
            movement = Vector2.zero;
        }

        float interpolation = moveCurve.Evaluate(movement.magnitude / total);



        // moving in the direction of the target by the speed variable
        //rb.MovePosition(rb.position + Vector2.Lerp(rb.position, destination, interpolation) * speed * Time.deltaTime);
        rb.MovePosition((rb.position + movement.normalized * speed * interpolation * 2 * Time.deltaTime));
        
    }

    public void ChangeHealth(float amount)
    {
        
        // change health by amount
        health += amount;
        health = Mathf.Clamp(health, 0, 5);
        // set slider to reflect value
        slider.value = health;
        if (health == 0)
        {
            isDead = true;
            animator.SetBool("Is Dead", true);
        } else
        {
            isDead = false;
            animator.SetBool("Is Dead", false);
        }

    }

    public void SpawnEnemy()
    {
        if (isDead) return;
        // spawn in an enemy at a random location at a radius of spawnDist around the player
        Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0).normalized;

        Instantiate(enemy, new Vector3(spawnPosition.x + rb.position.x, spawnPosition.y + rb.position.y, 0).normalized * spawnDist, Quaternion.identity);
    }

    public void Restart()
    {
        ChangeHealth(5);
    }
}
