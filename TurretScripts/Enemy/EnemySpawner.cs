using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public static int WavesLength;
    public static int CountEnemyAlive = 0;
    public float WaveRate;
    public float RateofSP = 0.2f;
    public float RateofIM = 0.5f;
    private Manager vars;

    private int WaveIndex=0;

    private void Awake()
    {
        vars = Manager.GetManagerVars();
        EventCenter.AddListener(EventDefine.StartWave, NextWave);
        WavesLength = waves.Length;
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.StartWave, NextWave);
    }

    private void NextWave()
    {
        if (WaveIndex < WavesLength)
        {
            StartCoroutine("Spawner", WaveIndex);
            WaveIndex++;
        }
        if(WaveIndex == WavesLength)//在最后一波生成后不再使用NextWave按钮
        {
            EventCenter.Broadcast(EventDefine.isNext);
        }
    }
    IEnumerator Spawner(int index)
    {
        for (int i = 0; i < waves[index].IronmanCount; i++)
        {
            Instantiate(vars.IronMan, transform.position, Quaternion.identity);
            CountEnemyAlive++;
            if (i != waves[index].IronmanCount - 1)
                yield return new WaitForSeconds(RateofIM);
        }
        for (int i = 0; i < waves[index].SpiderCount; i++)
        {
            Instantiate(vars.Spider, transform.position, Quaternion.identity);
            CountEnemyAlive++;
            if (i != waves[index].SpiderCount - 1)
                yield return new WaitForSeconds(RateofSP);
        }
        //while (CountEnemyAlive > 0)
        //{
        //    yield return 0;
        //}
        yield return new WaitForSeconds(WaveRate);
    }

    
}
