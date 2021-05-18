using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(Loading());
    //}

    //IEnumerator Loading()
    //{
    //    yield return new WaitForSeconds(2f);
    //    SceneManager.LoadScene(3);
    //}
    private AsyncOperation asyncOperation;
    private float Timer;
    void Start()
    {
        Timer = 2f;
        // 协程启动异步加载
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        asyncOperation = SceneManager.LoadSceneAsync("GameScence");
        //终止自动切换场景
        asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }
    void Update()
    {
        // 获取加载进度，让进度条动起来
        //this.SettingLoadingUI(this.asyncOperation.progress);

        // 当进度大于0.9时就已经加载完毕
        Timer -= 0.005f;//不知道为什么第二次进入Loading时Time.deltaTime失效了
        if (asyncOperation.progress >= 0.9f)
        {
            //Debug.Log(Timer);
            if (Timer <= 0)
            {
                // 允许切换。将在下一帧切换场景
                asyncOperation.allowSceneActivation = true;
            }
        }
    }

}
