using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed = 5f;
    public Transform pos;
    public float HP;
    public float Damage;
    public float AttackDis;
    public float AttackSpe;
    public float MoveDis;
    public bool isBusy;
    

    private float timer;
    public GameObject TargetEnemy;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("CheckForTarget", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position,pos.position)>MoveDis)
        {
            TargetEnemy = null;
        }
        if (TargetEnemy != null)
        {
            if (Vector3.Distance(transform.position, TargetEnemy.transform.position) > AttackDis)
            {
                transform.Translate((TargetEnemy.transform.position - transform.position)*Time.deltaTime*speed, Space.World);
            }
        }
        else if (Vector3.Distance(transform.position, pos.position) > 0.1f)
        {
            Move(pos);
        }else if (TargetEnemy == null)
        {
            animator.SetInteger("Attack", 0);
            isBusy = false;
        }
        else
        {
            float currentTargetDist = Vector3.Distance(transform.position, TargetEnemy.transform.position);
            if (currentTargetDist > AttackDis)
            {
                TargetEnemy = null;
            }
        }

        timer += Time.deltaTime;
        if (timer >= AttackSpe)
        {
            if (TargetEnemy != null)
            {
                timer = 0;
                animator.SetInteger("Attack", 1);
                Attack();
            }
        }
        
    }
    private void Move(Transform pos)
    {
        transform.LookAt(pos);
        transform.Translate((pos.position-transform.position).normalized * speed * Time.deltaTime, Space.World);
    }
    private void Dead()
    {
    //    animator.SetInteger("Die", 1);
    //    Destroy(gameObject,2f);
        if (TargetEnemy != null)
        {
            TargetEnemy.GetComponent<Enemy>().isBusy = false;
            TargetEnemy.GetComponent<Enemy>().isMove = true;
        }
        if (pos.name == "pos1")
        {
            transform.parent.GetComponent<Battacks>().isSoldierAlive[0] = false;
        }
        else if (pos.name == "pos2")
        {
            transform.parent.GetComponent<Battacks>().isSoldierAlive[1] = false;
        }
        else if (pos.name == "pos3")
        {
            transform.parent.GetComponent<Battacks>().isSoldierAlive[2] = false;
        }
        
    }
    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;
        HP -= damage;
        if (HP <= 0)
        {
            Dead();
        }
    }
    private void CheckForTarget()
    {
        if(TargetEnemy!= null) { return; }
        Collider[] colls = Physics.OverlapSphere(transform.position, AttackDis);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Enemy" )
            {
                if(colls[i].GetComponent<Enemy>().enemytype == Enemy.EnemyType.Spider && colls[i].GetComponent<Enemy>().isBusy == false)
                {
                    colls[i].GetComponent<Enemy>().isBusy = true;
                    colls[i].GetComponent<Enemy>().TargetSoldier = gameObject;//保证士兵对蜘蛛有一定的阻挡作用
                    isBusy = true;
                    TargetEnemy = colls[i].gameObject;
                    break;
                }
                else if(colls[i].GetComponent<Enemy>().enemytype == Enemy.EnemyType.Mech)
                {
                    float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                    if (dist < distAway)
                    {
                        TargetEnemy = colls[i].gameObject;
                        distAway = dist;
                    }
                }

            }
        }
    }
    private void Attack()
    {
        transform.LookAt(TargetEnemy.transform);
        TargetEnemy.GetComponent<Enemy>().TakeDamge(Damage);
    }

}
