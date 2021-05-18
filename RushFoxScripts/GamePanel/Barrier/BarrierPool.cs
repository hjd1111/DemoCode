using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPool : MonoBehaviour
{
    public static BarrierPool Instance;
    private int SpawnCount=4;
   

    public GameObject spine1;
    public GameObject spine2;
    public GameObject spine3;
    public GameObject fire;
    public GameObject gem;

    private List<GameObject> Spine1 = new List<GameObject>();
    private List<GameObject> Spine2 = new List<GameObject>();
    private List<GameObject> Spine3 = new List<GameObject>();
    private List<GameObject> Fire = new List<GameObject>();
    private List<GameObject> Gem = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateObject(spine1, ref Spine1);
        }
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateObject(spine2, ref Spine2);
        }
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateObject(spine3, ref Spine3);
        }
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateObject(fire, ref Fire);
        }
        for (int i = 0; i < SpawnCount; i++)
        {
            InstantiateObject(gem, ref Gem);
        }
    }
    private GameObject InstantiateObject(GameObject prefab, ref List<GameObject> addList)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        addList.Add(go);
        return go;
    }
    public GameObject GetSpine1Platform()
    {
        for (int i = 0; i < Spine1.Count; i++)
        {
            if (Spine1[i].activeInHierarchy == false)
            {
                return Spine1[i];
            }
        }
        return InstantiateObject(spine1, ref Spine1);//因为基本不可能出现
        //组合平台不够用的情况，所以这些语句基本用不上
    }
    public GameObject GetSpine2Platform()
    {
        for (int i = 0; i < Spine2.Count; i++)
        {
            if (Spine2[i].activeInHierarchy == false)
            {
                return Spine2[i];
            }
        }
        return InstantiateObject(spine2, ref Spine2);
    }
    public GameObject GetSpine3Platform()
    {
        for (int i = 0; i < Spine1.Count; i++)
        {
            if (Spine3[i].activeInHierarchy == false)
            {
                return Spine3[i];
            }
        }
        return InstantiateObject(spine3, ref Spine3);
    }
    public GameObject GetFirePlatform()
    {
        for (int i = 0; i < Spine1.Count; i++)
        {
            if (Fire[i].activeInHierarchy == false)
            {
                return Fire[i];
            }
        }
        return InstantiateObject(fire, ref Fire);
    }
    public GameObject GetGemPlatform()
    {
        for (int i = 0; i < Spine1.Count; i++)
        {
            if (Gem[i].activeInHierarchy == false)
            {
                return Gem[i];
            }
        }
        return InstantiateObject(gem, ref Gem);
    }
}
