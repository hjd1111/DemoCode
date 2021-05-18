using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public static bool ispause = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        EventCenter.AddListener(EventDefine.ShowGamePanel, Show);
        EventCenter.AddListener<bool>(EventDefine.IsMusicOn, isMusicOn);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGamePanel, Show);
        EventCenter.RemoveListener<bool>(EventDefine.IsMusicOn, isMusicOn);
    }
    private void Update()
    {
        IsPause();
    }
    private void IsPause()
    {
        if (!ControlinPlay.isDead && ispause == false && Input.GetKeyDown(KeyCode.F))
        {
            Gamemanager.Instance.SetDiamondCount(Gamemanager.Instance.NewDiamondCount);
            EventCenter.Broadcast(EventDefine.ShowPausePanel);
            Time.timeScale = 0;
            GameData.isGame = false;
            ispause = true;
        }
        else if (ispause && Input.GetKeyDown(KeyCode.F))
        {
            EventCenter.Broadcast(EventDefine.ClosePausePanel);
            GameData.isGame = true;
            Time.timeScale = 1;
            ispause = false;
        }
    }
    private void isMusicOn(bool value)
    {
        audioSource.mute = value;
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}   

    


