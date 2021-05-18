using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount : MonoBehaviour
{
    public float TimeofSkill;
    public MyButton currentButton;
    private float Timer=100;
    [HideInInspector]
    public bool iscount;
    

   
    // Update is called once per frame
    void Update()
    {
        if (Timer > TimeofSkill)
        {
            transform.GetComponent<Image>().fillAmount = 1;
            Timer = 0;
            iscount = false;
            currentButton.interactable = true;
        }
        else
        {
            if (iscount)
            {
                Timer += Time.deltaTime;
                transform.GetComponent<Image>().fillAmount = Timer / TimeofSkill;
                currentButton.interactable = false;
            }
        }
    }
}
