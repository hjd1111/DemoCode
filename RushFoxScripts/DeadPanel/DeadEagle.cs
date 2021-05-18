using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadEagle : MonoBehaviour
{
    private int speed=80;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 1, 0) * speed * Time.deltaTime);
        transform.GetComponent <Image>().sprite= transform.GetComponent<SpriteRenderer>().sprite;
        if (transform.position.y > 500)
        {
            gameObject.SetActive(false);
        }
    }
}
