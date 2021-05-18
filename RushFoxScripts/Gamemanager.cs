using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    private GameData data;
    public int BarrierCount=0;
    public int DestoryCount = 0;
    public int GemCount = 0;
    public int LevelScore;

    public int Score = 0;
    public int BestScore=0;
    public int BestScoreHard = 0;
    public int NewDiamondCount=0;
    public int TotalDiamond = 0;
    

    public GameObject PalyerINGame;
    public GameObject PlayerINMain;
    public GameObject HardSpawner;

    public Text Diamondtxt;
    public Text Scoretxt;
    


    public bool isFirstGame;
    public bool isHardMode;
    public bool isTwoJump;
    public bool isMoreScore;
    public bool isMoreSpeed;
    public bool isMusicOn=true;
    public int ScorePrice;
    public int SpeedPrice;


    private void Awake()
    {
        Instance = this;
        InitGameData();
        Init();
        InvokeRepeating("UpdateScore", 0f, 0.1f);
        EventCenter.AddListener<int>(EventDefine.EatDiamond, UpdateDiamond);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.EatDiamond, UpdateDiamond);
    }
    // Update is called once per frame
    void Update()
    {
        //IsGame();//切换场景，因为没有使用广播所以比较繁琐//后切换为广播
        //IsDead();
        //IsPause();
        AddBarrierSpeed();
        if (DestoryCount > 1)//限制同一时间拥有的障碍物数量
        {
            BarrierCount = 0;
        }
        if (isHardMode)
        {
            HardSpawner.SetActive(true);
        }
    }
    public void Init()
    {
        if (isMoreSpeed)
        {
            PlayControl.SetSpeed(ShopPanel.GetSpeedTimes());
            ControlinPlay.SetSpeed(ShopPanel.GetSpeedTimes());
        }
    }
    /*private void IsGame()
    {
        if (GameData.isGameStart)
        {
            MainPanel.SetActive(false);
            GamePanel.SetActive(true);
            ScorePanel.SetActive(true);
            isMainPanel = false;
        }
        else
        {
            GamePanel.SetActive(false);
            MainPanel.SetActive(true);
            ScorePanel.SetActive(false);
            isMainPanel = true;
        }
        
    }*/
    /*private void IsDead()
    {
        if (isDead == true)
        {
            SetDiamondCount(NewDiamondCount);
            if (isHardMode)
            {
                SetBestScoreHard(Score);
                if (GetBestScoreHard() <= GetScore())
                {
                    New.SetActive(true);
                    txt_BestScore.text = "最高分：" + GetScore().ToString();
                }
                else
                {
                    New.SetActive(false);
                    txt_BestScore.text = "最高分：" + GetBestScoreHard().ToString();
                }
            }
            else
            {
                SetBestScore(Score);
                if (GetBestScore() <= GetScore())
                {
                    New.SetActive(true);
                    txt_BestScore.text = "最高分：" + GetScore().ToString();
                }
                else
                {
                    New.SetActive(false);
                    txt_BestScore.text = "最高分：" + GetBestScore().ToString();
                }
            }
            if (isMoreScore)
            {
                txt_Score.text = GetScore().ToString() +"*"+ ShopPanel.GetScoreTimes().ToString()
                    +"="+ ((int)(GetScore()*ShopPanel.GetScoreTimes())).ToString();
            }
            else
            {
                txt_Score.text = GetScore().ToString();
            }
            txt_NewAddDiamond.text = "+" + NewDiamondCount.ToString();
            EagleSpwaner.SetActive(false);
            //NewDiamondCount = 0;
            ScorePanel.SetActive(false);
            isDead = false;
        }
    }*/
    /*private void IsPause()
    {
        if (isPause == false && Input.GetKeyDown(KeyCode.F))
        {
            SetDiamondCount(NewDiamondCount);
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
        }
        else if (isPause && Input.GetKeyDown(KeyCode.F))
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }
    }*/
    
    private void UpdateScore()
    {
        if (GameData.isGame)
        {
            if (isHardMode)
            {
                Score += 2;
            }
            else
            {
                Score++;
            }
            Scoretxt.text = Score.ToString();
        }
    }
    private void UpdateDiamond(int value)
    {
        NewDiamondCount += value;
        Diamondtxt.text = NewDiamondCount.ToString();
    }


    //SET
    public void SetDiamondCount(int value)
    {
        TotalDiamond += value;
        Save();
    }
    public void SetBestScore(int value)
    {
        if (value > BestScore)
        {
            BestScore = value;
        }
        Save();
    }
    public void SetBestScoreHard(int value)
    {
        if (value > BestScoreHard)
        {
            BestScoreHard = value;
        }
        Save();
    }
    public void AddBarrierSpeed()
    {
        if (GetScore()!=0 && GetScore() % LevelScore == 0)
        {
            EventCenter.Broadcast<int>(EventDefine.AddSpeed,50);
            LevelScore +=1500;
        }
    }
    public void SetMusic(bool value)
    {
        isMusicOn = value;
        Save();
    }

    //GET
    public int GetTotalDiamondCount()
    {
        return TotalDiamond;
    }
    public int GetBestScore()
    {        return BestScore;
    }
    public int GetScore()
    {
        return Score;
    }
    public int GetBestScoreHard()
    {
        return BestScoreHard;
    }
    public bool GetIsMusicOn()
    {
        return isMusicOn;
    }

    //数据处理
    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                data.SetBestScore(BestScore);
                data.SetDiamondCount(TotalDiamond);
                data.SetIsFirstGame(isFirstGame);
                data.SetLastMode(isHardMode);
                data.SetBestScoreHard(BestScoreHard);
                data.SetisTwoJump(isTwoJump);
                data.SetisMoreScore(isMoreScore);
                data.SetisMoreSpeed(isMoreSpeed);
                data.SetScorePrice(ScorePrice);
                data.SetSpeedPrice(SpeedPrice);
                data.SetMusic(isMusicOn);


                bf.Serialize(fs, data);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void Read()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData.data", FileMode.Open))
            {
                data = (GameData)bf.Deserialize(fs);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void InitGameData()
    {
        Read();
        if (data != null)
        {
            isFirstGame = data.GetisFirstGame();
        }
        else
        {
            isFirstGame = true;
        }
        if (isFirstGame)
        {
            isFirstGame = false;
            TotalDiamond = 0;
            BestScore = 0;
            isHardMode = false;
            BestScoreHard = 0;
            isTwoJump = false;
            isMoreScore = false;
            isMoreSpeed = false;
            ScorePrice = 20;
            SpeedPrice = 20;
            isMusicOn = true;

            data = new GameData();
            Save();
        }
        else
        {
            TotalDiamond = data.GetDiamondCount();
            BestScore = data.GetBestScore();
            isHardMode = data.GetLastMode(); ;
            BestScoreHard = data.GetBestScoreHard();
            isTwoJump = data.GetTwoJump();
            isMoreScore = data.GetisMoreScore();
            isMoreSpeed = data.GetisMoreSpeed();
            ScorePrice = data.GetScorePrice();
            SpeedPrice = data.GetSpeedPrice();
            isMusicOn = data.GetIsMusicOn();
        }
    }
    public void ResetData()
    {
        isFirstGame = false;
        TotalDiamond = 0;
        BestScore = 0;
        isHardMode = false;
        BestScoreHard = 0;
        isTwoJump = false;
        isMoreScore = false;
        isMoreSpeed = false;
        isMusicOn = true;
        ScorePrice = 20;
        SpeedPrice = 20;
        Save();

    }
}
