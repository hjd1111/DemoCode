using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Spider,
        Mech,
    }

    [Header("[Enemy Type]")]
    public EnemyType enemytype = EnemyType.Spider;

    private Transform[] positions;
    private int index = 0;

    private float totalhp;
    public GameObject Explosion;
    private float TurnSpeed = 10f;
    private Slider slider;
    public Canvas HPCanvas;
    private float timer;
    private float Firetimer=0;
    private Animator animator;

    public float speed = 10;
    public float hp = 100;
    public float Damage = 5;
    public float AttackDis;
    public float AttackSpe;
    public int Value;
    public bool isBusy;
    public bool isMove = true;
    public GameObject TargetSoldier;
    // Use this for initialization
    void Start()
    {
        float whichroad = Random.Range(0f, 5f);
        if (WayPoints.Instance.isOnlyOneRoad)
        {
            positions = WayPoints.Instance.positions1;
        }
        else if (whichroad >= 2.5f)
        {
            //Debug.Log(whichroad);
            positions = WayPoints.Instance.positions1;
        }
        else if(whichroad < 2.5f)
        {
            //Debug.Log(whichroad);
            positions = WayPoints.Instance.positions2;
        }
        totalhp = hp;
        slider = HPCanvas.GetComponentInChildren<Slider>();
        animator = GetComponent<Animator>();
        InvokeRepeating("CheckForTarget", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Firetimer += Time.deltaTime;
        if (TargetSoldier == null || TargetSoldier.GetComponent<Soldier>().HP<0)
        {
            Move();
            isBusy = false;
        }
        else
        {
            float currentTargetDist = Vector3.Distance(transform.position, TargetSoldier.transform.position);
            if (currentTargetDist > AttackDis)
            {
                TargetSoldier = null;
            }
        }
        
        if (timer >= AttackSpe)
        {
            if (TargetSoldier != null && Vector3.Distance(transform.position, TargetSoldier.transform.position) < AttackDis)
            {
                isMove = false;
                timer = 0;
                animator.SetInteger("Attack", 1);
                Attack();
            }
            else
            {
                isMove = true;
            }
        }
    }
    private void Move()
    {
        if (index > positions.Length) { return; }
        if (!isMove) { return; }
        else if (index > positions.Length - 1)
        {
            Reach();
        }
        else
        {
            //transform.LookAt(positions[index]);
            animator.SetInteger("Attack", 0);
            Quaternion q = Quaternion.LookRotation(positions[index].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, TurnSpeed * Time.deltaTime);
            transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed, Space.World);
            if (Vector3.Distance(positions[index].position, transform.position) < 0.5f)
            {
                index++;
            }
        }
        
    }
    void Reach()
    {
        EventCenter.Broadcast(EventDefine.ReduceHeart);
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }
    public void TakeDamge(float damge)
    {
        if (hp <= 0) return;
        hp -= damge;
        slider.value = (int)hp / totalhp;
        if (hp <= 0)
        {
            if (TargetSoldier != null)
            {
                TargetSoldier.GetComponent<Soldier>().isBusy = false;
            }
            Die();
        }
    }

    private void Die()
    {
        EventCenter.Broadcast<int>(EventDefine.ChangeMoney, Value);
        GameObject effect = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
        {
            //Vector3 dir = other.transform.position - transform.position;
            //Vector3 knockBackPos = other.transform.position * (-dir.normalized * knockBack);//击退
            //Vector3 knockBackPos = other.transform.position + (dir.normalized * knockBack);
            //knockBackPos.y = 1;
            //other.transform.position = knockBackPos;
            TakeDamge(other.GetComponent<Bullet>().damage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "FireBall")
        {
            if (Firetimer > 0.5f)
            {
                TakeDamge(other.GetComponent<Skill>().damage);
                Firetimer = 0;
                Debug.Log("wssb");
            }
        }
    }
    private void CheckForTarget()
    {
        if (TargetSoldier != null) { return; }
        Collider[] colls = Physics.OverlapSphere(transform.position, AttackDis);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Soldier")
            {
                if (colls[i].GetComponent<Soldier>().isBusy && enemytype==EnemyType.Spider) { continue; }//蜘蛛会略过忙碌的士兵
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    TargetSoldier = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }
    private void Attack()
    {
        TargetSoldier.GetComponent<Soldier>().TakeDamage(Damage);
    }
}
