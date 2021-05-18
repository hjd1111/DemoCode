using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPanel : MonoBehaviour
{
    private Button Yes;
    private Button No;
    
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowResetPanel, Show);
        EventCenter.AddListener(EventDefine.CloseResetPanel, Close);
        Yes = transform.Find("Yes").GetComponent<Button>();
        No = transform.Find("No").GetComponent<Button>();
        Yes.onClick.AddListener(onYesButtonClick);
        No.onClick.AddListener(onNoButtonClick);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowResetPanel, Show);
    }

    private void onYesButtonClick()
    {
        Time.timeScale = 1;
        Gamemanager.Instance.ResetData();
        gameObject.SetActive(false);
    }


    private void onNoButtonClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
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
