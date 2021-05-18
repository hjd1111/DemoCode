using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public static bool isGame=false;
    public static bool isReplay = false;

    private bool isFirstGame;
    private int DiamondCount;
    private int BestScore;
    private int BestScoreHard;
    private bool LastMode;
    private bool isTwoJump;
    private bool isMoreScore;
    private bool isMoreSpeed;
    private int ScorePrice;
    private int SpeedPrice;
    private bool IsMusicOn;

    public void SetIsFirstGame(bool isfirstgame)
    {
        isFirstGame = isfirstgame;
    }
    public void SetDiamondCount(int diamondcount)
    {
        DiamondCount = diamondcount;
    }
    public void SetBestScore(int bestscore)
    {
        BestScore = bestscore;
    }
    public void SetLastMode(bool lastmode)
    {
        LastMode = lastmode;
    }
    public void SetBestScoreHard(int bestscorehard)
    {
        BestScoreHard = bestscorehard;
    }
    public void SetisTwoJump(bool twojump)
    {
        isTwoJump = twojump;
    }
    public void SetisMoreScore(bool morescore)
    {
        isMoreScore = morescore;
    }
    public void SetisMoreSpeed(bool morespeed)
    {
        isMoreSpeed = morespeed;
    }
    public void SetScorePrice(int scoreprice)
    {
        ScorePrice = scoreprice;
    }
    public void SetSpeedPrice(int speedprice)
    {
        SpeedPrice = speedprice;
    }
    public void SetMusic(bool value)
    {
        IsMusicOn = value;
    }


    public bool GetisFirstGame()
    {
        return isFirstGame;
    }
    public int GetDiamondCount()
    {
        return DiamondCount;
    }
    public int GetBestScore()
    {
        return BestScore;
    }
    public int GetBestScoreHard()
    {
        return BestScoreHard;
    }
    public bool GetLastMode()
    {
        return LastMode;
    }
    public bool GetTwoJump()
    {
        return isTwoJump;
    }
    public bool GetisMoreScore()
    {
        return isMoreScore;
    }
    public bool GetisMoreSpeed()
    {
        return isMoreSpeed;
    }
    public int GetScorePrice()
    {
        return ScorePrice;
    }
    public int GetSpeedPrice()
    {
        return SpeedPrice;
    }
    public bool GetIsMusicOn()
    {
        return IsMusicOn;
    }
}
