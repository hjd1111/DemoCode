using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    private Button StartButton;
    private Button SettingButton;
    private Button ExitButton;
    public Button BackButton;
    public Button SoundButton;
    public GameObject SettingCanvas;
    private Manager vars;
    private AudioSource audioSource;
    private void Awake()
    {
        vars = Manager.GetManagerVars();
        audioSource = transform.GetComponent<AudioSource>();
        EventCenter.AddListener<bool>(EventDefine.isMusic, isMusic);
        
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventDefine.isMusic, isMusic);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartButton = transform.Find("StartButton").GetComponent<Button>();
        SettingButton = transform.Find("SettingButton").GetComponent<Button>();
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();

        StartButton.onClick.AddListener(onStartClick);
        SettingButton.onClick.AddListener(onSettingClick);
        ExitButton.onClick.AddListener(onExitClick);
        BackButton.onClick.AddListener(onBackClick);
        SoundButton.onClick.AddListener(onSoundClick);
        SettingCanvas.SetActive(false);
    }
    private void Init()
    {
        isMusic(!Datamanager.Instance.GetisMusic());
        if (Datamanager.Instance.GetisMusic() == false)
        {
            SoundButton.transform.GetComponent<Image>().sprite = vars.SoundOff;
        }
        else
        {
            SoundButton.transform.GetComponent<Image>().sprite = vars.SoundOn;
        }
    }

    private void isMusic(bool value)
    {
        audioSource.mute = value;
    }
    private void onStartClick()
    {
        SceneManager.LoadScene(1);
    }
    private void onSettingClick()
    {
        if (!SettingCanvas.activeInHierarchy)
        {
            SettingCanvas.SetActive(true);
        }
    }
    private void onExitClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    private void onBackClick()
    {
        //Debug.Log("wssb");
        if (SettingCanvas.activeInHierarchy)
        {
            SettingCanvas.SetActive(false);
        }
        Datamanager.Instance.Save();
    }
    private void onSoundClick()
    {

        Datamanager.Instance.SetisMusic(!Datamanager.Instance.GetisMusic());
        if (Datamanager.Instance.GetisMusic() == false)
        {
            SoundButton.transform.GetComponent<Image>().sprite = vars.SoundOff;
        }
        else
        {
            SoundButton.transform.GetComponent<Image>().sprite = vars.SoundOn;
        }
        EventCenter.Broadcast(EventDefine.isMusic, !Datamanager.Instance.GetisMusic());
        Datamanager.Instance.Save();
    }
}
