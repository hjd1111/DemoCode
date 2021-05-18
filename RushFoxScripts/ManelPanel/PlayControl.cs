using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{
    private static int speed = 200 ;
    public Animator ani;
    private int isLeft = -1;
    private bool isJump = false;
    public static bool isMove=true;
    private AudioSource m_AudioSource;
    private ManagerVars vars;

    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener<bool>(EventDefine.IsMusicOn, isMusicOn);
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventDefine.IsMusicOn, isMusicOn);
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetInteger("Ani_int", 0);
        gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        if ((Input.GetKey(KeyCode.A) && isMove) || Input.GetKey(KeyCode.D))//左右移动
        {
            ani.SetInteger("Ani_int", 1);
            if (Input.GetKey(KeyCode.A))
            {
                isLeft = 1;
            } else if (Input.GetKey(KeyCode.D))
            {
                isLeft = -1;
                //Gamemanager.Instance.isMove = true;
            }
            transform.localScale = new Vector3(-isLeft, 1, 1) * 2 ;
            transform.Translate(Vector3.left * Time.deltaTime * speed * isLeft);

        }


        if (Input.GetKey(KeyCode.W) && IsRayLadder())//爬楼梯
        {
            ani.SetInteger("Ani_int", 2);
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 50;
        }



        if (Input.GetKeyDown(KeyCode.Space) && !isJump)//跳跃
        {
            m_AudioSource.PlayOneShot(vars.Jumpclip);
            transform.DOMoveY(transform.position.y + 100, 0.3f).OnComplete(()=>
            {
                StartCoroutine("TwoJump");
            });
            isJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) && isJump)
        {
            transform.DOMoveY(129, 0.2f);
        }
        
    }
    IEnumerator TwoJump()
    {
        yield return new WaitUntil(() => (Gamemanager.Instance.isTwoJump && Input.GetKeyDown(KeyCode.Space)));
        m_AudioSource.PlayOneShot(vars.Jumpclip);
        transform.DOMoveY(transform.position.y + 100, 0.3f);
    }

    public static void SetSpeed(float times)
    {
        speed = (int)(speed * times);
    }
    private void isMusicOn(bool value)
    {
        m_AudioSource.mute = value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isJump = false;
        }
    }
    public bool IsRayLadder()//判断楼梯，可扩展
    {
        //RaycastHit2D hit = Physics2D.Raycast(RayDown.position, Vector2.down, 1f, Ladder);

        //if (hit)
        //{
        //    if (hit.collider.tag == "ladder")
        //    {
        //        return true;
        //    }
        //}
        //return false;
        if (transform.position.x > 420&& transform.position.x<460)
        {
            return true;
        }
        return false;
    }
    
    
}
