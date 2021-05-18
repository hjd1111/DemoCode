using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Button Home;
    private Button RePlay;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowPausePanel, Show);
        EventCenter.AddListener(EventDefine.ClosePausePanel, Close);
        Home = transform.Find("Home").GetComponent<Button>();
        RePlay = transform.Find("RePlay").GetComponent<Button>();
        Home.onClick.AddListener(onHomeButtonClick);
        RePlay.onClick.AddListener(onRePlayButtonClick);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowPausePanel, Show);
        EventCenter.AddListener(EventDefine.ClosePausePanel, Close);
    }
    private void onHomeButtonClick()
    {
        Time.timeScale = 1;
        EventCenter.Broadcast(EventDefine.ResetSpeed);
        GameData.isGame = false;
        ControlinPlay.isDead = false;
        Gamemanager.Instance.isHardMode = false;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isReplay = false;
    }
    private void onRePlayButtonClick()
    {
        Time.timeScale = 1;
        GamePanel.ispause = false;
        gameObject.SetActive(false);
        GameData.isGame = true;
        ControlinPlay.isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isReplay = true;
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
