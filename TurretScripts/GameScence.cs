using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScence : MonoBehaviour
{
    public string LevelName;
    public static GameScence Instance;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = transform.GetComponent<AudioSource>();
        EventCenter.AddListener<bool>(EventDefine.isMusic, isMusic);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventDefine.isMusic, isMusic);
    }
    private void Start()
    {
        LevelName = Datamanager.Instance.LevelName;
        //Debug.Log(LevelName);
        Instantiate(Resources.Load(LevelName));
        isMusic(!Datamanager.Instance.GetisMusic());
    }
    private void isMusic(bool value)
    {
        audioSource.mute = value;
    }
}
