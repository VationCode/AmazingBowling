using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCoroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("HellowCoroutine");
        StartCoroutine("HiCSharp");
        Debug.Log("End");
    }

   IEnumerator HellowCoroutine()
    {
        Debug.Log("Hello");
        yield return new WaitForSeconds(3f);
        Debug.Log("HeolloUinty");
    }
    IEnumerator HiCSharp()
    {
        Debug.Log("HI");
        yield return new WaitForSeconds(5f);
        Debug.Log("HICSharp");

    }
}
