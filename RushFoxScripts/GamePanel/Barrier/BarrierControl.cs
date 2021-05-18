using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    public int speed;
    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.AddSpeed, Addspeed);
        EventCenter.AddListener(EventDefine.ResetSpeed, resetspeed);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.AddSpeed, Addspeed);
        EventCenter.RemoveListener(EventDefine.ResetSpeed, resetspeed);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x < -300)
        {
            transform.position = new Vector3(700, transform.position.y, transform.position.z);
            gameObject.SetActive(false);
            Gamemanager.Instance.DestoryCount++;
        }
        //Debug.Log(speed);
    }
    public  void Addspeed(int value)
    {
        speed += value;
    }
    public void resetspeed()
    {
        if (Gamemanager.Instance.isHardMode)
        {
            speed = 400;
        }
        else
        {
            speed = 200;
        }
    }
    
}
