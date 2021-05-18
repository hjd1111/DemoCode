using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public MyButton Show;
    public Button YES;
    public Button No;
    public Button Back;

    private void Awake()
    {
        Show.onClick.AddListener(ShowPanel);
        YES.onClick.AddListener(OnYesClick);
        No.onClick.AddListener(OnNoClick);
        Back.onClick.AddListener(OnBackClick);
        gameObject.SetActive(false);
    }
    private void ShowPanel()
    {
        gameObject.SetActive(true);
    }
    private void OnYesClick()
    {
        gameObject.SetActive(false);
        Datamanager.Instance.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnNoClick()
    {
        gameObject.SetActive(false);
    }
    private void OnBackClick()
    {
        SceneManager.LoadScene(0);
    }
}
