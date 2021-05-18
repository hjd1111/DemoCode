using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    private Button TwoJump;
    private Button MoreScore;
    private Button MoreSpeed;
    private Button Back;

    public Hint hint;

    private static float ScoreTimes=1f;
    private static float SpeedTimes=1f;


    

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel, Show);
        EventCenter.AddListener(EventDefine.CloseShopPanel, Close);
        TwoJump = transform.Find("TwoJump/Diamond").GetComponent<Button>();
        MoreScore = transform.Find("MoreScore/Diamond").GetComponent<Button>();
        MoreSpeed = transform.Find("MoreSpeed/Diamond").GetComponent<Button>();
        Back = transform.Find("Back").GetComponent<Button>();

        TwoJump.onClick.AddListener(onTwoJumpClick);
        MoreScore.onClick.AddListener(onMoreScoreClick);
        MoreSpeed.onClick.AddListener(onMoreSpeedClick);
        Back.onClick.AddListener(onBackClick);
        
        if (Gamemanager.Instance.isTwoJump) TwoJump.interactable = false;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel, Show);
    }
    private void Update()
    {
        MoreScore.GetComponentInChildren<Text>().text = Gamemanager.Instance.ScorePrice.ToString();
        MoreSpeed.GetComponentInChildren<Text>().text = Gamemanager.Instance.SpeedPrice.ToString();
    }
    private void onBackClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Gamemanager.Instance.Init();
    }
    private void onTwoJumpClick()
    {
        int price = int.Parse(TwoJump.GetComponentInChildren<Text>().text);
        if (Gamemanager.Instance.GetTotalDiamondCount()>=price)
        {
            Gamemanager.Instance.isTwoJump = true;
            Gamemanager.Instance.SetDiamondCount(-price);
            TwoJump.interactable = false;
            Gamemanager.Instance.Save();
        }
        else
        {
            hint.Show();
        }
    }
    private void onMoreScoreClick()
    {
        int price = Gamemanager.Instance.ScorePrice;
        if (Gamemanager.Instance.GetTotalDiamondCount() >= price)
        {
            Gamemanager.Instance.isMoreScore = true;
            Gamemanager.Instance.SetDiamondCount(-price);
            price *= 2;
            Gamemanager.Instance.ScorePrice = price;
            MoreScore.GetComponentInChildren<Text>().text = price .ToString();
            ScoreTimes += 0.1f;
            Gamemanager.Instance.Save();
        }
        else
        {
            EventCenter.Broadcast(EventDefine.Hint);
        }
    }
    private void onMoreSpeedClick()
    {
        int price = Gamemanager.Instance.SpeedPrice;
        if (Gamemanager.Instance.GetTotalDiamondCount() >= price)
        {
            Gamemanager.Instance.isMoreSpeed = true;
            Gamemanager.Instance.SetDiamondCount(-price);
            price *= 2;
            Gamemanager.Instance.SpeedPrice = price;
            MoreSpeed.GetComponentInChildren<Text>().text = price.ToString();
            SpeedTimes += 0.1f;
            Gamemanager.Instance.Save();
        }
        else
        {
            EventCenter.Broadcast(EventDefine.Hint);
        }
    }


    public static float GetScoreTimes()
    {
        return ScoreTimes;
    }
    public static float GetSpeedTimes()
    {
        return SpeedTimes;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Close()
    {
        gameObject.SetActive(false);
    }
}
