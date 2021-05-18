using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private bool isFirstGame;
    private int[] StarCounts=new int[10];
    private bool isMusic;

    public bool GetisFirstGame()
    {
        return isFirstGame;
    }

    public int[] GetStarCounts()
    {
        return StarCounts;
    }
    public bool GetisMusic()
    {
        return isMusic;
    }
    public void SetisFirstGame(bool isfirstgame)
    {
        isFirstGame = isfirstgame;
    }
    public void SetStarCounts(int[] NewStarsNum)
    {
        StarCounts = NewStarsNum;
    }
    public void SetisMusic(bool value)
    {
        isMusic=value;
    }
    public void SetStarCounts(int index,int num)
    {
        StarCounts[index] = num;
    }
    public void DeletAll()
    {
        for (int i = 0; i < 10; i++)
        {
            StarCounts[i] = 0;
        }
    }

}
