using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    public GameObject eagles;
    public Transform StartPos;
    public GameObject Player;
    public bool isHard;

    public float WaveTime;
    public float CreatWaitTime;
    private int speed=600;
    

    private void Start()
    {
        InvokeRepeating("MakeEagles", 3, WaveTime);
    }
    private void MakeEagles()
    {
        if (isHard && !Gamemanager.Instance.isHardMode) return;
        int num = Random.Range(1, 5);
        int ang;
        if (isHard)
        {
            ang = Random.Range(10, 30);
        }
        else
        {
            ang = Random.Range(-30, -10);
        }
        if (!ControlinPlay.isDead&&GameData.isGame)
        {
            StartCoroutine(EaglesMove(StartPos, num, ang));
        }
    }
    IEnumerator EaglesMove(Transform start,int num,int angspeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject eagle = Instantiate(eagles, start);
            if (isHard)
            {
                eagle.transform.localScale=new Vector3(-1.5f, 1.5f, 1) ;
            }
            Vector3 target = (Player.transform.position - eagle.transform.position).normalized;
            eagle.GetComponent<EagleMove>().Target = target;
            eagle.GetComponent<EagleMove>().Speed = speed;
            eagle.GetComponent<EagleMove>().Anglespeed = angspeed;
            yield return new WaitForSeconds(CreatWaitTime);
        }
    }
}
