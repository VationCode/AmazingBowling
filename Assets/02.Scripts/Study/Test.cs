using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DogÀÇ ÃÑ °³¼ö" + Dog.count);
        Dog.ShowAnimalType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
