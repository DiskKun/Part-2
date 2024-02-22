using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Selected(true);
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
}
