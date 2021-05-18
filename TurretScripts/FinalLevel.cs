using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalLevel : MonoBehaviour
{
    public Button Back;
    private void Start()
    {
        Back.onClick.AddListener(BackClick);
    }
    private void BackClick()
    {
        SceneManager.LoadScene(1);
    }
}
