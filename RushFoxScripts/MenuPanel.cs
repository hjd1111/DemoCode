using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public Button Continue;
    public Button Exit;
    public Button Sound;
    private ManagerVars vars;
    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.ShowMenu, Show);
        Continue.onClick.AddListener(ContinueGame);
        Exit.onClick.AddListener(ExitGame);
        Sound.onClick.AddListener(OnSoundClick);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMenu, Show);
    }
    private void Show()
    {
        if (Gamemanager.Instance.GetIsMusicOn() == false)
        {
            Sound.transform.GetComponent<Image>().sprite = vars.musicOff;
        }
        else
        {
            Sound.transform.GetComponent<Image>().sprite = vars.musicOn;
        }
        gameObject.SetActive(true);
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        
    }
    private void OnSoundClick()
    {
        //Debug.Log(Gamemanager.Instance.GetIsMusicOn());
        Gamemanager.Instance.SetMusic(!Gamemanager.Instance.GetIsMusicOn());
        //Debug.Log(Gamemanager.Instance.GetIsMusicOn());
        SetSound();
    }
    private void SetSound()
    {
        if (Gamemanager.Instance.GetIsMusicOn() == false)
        {
            Sound.transform.GetComponent<Image>().sprite = vars.musicOff;
        }
        else
        {
            Sound.transform.GetComponent<Image>().sprite = vars.musicOn;
        }
        EventCenter.Broadcast(EventDefine.IsMusicOn, !Gamemanager.Instance.GetIsMusicOn());
        Gamemanager.Instance.Save();

    }
    private void ExitGame()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
