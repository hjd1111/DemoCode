using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public TurretAI.TurretType type = TurretAI.TurretType.Cannon;
    public Transform target;
    public bool lockOn;
    //public bool track;
    public float speed = 1;
    public float turnSpeed = 1;
    public bool catapult;
    public float damage;
    public Transform head;
    public float boomTimer = 2;
    public ParticleSystem explosion;


    //public float knockBack = 0.1f;
    //public Vector3 _startPosition;
    //public float dist;

    private void Awake()
    {
        EventCenter.AddListener<string>(EventDefine.isSkill, BulletSkill);
    }
    private void OnDestroy()
    {
        EventCenter.AddListener<string>(EventDefine.isSkill, BulletSkill);
    }
    private void Start()
    {
        if (catapult)
        {
            lockOn = true;
            
        }

        if (type == TurretAI.TurretType.MissileSingle)
        {
            StartCoroutine(Delay());
        }
        if (type == TurretAI.TurretType.Cannon)
        {
            
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Explosion();
            return;
        }

        if (transform.position.y < 0)
        {
            Explosion();
        }

        boomTimer -= Time.deltaTime;
        if (boomTimer < 0)
        {
            Explosion();
        }

        if (type == TurretAI.TurretType.Catapult)
        {
            if (lockOn)
            {
                Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                lockOn = false;
            }
        }
        else if(type == TurretAI.TurretType.MissileDual)
        {
            Vector3 dir = target.position - transform.position;
            //float distThisFrame = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            //transform.Translate(dir.normalized * distThisFrame, Space.World);
            //transform.LookAt(target);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);//追踪
            transform.rotation = Quaternion.LookRotation(newDirection);

        }else if (type == TurretAI.TurretType.MissileSingle)
        {
            //if (!Find)
            //{
            //    transform.Translate(head.forward * speed * Time.deltaTime * 1, Space.Self);
            //}
            //else
            //{
            //    Vector3 dir = target.position - transform.position;
            //    //q = Quaternion.LookRotation(dir);
            //    //transform.rotation = Quaternion.Slerp(transform.rotation, q, turnSpeed * 30);
            //    Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            //    transform.rotation = Quaternion.LookRotation(newDirection);
            //    float singleSpeed = speed * Time.deltaTime;
            //    transform.Translate(transform.forward * singleSpeed * 3, Space.World);
            //}
            Vector3 dir = target.position - transform.position;
            //q = Quaternion.LookRotation(dir);
            //transform.rotation = Quaternion.Slerp(transform.rotation, q, turnSpeed * 30);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            float singleSpeed = speed * Time.deltaTime;
            transform.Translate(transform.forward * singleSpeed * 3, Space.World);
        }
        else if (type == TurretAI.TurretType.Cannon)
        {
            Vector3 distance = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(distance);
            float singleSpeed = speed * Time.deltaTime;
            transform.Translate(transform.forward * speed , Space.World);
        }
    }

    private void BulletSkill(string skillname)
    {
        switch (skillname)
        {
            case "OneShoot":
                damage = 10000;
                break;
        }
    }
    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            //Vector3 dir = other.transform.position - transform.position;
            //Vector3 knockBackPos = other.transform.position * (-dir.normalized * knockBack);//击退
            //Vector3 knockBackPos = other.transform.position + (dir.normalized * knockBack);
            //knockBackPos.y = 1;
            //other.transform.position = knockBackPos;
            Explosion();
        }
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
    }
    
}
