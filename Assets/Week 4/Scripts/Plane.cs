using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPositionThreshold = 0.2f;
    Vector2 lastPosition;
    LineRenderer lineRenderer;
    Vector2 currentPosition;
    Rigidbody2D rb;
    public float speed = 1;
    public AnimationCurve landing;
    public float landingTimer;
    public List<Sprite> sprites;
    SpriteRenderer sr;
    public Color red;
    public Color white;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        speed = Random.Range(1, 3);
        
        sr.sprite = sprites[Random.Range(0, 4)];
        transform.localScale = new Vector3(5, 5, 1);

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.color = white;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        sr.color = red;
        float dist = Vector3.Distance(collision.transform.position, transform.position);
        if (dist < 1)
        {
            Destroy(gameObject);
            Destroy(collision);
        }
    }
    


    private void FixedUpdate()
    {
        currentPosition = transform.position;
        if (points.Count > 0)
        {
            Vector2 direction = points[0] - currentPosition;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            rb.rotation = -angle;
        }
        rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.deltaTime);
        
    }
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            landingTimer += 0.5f * Time.deltaTime;
            float interpolation = landing.Evaluate(landingTimer);
            if (transform.localScale.z < 0.1)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, interpolation);
            }
        }

        lineRenderer.SetPosition(0, currentPosition);

        if (points.Count > 0)
        {
            if (Vector2.Distance(currentPosition, points[0]) < newPositionThreshold)
            {
                points.RemoveAt(0);

                for (int i = 0; i < lineRenderer.positionCount - 2; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i + 1));
                }
                lineRenderer.positionCount--;
            }
        }
    }

    private void OnMouseDown()
    {
        points = new List<Vector2>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(lastPosition, newPosition) > newPositionThreshold)
        {
            points.Add(newPosition);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            lastPosition = newPosition;
        }
    }
}
