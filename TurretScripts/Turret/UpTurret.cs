using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTurret : TurretAI
{
    public bool HaveSkill;
    public bool HaveSoldier;

    private Vector3 UprandomRot;
    private Animator Upanimator;
    private float Uptimer;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChackForTarget", 0, 0.5f);
        //shotScript = GetComponent<TurretShoot_Base>();
        if (HaveSkill)
        {
            if (turretType == TurretType.Cannon)
            {
                InvokeRepeating("CannonSkillOne", 1, shootCoolDown);
            }
        }
        if (transform.GetChild(0).GetComponent<Animator>())
        {
            Upanimator = transform.GetChild(0).GetComponent<Animator>();
        }

        UprandomRot = new Vector3(0, Random.Range(0, 359), 0);
    }

    // Update is called once per frame
    void Update()
    {
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

        Uptimer += Time.deltaTime;
        if (Uptimer >= shootCoolDown)
        {
            if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > MinAttackDis)
            {
                Uptimer = 0;
                if (Upanimator != null)
                {
                    Upanimator.SetTrigger("Fire");
                }
                ShootTrigger();
            }
        }
    }
    private void CannonSkillOne()
    {
        int triger = Random.Range(0, 100);
        if (triger < 5)
        {
            EventCenter.Broadcast<string>(EventDefine.isSkill, "OneShoot");
        }
    }
    private void CatapultSkill()
    {

    }
    private void MissileSkill()
    {

    }
    private void BattackSkill()
    {

    }

}
