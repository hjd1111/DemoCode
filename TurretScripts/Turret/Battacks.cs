using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battacks : MonoBehaviour
{
    public GameObject Solider;
    public Transform[] SoliderPos;
    public float MoveDis=20f;
    private Transform RootPos;
    private GameObject NowSolider;
    public int SoldierNum=3;
    public bool[] isSoldierAlive;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        isSoldierAlive[0] = false;
        isSoldierAlive[1] = false;
        isSoldierAlive[2] = false;
        InvokeRepeating("CheckAlive", 0, 25f);
        RootPos = transform.Find("SoliderPos").GetComponent<Transform>();
    }
    private void Awake()
    {
        isSoldierAlive = new bool[SoldierNum];
        EventCenter.AddListener<Vector3>(EventDefine.MoveTarget, MoveTarget);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector3>(EventDefine.MoveTarget, MoveTarget);
    }
    private void MoveTarget(Vector3 target)
    {
        //Debug.Log("wssb");
        if (Vector3.Distance(transform.position, target) < MoveDis)
        {
            Gamemanager.Instance.SlectedSite.thisTurret.GetComponent<Battacks>().RootPos.position = target;
        }
    }
    private void CheckAlive()
    {
        Vector3 temp = transform.position;
        temp.y = 1;
        if (!isSoldierAlive[0])
        {
            GameObject soliders = Instantiate(Solider, temp, Quaternion.identity);
            soliders.transform.parent = transform;
            soliders.GetComponent<Soldier>().pos = SoliderPos[0];
            isSoldierAlive[0] = true;
        }
        if (!isSoldierAlive[1])
        {
            GameObject soliders = Instantiate(Solider, temp, Quaternion.identity);
            soliders.transform.parent = transform;
            soliders.GetComponent<Soldier>().pos = SoliderPos[1];
            isSoldierAlive[1] = true;
        }
        if (!isSoldierAlive[2])
        {
            GameObject soliders = Instantiate(Solider, temp, Quaternion.identity);
            soliders.transform.parent = transform;
            soliders.GetComponent<Soldier>().pos = SoliderPos[2];
            isSoldierAlive[2] = true;
        }

    }
}
