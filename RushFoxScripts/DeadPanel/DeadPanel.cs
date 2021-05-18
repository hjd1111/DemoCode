using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadPanel : MonoBehaviour
{
    private Button Home;
    private Button RePlay;
    public Text txt_Score, txt_BestScore, txt_NewAddDiamond;
    public GameObject New;
    private AudioSource Myaudio;
    private ManagerVars vars;

    private void Awake()
    {
        Init();
        vars = ManagerVars.GetManagerVars();
        Myaudio = transform.GetComponent<AudioSource>();
        EventCenter.AddListener(EventDefine.ShowDeadPanel, Show);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowDeadPanel, Show);
    }

    private void Init()
    {
        Home = transform.Find("Home").GetComponent<Button>();
        RePlay = transform.Find("RePlay").GetComponent<Button>();
        Home.onClick.AddListener(onHomeButtonClick);
        RePlay.onClick.AddListener(onRePlayButtonClick);
        gameObject.SetActive(false);
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
        GameData.isGame = true;
        ControlinPlay.isDead = false;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isReplay = true;
    }
    private void Show()
    {
        gameObject.SetActive(true);
        Myaudio.PlayOneShot(vars.DeadClip);
        GameData.isGame = false;
        Gamemanager.Instance.SetDiamondCount(Gamemanager.Instance.NewDiamondCount);
        if (Gamemanager.Instance.isHardMode)
        {
            Gamemanager.Instance.SetBestScoreHard(Gamemanager.Instance.Score);
            if (Gamemanager.Instance.GetBestScoreHard() <= Gamemanager.Instance.GetScore())
            {
                Myaudio.PlayOneShot(vars.NewBestClip);
                New.SetActive(true);
                txt_BestScore.text = "最高分：" + Gamemanager.Instance.GetScore().ToString();
            }
            else
            {
                New.SetActive(false);
                txt_BestScore.text = "最高分：" + Gamemanager.Instance.GetBestScoreHard().ToString();
            }
        }
        else
        {
            Gamemanager.Instance.SetBestScore(Gamemanager.Instance.Score);
            if (Gamemanager.Instance.GetBestScore() <= Gamemanager.Instance.GetScore())
            {
                Myaudio.PlayOneShot(vars.NewBestClip);
                New.SetActive(true);
                txt_BestScore.text = "最高分：" + Gamemanager.Instance.GetScore().ToString();
            }
            else
            {
                New.SetActive(false);
                txt_BestScore.text = "最高分：" + Gamemanager.Instance.GetBestScore().ToString();
            }
        }
        if (Gamemanager.Instance.isMoreScore)
        {
            txt_Score.text = Gamemanager.Instance.GetScore().ToString() + "*" + ShopPanel.GetScoreTimes().ToString()
                + "=" + ((int)(Gamemanager.Instance.GetScore() * ShopPanel.GetScoreTimes())).ToString();
        }
        else
        {
            txt_Score.text = Gamemanager.Instance.GetScore().ToString();
        }
        txt_NewAddDiamond.text = "+" + Gamemanager.Instance.NewDiamondCount.ToString();
        //NewDiamondCount = 0;
    }
}
