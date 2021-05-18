using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Site : MonoBehaviour
{
    [HideInInspector]
    public GameObject thisTurret;
    public GameObject FadeTurret;
    public TurretData turretdata;

    public int UpGradeLevel = 0;

    public int WhichOne;//0=cannon,1==catapult,2==missile,3==battack

    private Manager vars;
    //private Renderer renderer;

    //private void Start()
    //{
    //    renderer = GetComponent<Renderer>();
    //}
    private void Awake()
    {
        vars = Manager.GetManagerVars();
        EventCenter.AddListener(EventDefine.DestoryFadeTurret, DestoryFade);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.DestoryFadeTurret, DestoryFade);
    }
    private void SetWhich()
    {
        if(thisTurret.GetComponent<TurretAI>() != null && thisTurret.GetComponent<TurretAI>().turretType == TurretAI.TurretType.Cannon ||
           thisTurret.GetComponent<UpTurret>()!=null && thisTurret.GetComponent<UpTurret>().turretType == TurretAI.TurretType.Cannon)
        {
            WhichOne = 0;
        }
        else if (thisTurret.GetComponent<TurretAI>() != null && thisTurret.GetComponent<TurretAI>().turretType == TurretAI.TurretType.Catapult ||
           thisTurret.GetComponent<UpTurret>() != null && thisTurret.GetComponent<UpTurret>().turretType == TurretAI.TurretType.Catapult)
        {
            WhichOne = 1;
        }
        else if(thisTurret.GetComponent<TurretAI>() != null && thisTurret.GetComponent<TurretAI>().turretType == TurretAI.TurretType.MissileSingle ||
           thisTurret.GetComponent<UpTurret>()!=null && thisTurret.GetComponent<UpTurret>().turretType == TurretAI.TurretType.MissileSingle)
        {
            WhichOne = 2;
        }
        else
        {
            WhichOne = 3;
        }
    }
    public void DestoryFade()
    {
        Destroy(FadeTurret);
        if (thisTurret != null)
        {
            thisTurret.SetActive(true);
        }
    }
    public void ShowFateTurret(int i)
    {
        Destroy(FadeTurret);
        Vector3 temp = transform.position;
        temp.y += 2f;
        switch (i)
        {
            case 1:
                FadeTurret = Instantiate(vars.FadeCannon, temp, Quaternion.identity);
                break;
            case 2:
                FadeTurret = Instantiate(vars.FadeCatapult, temp, Quaternion.identity);
                break;
            case 3:
                FadeTurret = Instantiate(vars.FadeMissile, temp, Quaternion.identity);
                break;
            case 4:
                FadeTurret = Instantiate(vars.FadeBattacks, temp, Quaternion.identity);
                break;
            case 5:
                //没做其他塔的最终模型
                SetWhich();
                thisTurret.SetActive(false);
                switch (WhichOne)
                {
                    case 0:
                        if (UpGradeLevel == 0)
                        {
                            FadeTurret = Instantiate(vars.FadeCannonUp, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.UpDescribe[WhichOne];
                        }
                        else if (UpGradeLevel == 1)
                        {
                            FadeTurret = Instantiate(vars.FadeCannonFinal, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.FinalDescribe[WhichOne];
                        }
                        break;
                    case 1:
                        if (UpGradeLevel == 0)
                        {
                            FadeTurret = Instantiate(vars.FadeCatapultUp, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.UpDescribe[WhichOne];
                        }
                        else if (UpGradeLevel == 1)
                        {
                            FadeTurret = Instantiate(vars.FadeCatapultFinal, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.FinalDescribe[WhichOne];
                        }
                        break;
                    case 2:
                        if (UpGradeLevel == 0)
                        {
                            FadeTurret = Instantiate(vars.FadeMissileUp, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.UpDescribe[WhichOne];
                        }
                        else if (UpGradeLevel == 1)
                        {
                            FadeTurret = Instantiate(vars.FadeMissileFinal, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.FinalDescribe[WhichOne];
                        }
                        break;
                    case 3:
                        if (UpGradeLevel == 0)
                        {
                            FadeTurret = Instantiate(vars.FadeBattacksUp, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.UpDescribe[WhichOne];
                        }
                        else if (UpGradeLevel == 1)
                        {
                            FadeTurret = Instantiate(vars.FadeBattacksFinal, temp, Quaternion.identity);
                            Gamemanager.Instance.Up.transform.GetChild(1).GetComponent<Text>().text = vars.FinalDescribe[WhichOne];
                        }
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void BuildTurret(TurretData turretdata,bool isBattacks=false)
    {
        
        Vector3 temp = transform.position;
        temp.y += 2f;
        this.turretdata = turretdata;
        if (isBattacks)
        {
            thisTurret = Instantiate(turretdata.BasePrefab, temp, transform.rotation);
        }
        else
        {
            thisTurret = Instantiate(turretdata.BasePrefab, temp, Quaternion.identity);
        }
        GameObject effect = Instantiate(vars.BuildEffect, temp, Quaternion.identity);
        Gamemanager.Instance.Up.transform.GetChild(0).GetComponent<Text>().text = turretdata.UpgradedCost.ToString();

        DestoryFade();
        
        Destroy(effect, 1);
    }
    
    public void UpGradeTurret()
    {
        Destroy(thisTurret);
        DestoryFade();
        UpGradeLevel++;
        Vector3 temp = transform.position;
        temp.y += 2f;
        if (UpGradeLevel == 1)
        {
            thisTurret = Instantiate(turretdata.UpgradedPrefab, temp, Quaternion.identity);
            GameObject effect = Instantiate(vars.BuildEffect, temp, Quaternion.identity);
            Gamemanager.Instance.Up.image.sprite = vars.TurretFinal[WhichOne];
            EventCenter.Broadcast<int>(EventDefine.ChangeMoney, -turretdata.UpgradedCost);
            Gamemanager.Instance.Up.transform.GetChild(0).GetComponent<Text>().text = (vars.CanonData.UpgradedCost * 1.5).ToString();
            Destroy(effect, 1);
        }
        if (UpGradeLevel == 2)
        {
            thisTurret = Instantiate(turretdata.FinalPrefab, temp, Quaternion.identity);
            GameObject effect = Instantiate(vars.BuildEffect, temp, Quaternion.identity);
            EventCenter.Broadcast<int>(EventDefine.ChangeMoney, -int.Parse((turretdata.UpgradedCost*1.5).ToString()));
            Destroy(effect, 1);
            Gamemanager.Instance.Up.image.sprite = vars.TurretUp[4];
            Gamemanager.Instance.Up.interactable = false;
        }
    }
    public void DestoryTurret()
    {
        Destroy(thisTurret);
        UpGradeLevel = 0;
        turretdata = null;
        thisTurret = null;
        Gamemanager.Instance.Up.interactable = true;
        GameObject effect = Instantiate(vars.BuildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    //private void OnMouseEnter()
    //{
    //    if (thisTurret == null && EventSystem.current.IsPointerOverGameObject() == false)
    //    {
    //        renderer.material.color = Color.red;
    //    }
    //}
    //private void OnMouseExit()
    //{
    //    renderer.material.color = Color.white;
    //}
}
