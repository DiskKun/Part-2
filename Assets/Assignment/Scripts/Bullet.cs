using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 direction;
    public float speed = 3;
    GameObject player;
    GameObject gun;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player Centre");
        gun = GameObject.Find("Gun");
        // set the direction the bullet will travel in for the duration of its time on screen to the vector equal to the distance between the gun and the player
        direction = gun.transform.position - player.transform.position;
        
    }

    private void FixedUpdate()
    {
        // move by the normalized direction vector times the speed
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        // destroy the bullet when not being rendered
        Destroy(gameObject);
    }
}
