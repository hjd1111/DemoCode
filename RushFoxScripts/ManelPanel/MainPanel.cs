using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainPanel : MonoBehaviour
{
    private Transform target;
    public Text DiamondText;
    public GameObject Teach;
    private bool isShop=false;
    private bool isReset=false;
    private bool isMenu = false;
    public GameObject Shop;
    public GameObject Reset;
    private AudioSource audioSource;
    // Update is called once per frame

    private void Awake()
    {
        ShowTeach();
        EventCenter.AddListener(EventDefine.ShowMainPanel, Show);
        EventCenter.AddListener<bool>(EventDefine.IsMusicOn, isMusicOn);
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = !Gamemanager.Instance.GetIsMusicOn();
        Shop.SetActive(false);
        Reset.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainPanel, Show);
        EventCenter.RemoveListener<bool>(EventDefine.IsMusicOn, isMusicOn);
    }
    private void Start()
    {
        if (GameData.isReplay)
        {
            EventCenter.Broadcast(EventDefine.ShowGamePanel);
            EventCenter.Broadcast(EventDefine.ResetSpeed);
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        DiamondText.text = Gamemanager.Instance.GetTotalDiamondCount().ToString();

        IsReset();
        IsShop();
        IsNormalGame();
        IsMenu();
        
        if (target.position.x < -42)
        {
            PlayControl.isMove = false;
        }
        else
        {
            PlayControl.isMove = true;
        }
        

        IsHardGame();

    }
    private void isMusicOn(bool value)
    {
        audioSource.mute = value;
    }
    private void ShowTeach()
    {
        Teach.SetActive(true);
        StartCoroutine("DestoryTeach");
    }
    IEnumerator DestoryTeach()
    {
        yield return new WaitForSeconds(5f);
        Teach.SetActive(false);
    }

    private void IsNormalGame()
    {
        if (target.position.x > 600)
        {
            Gamemanager.Instance.isHardMode = false;
            EventCenter.Broadcast(EventDefine.ShowGamePanel);
            GameData.isGame = true;
            gameObject.SetActive(false);
            Gamemanager.Instance.Save();
        }

    }
    private void IsHardGame()
    {
        if (target.position.y > 500)
        {
            Gamemanager.Instance.isHardMode = true;
            EventCenter.Broadcast(EventDefine.ShowGamePanel);
            EventCenter.Broadcast(EventDefine.ResetSpeed);
            GameData.isGame = true;
            gameObject.SetActive(false);
            Gamemanager.Instance.Save();
        }
    }
    private void IsReset()
    {
        if (target.position.x > 275 && target.position.x < 300)
        {
            Reset.SetActive(true);
            if (!isReset && Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                EventCenter.Broadcast(EventDefine.ShowResetPanel);
                isReset = true;
            }
            else if (isReset && Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 1;
                EventCenter.Broadcast(EventDefine.CloseResetPanel);
                isReset = false;
            }
        }
        else
        {
            Reset.SetActive(false);
        }
        
    }
    private void IsShop()
    {
        if (target.position.x > 25 && target.position.x < 50)
        {
            Shop.SetActive(true);
            if (!isShop && Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                EventCenter.Broadcast(EventDefine.ShowShopPanel);
                isShop = true;
            }
            else if (isShop && Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 1;
                EventCenter.Broadcast(EventDefine.CloseShopPanel);
                isShop = false;
            }
        }
        else
        {
            Shop.SetActive(false);
        }
        
    }
    private void IsMenu()
    {
        if (!isMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            EventCenter.Broadcast(EventDefine.ShowMenu);
            Time.timeScale = 0;
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
