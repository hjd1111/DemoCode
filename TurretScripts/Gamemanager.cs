using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Gamemanager : MonoBehaviour
{
    public Site SlectedSite;
    private Animator ShowHideBuild;
    private Animator ShowHideUp;


    public GameObject BuildCanvas;
    public GameObject UpCanvas;
    public GameObject Go;
    public MenuCancvas menu;
    private Transform Quad;
    private AudioSource Myaudio;

    


    private MyButton BuildCanon;
    private MyButton BuildCatapult;
    private MyButton BuildMissile;
    private MyButton BuildBarracks;
    private Button Destory;
    public MyButton Up;

    private Text CanonCost;
    private Text CatapultCost;
    private Text MissileCost;
    private Text BarracksCost;
    


    public static Gamemanager Instance;
    private Manager vars;

    private void Start()
    {
        Init();
    }

    private void Awake()
    {
        Instance = this;
        Myaudio = transform.GetComponent<AudioSource>();
        vars = Manager.GetManagerVars();
        EventCenter.AddListener<int>(EventDefine.ShowFateTurret, ShowFateTurret);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.ShowFateTurret, ShowFateTurret);
    }
    private void Update()
    {
        //Debug.Log(CanonData.BasePrefab.name);

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isSite = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Site"));
                if (isSite)
                {
                    Site site = hit.collider.GetComponent<Site>();
                    SlectedSite = site;
                    if (site.thisTurret == null)
                    {
                        if (BuildCanvas.activeInHierarchy)
                        {
                            HideBuild();
                        }
                        Vector3 temp = site.transform.position;
                        temp.y = 3.2f;
                        temp.x -= 1.5f;
                        ShowBuild(temp);

                    }
                    else if (site.thisTurret != null)//已经有建筑了
                    {
                        if (UpCanvas.activeInHierarchy)
                        {
                            HideUp();
                        }
                        Vector3 temp = site.transform.position;
                        temp.y = 3.2f;
                        temp.x -= 1.5f;
                        ShowUp(temp, site.thisTurret);
                        //Debug.Log(site.thisTurret.name);
                    }
                }
                else
                {
                    if (BuildCanvas.activeInHierarchy)
                    {
                        HideBuild();
                    }
                    else if (UpCanvas.activeInHierarchy)
                    {
                        HideUp();
                    }
                }
            }
        }
        if (SlectedSite!=null && SlectedSite.UpGradeLevel == 2)
        {
            Instance.Up.interactable = false;
            Instance.Up.transform.GetChild(0).GetComponent<Text>().text = null;
        }
        else
        {
            Instance.Up.interactable = true;
        }
    }

    private void Init()
    {
        BuildCanon = BuildCanvas.transform.GetChild(0).GetComponent<MyButton>();
        CanonCost = BuildCanon.GetComponentInChildren<Text>();
        BuildCanon.onClick.AddListener(onCanonClick);

        BuildCatapult = BuildCanvas.transform.GetChild(1).GetComponent<MyButton>();
        CatapultCost = BuildCatapult.GetComponentInChildren<Text>();
        BuildCatapult.onClick.AddListener(onCatapultClick);

        BuildMissile = BuildCanvas.transform.GetChild(2).GetComponent<MyButton>();
        MissileCost = BuildMissile.GetComponentInChildren<Text>();
        BuildMissile.onClick.AddListener(onMissileClick);

        BuildBarracks = BuildCanvas.transform.GetChild(3).GetComponent<MyButton>();
        BarracksCost = BuildBarracks.GetComponentInChildren<Text>();
        BuildBarracks.onClick.AddListener(onBarracksClick);

        Up = UpCanvas.transform.GetChild(0).GetComponent<MyButton>();
        Up.onClick.AddListener(OnUpGradeButtonDown);

        Destory = UpCanvas.transform.GetChild(1).GetComponent<Button>();
        Destory.onClick.AddListener(OnDestoryButtonDown);
        

        ShowHideBuild = BuildCanvas.GetComponent<Animator>();
        ShowHideUp = UpCanvas.GetComponent<Animator>();

        Go.SetActive(false);
    }
    private void ShowFateTurret(int i)
    {
        SlectedSite.ShowFateTurret(i);
    }
    
    private void onCanonClick()
    {
        //Debug.Log(SlectedSite.name);
        EventCenter.Broadcast(EventDefine.DestoryFadeTurret);
        if (menu.money >= vars.CanonData.cost)
        {
            SlectedSite.BuildTurret(vars.CanonData);
            HideBuild();
            EventCenter.Broadcast<int>(EventDefine.ChangeMoney, -vars.CanonData.cost);
        }
        else
        {
            //没钱
            EventCenter.Broadcast(EventDefine.NoMoney);
        }

    }
    private void onCatapultClick()
    {
        EventCenter.Broadcast(EventDefine.DestoryFadeTurret);
        if (menu.money >= vars.CatapultData.cost)
        {
            SlectedSite.BuildTurret(vars.CatapultData);
            HideBuild();
            EventCenter.Broadcast<int>(EventDefine.ChangeMoney, -vars.CatapultData.cost);
        }
        else
        {
            //没钱
            EventCenter.Broadcast(EventDefine.NoMoney);
        }
    }
    private void onMissileClick()
    {
        EventCenter.Broadcast(EventDefine.DestoryFadeTurret);
        if (menu.money >= vars.MissileData.cost)
        {
            SlectedSite.BuildTurret(vars.MissileData);
            HideBuild();
            EventCenter.Broadcast<int>(EventDefine.ChangeMoney, -vars.MissileData.cost);
        }
        else
        {
            //没钱
            EventCenter.Broadcast(EventDefine.NoMoney);
        }
    }
    private void onBarracksClick()
    {
        EventCenter.Broadcast(EventDefine.DestoryFadeTurret);
        if (menu.money >= vars.BarracksData.cost)
        {
            SlectedSite.BuildTurret(vars.BarracksData,true);
            HideBuild();
            EventCenter.Broadcast<int>(EventDefine.ChangeMoney, -vars.BarracksData.cost);
        }
        else
        {
            //没钱
            EventCenter.Broadcast(EventDefine.NoMoney);
        }
    }
    public void OnUpGradeButtonDown()
    {
        if (menu.money >= SlectedSite.turretdata.UpgradedCost)
        {
            SlectedSite.UpGradeTurret();
            HideUp();
        }
        else
        {
            //没钱TODO
            EventCenter.Broadcast(EventDefine.NoMoney);
        }
        //Up.interactable = false;
        //可通过升级路线来修改
    }
    public void OnDestoryButtonDown()
    {
        SlectedSite.DestoryTurret();
        HideUp();
        Myaudio.PlayOneShot(vars.catapult);
    }
    

    public void ShowBuild(Vector3 pos)
    {
        HideBuild();
        BuildCanvas.SetActive(false);
        BuildCanvas.SetActive(true);
        BuildCanvas.transform.position = pos;

    }
    public void ShowUp(Vector3 pos, GameObject thisTurret)
    {
        //StopCoroutine(HideUp());
        
        switch (thisTurret.name)
        {
            case "Cannon(Clone)"://可扩展
                Up.image.sprite = vars.TurretUp[0];
                Quad = thisTurret.transform.Find("Quad");
                Quad.gameObject.SetActive(true);
                break;
            case "Catapult(Clone)":
                Up.image.sprite = vars.TurretUp[1];
                Quad = thisTurret.transform.Find("Quad");
                Quad.gameObject.SetActive(true);
                break;
            case "Missile(Clone)":
                Up.image.sprite = vars.TurretUp[2];
                Quad = thisTurret.transform.Find("Quad");
                Quad.gameObject.SetActive(true);
                break;
            case "Barracks(Clone)":
                Up.image.sprite = vars.TurretUp[3];
                Quad = thisTurret.transform.Find("Quad");
                Quad.gameObject.SetActive(true);
                Go.SetActive(true);
                break;

            default:
                break;
        }
        UpCanvas.SetActive(false);
        UpCanvas.SetActive(true);
        UpCanvas.transform.position = pos;
    }



    private void HideBuild()
    {
        BuildCanvas.SetActive(false);
    }
    private void HideUp()
    {
        if(Quad!=null)
        Quad.gameObject.SetActive(false);
        UpCanvas.SetActive(false);
    }

    

}


