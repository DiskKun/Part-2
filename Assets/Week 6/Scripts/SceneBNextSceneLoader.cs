using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBNextSceneLoader : MonoBehaviour
{
    SceneLoader sl;
    // Start is called before the first frame update
    void Start()
    {
        sl = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sl.LoadNextScene();
    }
}
