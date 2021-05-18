using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ControlinPlay : MonoBehaviour
{
    private bool isJump=false;
    private int isLeft;
    private static int speed = 200;
    public static bool isDead = false;
    private ManagerVars vars;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        m_AudioSource = GetComponent<AudioSource>();
        EventCenter.AddListener<bool>(EventDefine.IsMusicOn, isMusicOn);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventDefine.IsMusicOn, isMusicOn);
    }

    void Update()
    {
        if (isDead) { return; }
        gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))//左右移动
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                isLeft = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                isLeft = -1;
            }
            transform.localScale = new Vector3(1, 1, 1) * 2;
            transform.Translate(Vector3.left * Time.deltaTime * speed * isLeft);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isJump == false )//跳跃
        {
            m_AudioSource.PlayOneShot(vars.Jumpclip);
            transform.DOMoveY(transform.position.y + 100, 0.3f).OnComplete(() =>
            {
                if (!isDead && Gamemanager.Instance.isTwoJump)
                {
                    StartCoroutine("TwoJump");
                }
            });
            isJump = true;
            
        }else if (Input.GetKeyUp(KeyCode.S) && isJump == true)
        {
            transform.DOMoveY(127, 0.2f);
        }

        if (transform.position.y < -100)
        {
            EventCenter.Broadcast(EventDefine.ShowDeadPanel);
            GameData.isGame = false;
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    IEnumerator TwoJump()
    {
        yield return new WaitUntil(() => (Gamemanager.Instance.isTwoJump && Input.GetKeyDown(KeyCode.Space)));
        m_AudioSource.PlayOneShot(vars.Jumpclip);
        transform.DOMoveY(transform.position.y + 80, 0.2f);
    }
    public static void SetSpeed(float times)
    {
        speed = (int)(speed * times);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isJump = false;
        }
        if (collision.tag == "Enemy")
        {
            EventCenter.Broadcast(EventDefine.ShowDeadPanel);
            GameData.isGame = false;
            isDead = true;
            gameObject.SetActive(false);
        }
        if (collision.tag == "Gem")
        {
            m_AudioSource.PlayOneShot(vars.DiamondClip);
            if (Gamemanager.Instance.isHardMode)
            {
                EventCenter.Broadcast<int>(EventDefine.EatDiamond, 2);
            }
            else
            {
                EventCenter.Broadcast<int>(EventDefine.EatDiamond, 1);
            }
            collision.gameObject.SetActive(false);
        }
    }
    private void isMusicOn(bool value)
    {
        m_AudioSource.mute = value;
    }
}
