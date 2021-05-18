using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverCanvas : MonoBehaviour
{
    private Button ReplayButton;
    private Button MapButton;
    private Text Lose;
    public GameObject[] Stars;
    private Manager vars;
    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.StarsNum, InitStars);
        EventCenter.AddListener(EventDefine.ShowOver, Show);
        EventCenter.AddListener(EventDefine.CloseOver, Close);
    }
    // Start is called before the first frame update
    void Start()
    {
        ReplayButton = transform.Find("ReplayButton").GetComponent<Button>();
        MapButton = transform.Find("MapButton").GetComponent<Button>();
        Lose = transform.Find("Lose").GetComponent<Text>();
        ReplayButton.onClick.AddListener(onReplayButtonClick);
        MapButton.onClick.AddListener(onMapButtonClick);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.CloseOver, Close);
        EventCenter.RemoveListener(EventDefine.ShowOver, Show);
        EventCenter.RemoveListener<int>(EventDefine.StarsNum, InitStars);
    }
    
    private void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void InitStars(int num)
    {
        if (num == 0)
        {
            Lose.gameObject.SetActive(true);
            return;
        }
        for (int i = 0; i < num; i++)
        {
            Stars[i].SetActive(true);
        }
    }
    private void onReplayButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void onMapButtonClick()
    {
        SceneManager.LoadScene(1);
        //Debug.Log(Mapmanager.tempStarsNum[0]);
        //Debug.Log(Mapmanager.tempStarsNum[1]);
    }

}
