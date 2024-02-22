using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    public float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Controller.SetSelectedPlayer(this);
    }

    public void Selected(bool selected)
    {
        if (selected)
        {
            sr.color = Color.green;
        } else if (!selected)
        {
            sr.color = Color.red;
        }
        
    }

    public void Move(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
}
