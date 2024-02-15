using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed);
    }
}
