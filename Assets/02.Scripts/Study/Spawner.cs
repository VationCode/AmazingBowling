using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject target;
    void Start()
    {
       GameObject instanceObj = Instantiate(target, spawnPos.position,spawnPos.rotation);
       
    }

    
    void Update()
    {
        
    }
}
