using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject ca;
    // Start is called before the first frame update
    void Start()
    {
        GameObject ba = Instantiate(ca, transform.position, Quaternion.identity);
    }

}
