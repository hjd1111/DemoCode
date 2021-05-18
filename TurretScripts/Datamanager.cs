using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Datamanager : MonoBehaviour
{
    public bool isFirstGame;
    public static Datamanager Instance;
    public int[] tempStarsNum;
    public string LevelName;
    public bool isMusic=true;
    private GameData data;

    private void Awake()
    {
        InitGameData();
        //ResetData();
        //Save();
        tempStarsNum = data.GetStarCounts();
        //Debug.Log(tempStarsNum[0]);
        EventCenter.AddListener(EventDefine.Save, Save);
        EventCenter.AddListener<string>(EventDefine.LevelName, SetName);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.Save, Save);
        EventCenter.RemoveListener<string>(EventDefine.LevelName, SetName);
    }

    void Start()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
            //避免场景加载时该对象销毁
            DontDestroyOnLoad(gameObject);
        }
        
    }

    public void SetName(string name)
    {
        LevelName = name;
        SceneManager.LoadScene(2);
    }
    public void SetisMusic(bool value)
    {
        isMusic = value;
        Save();
    }
    public bool GetisMusic()
    {
        return isMusic;
    }

    private void Read()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/Gamedata.data", FileMode.Open))
            {
                if (fs.Length != 0)
                {
                    data = (GameData)bf.Deserialize(fs);
                }
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
            tempStarsNum = new int[10];
            isMusic = true;

            data = new GameData();
            Save();
        }
    }
    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                data.SetStarCounts(tempStarsNum);
                data.SetisFirstGame(isFirstGame);
                data.SetisMusic(isMusic);
                bf.Serialize(fs, data);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void ResetData()
    {
        isFirstGame = true;
        tempStarsNum = new int[10];
        isMusic = true;
        Save();
    }

}
