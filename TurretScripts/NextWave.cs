using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    public float TimeofWave=10f;
    public float Timer = 10f;
    public bool isNextWave;
    public float Reward;



    // Update is called once per frame
    void Update()
    {
        if (isNextWave)
        {
            if (Timer < 0) return;
            Timer -= Time.deltaTime;
            Reward = transform.GetComponent<Image>().fillAmount = Timer / TimeofWave;
            if (Timer <= 0)
            {
                MenuCancvas.Instance.OnNextWaveClick();
            }
        }
        //if (gameObject.activeSelf == false)
        //{
        //    Debug.Log("wssb");
        //    HideTime -= Time.deltaTime;
        //    if (HideTime == 0)
        //    {
        //        gameObject.SetActive(true);
        //        HideTime = 3f;
        //        isNextWave = true;
        //    }
        //}
        

    }
    

}
