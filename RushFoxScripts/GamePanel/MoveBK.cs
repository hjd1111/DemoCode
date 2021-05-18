using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBK : MonoBehaviour
{

    public int speed = 5;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -300)
        {
            transform.position = new Vector3(300, transform.position.y, transform.position.z);
        }
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
