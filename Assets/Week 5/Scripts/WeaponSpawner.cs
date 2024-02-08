using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weapon;
    
    // Start is called before the first frame update
    public void SpawnArrow()
    {
        Instantiate(weapon, transform.position, transform.rotation);
    }
}
