using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float goalDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Controller.SelectedPlayer != null)
        {
            float magnitude = ((Vector2)Controller.SelectedPlayer.transform.position - (Vector2)transform.position).magnitude;
            if (magnitude < goalDistance + 1)
            {
                rb.MovePosition((Vector2)transform.position + ((Vector2)Controller.SelectedPlayer.transform.position - (Vector2)transform.position).normalized * magnitude / 2);
            } else
            {
                rb.MovePosition((Vector2)transform.position + ((Vector2)Controller.SelectedPlayer.transform.position - (Vector2)transform.position).normalized * goalDistance);

            }
        }
    }
}
