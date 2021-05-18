using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public enum TurretType
    {
        MissileSingle,
        MissileDual,
        Catapult,
        Cannon,
    }
    
    public GameObject currentTarget;
    public Transform turreyHead;

    public float attackDist = 10.0f;
    public float attackDamage;
    public float shootCoolDown;
    private float timer;
    private float loockSpeed=400;
    private float idleSpeed=80;
    public float MinAttackDis;

    private AudioSource Myaudio;
    private Manager vars;

    //public Quaternion randomRot;
    private Vector3 randomRot;
    private Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.MissileSingle;
    
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;
    private void Awake()
    {
        vars = Manager.GetManagerVars();
        Myaudio = transform.GetComponent<AudioSource>();
        EventCenter.AddListener<bool>(EventDefine.isMusic, isMusic);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventDefine.isMusic, isMusic);
    }
    void Start () {
        InvokeRepeating("ChackForTarget", 0, 0.5f);
        //shotScript = GetComponent<TurretShoot_Base>();
        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRitate();
        }

        timer += Time.deltaTime;
        if (timer >= shootCoolDown)
        {
            if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > MinAttackDis)
            {
                timer = 0;
                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                }
                ShootTrigger();
            }
        }
	}

    public void ChackForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Enemy")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    public void FollowTarget() //todo : smooth rotate
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        //turreyHead.forward = targetDir;
        if (turretType == TurretType.MissileSingle)
        {
            turreyHead.forward = targetDir;
        }
        else
        {
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
        }
    }

    public void ShootTrigger()
    {
        //shotScript.Shoot(currentTarget);
         Shoot(currentTarget);
        //Debug.Log("We shoot some stuff!");
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void IdleRitate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.rotation != Quaternion.Euler(randomRot))//以当前朝向为基础进行旋转
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), idleSpeed * Time.deltaTime );
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;

            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Myaudio.PlayOneShot(vars.catapult);
            Bullet projectile = missleGo.GetComponent<Bullet>();
            projectile.target = lockOnPos;
            projectile.damage = attackDamage;
        }
        else if(turretType == TurretType.MissileDual)
        {
            if (shootLeft)
            {
                Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
                Myaudio.PlayOneShot(vars.missile);
                Bullet projectile = missleGo.GetComponent<Bullet>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                projectile.damage = attackDamage;
            }
            else
            {
                Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
                Myaudio.PlayOneShot(vars.missile);
                Bullet projectile = missleGo.GetComponent<Bullet>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                projectile.damage = attackDamage;
            }

            shootLeft = !shootLeft;
        }
        else if(turretType==TurretType.MissileSingle)
        {
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Myaudio.PlayOneShot(vars.missile);
            Bullet projectile = missleGo.GetComponent<Bullet>();
            projectile.head = turreyHead;
            projectile.target = currentTarget.transform;
            projectile.damage = attackDamage;
        }
        else if (turretType == TurretType.Cannon)
        {
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, Quaternion.identity);
            Myaudio.PlayOneShot(vars.canon);
            Bullet projectile = missleGo.GetComponent<Bullet>();
            projectile.target = currentTarget.transform;
            projectile.damage = attackDamage;
        }
        
    }

    private void isMusic(bool value)
    {
        Myaudio.mute = value;
    }
}
