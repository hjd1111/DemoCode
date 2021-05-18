using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public int spawncount = 2;

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("Spawner",2,1.5f);
    }
    private void Spawner()
    {
        if (Gamemanager.Instance.BarrierCount > 3)
        {
            return;
        }
        int ran = Random.Range(0, 6);
        int ran1 = Random.Range(0, 2);
        if (ran == 0)
        {
            SpawnSpine1(ran1);
            Gamemanager.Instance.BarrierCount++;

        }else if (ran == 1)
        {
            SpawnSpine2(ran1);
            Gamemanager.Instance.BarrierCount++;
        }
        else if (ran == 2)
        {
            SpawnSpine3(ran1);
            Gamemanager.Instance.BarrierCount++;
        }
        else if(ran==3)
        {
            SpawnFire(ran1);
            Gamemanager.Instance.BarrierCount++;
        }
    }
    private void isSpawanGem(int ran,Transform go)
    {
        if (ran==0)
        {
            return;
        }
        else
        {
            if (Gamemanager.Instance.GemCount > 2) { Gamemanager.Instance.GemCount = 0; return; }
            SpawnGem(go.transform);
            Gamemanager.Instance.GemCount++;
        }
    }
    private void SpawnSpine1(int ran)
    {
        GameObject go = BarrierPool.Instance.GetSpine1Platform();
        go.transform.position = transform.position;
        isSpawanGem(ran , go.transform);
        go.SetActive(true);
    }
    private void SpawnSpine2(int ran)
    {
        GameObject go = BarrierPool.Instance.GetSpine2Platform();
        go.transform.position = transform.position;
        isSpawanGem(ran, go.transform);
        go.SetActive(true);
    }
    private void SpawnSpine3(int ran)
    {
        GameObject go = BarrierPool.Instance.GetSpine3Platform();
        go.transform.position = transform.position;
        isSpawanGem(ran, go.transform);
        go.SetActive(true);
    }
    private void SpawnFire(int ran)
    {
        GameObject go = BarrierPool.Instance.GetFirePlatform();
        go.transform.position = transform.position;
        isSpawanGem(ran, go.transform);
        go.SetActive(true);
    }
    private void SpawnGem(Transform barrier)
    {
        int ran = Random.Range(0,10);
        GameObject go = BarrierPool.Instance.GetGemPlatform();//为啥直接赋值会有偏差
        var Gempos = barrier.transform.position;
        if (ran < 5)
        {
            Gempos.y += 80f;
        }
        else
        {
            Gempos.y += 160f;
        }
        Gempos.x += 20f;
        go.transform.position = Gempos;
        go.SetActive(true);
    }
}
