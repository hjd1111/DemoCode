using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SlectLevel : MonoBehaviour
{
    public bool isunLock;
    private Image ima;
    private Button toGame;
    private int StarsNum=0;
    public GameObject txt_level;
    public GameObject[] Stars;
    private Manager vars;
    private void Awake()
    {
        vars = Manager.GetManagerVars();
    }
    // Start is called before the first frame update
    void Start()
    {
        ima = transform.GetComponent<Image>();
        toGame = transform.GetComponent<Button>();
        toGame.onClick.AddListener(ToGame);
        toGame.interactable = false;
        if (Datamanager.Instance.tempStarsNum[int.Parse(transform.name) - 1] > StarsNum)
        {
            StarsNum = Datamanager.Instance.tempStarsNum[int.Parse(transform.name) - 1];
        }
        if ((int.Parse(transform.name) - 2)>=0 && Datamanager.Instance.tempStarsNum[int.Parse(transform.name) - 2] > 0)
        {
            isunLock = true;
        }
        ShowStar();
        //Debug.Log(StarsNum + "+" + transform.name);
    }

    private void ToGame()
    {
        EventCenter.Broadcast<string>(EventDefine.LevelName, transform.name);
        //Debug.Log(transform.name);
    }
    private void ShowStar()
    {
        if (isunLock)
        {
            ima.overrideSprite = vars.Unlock;
            toGame.interactable = true;
            txt_level.SetActive(true);
            for (int i = 0; i < StarsNum; i++)
            {
                Stars[i].SetActive(true);
            }
        }
    }
}
