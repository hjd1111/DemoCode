using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EagleMove : MonoBehaviour
{
    private int speed ;
    private int anglespeed;
    private Vector3 target;

    public int Speed { get => speed; set => speed = value; }
    public int Anglespeed { get => anglespeed; set => anglespeed = value; }
    public Vector3 Target { get => target; set => target = value; }


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        transform.Translate(target*speed*Time.deltaTime);
        transform.Rotate(Vector3.back, anglespeed * Time.deltaTime);
        if (transform.position.y < -100||transform.position.y > 650||transform.position.x>800)
        {
            Destroy(gameObject);
        }
    }
}
