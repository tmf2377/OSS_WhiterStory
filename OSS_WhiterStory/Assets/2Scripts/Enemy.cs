using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D };
    public Type enemyType;
    public int maxHealth;
    public int curHealth;
    public int score;

    public int enemyCnt;
    public GameManager manager;

    public Transform target;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public GameObject[] coins;
    public GameObject[] potions;
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public bool isHit = false;

    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public MeshRenderer[] meshs;
    public NavMeshAgent nav;
    public Animator anim;

    public BossNPC bossNpc;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        if(enemyType == Type.D)
            bossNpc = GameObject.Find("Zone(Ludo)").GetComponent<BossNPC>();
        if(enemyType != Type.D)
            Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    void Update()
    {
        if(nav.enabled && enemyType != Type.D)
        {
            target = Player.instance.transform;
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    void Targeting()
    {
        if(!isDead && enemyType != Type.D)
        {
            float targetRadious = 0;
            float targetRange = 0;

            switch (enemyType)
            {
                case Type.A:
                    targetRadious = 1.5f;
                    targetRange = 3f;
                    break;
                case Type.B:
                    targetRadious = 1f;
                    targetRange = 12f;
                    break;

                case Type.C:
                    targetRadious = 0.5f;
                    targetRange = 25f;
                    break;
            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadious,
               transform.forward, targetRange,
               LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;

            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward*20, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;

            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward*20;

                yield return new WaitForSeconds(2f);
                break;

        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }
    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    void FreezeVelocity()
    {
        if(isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec,false));
        } else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            StartCoroutine(OnDamage(reactVec,false));
        }
    }
    public void HitByGrenade(Vector3 explosionPos)
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position- explosionPos;
        StartCoroutine(OnDamage(reactVec,true));

    }
    IEnumerator OnDamage(Vector3 reactVec,bool isGrenade)
    {
        if (!isDead)
        {
            if (isDead)
                yield break;
            if (isHit)
                yield break;
            isHit = true;
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.red;


            if (curHealth > 0)
            {
                yield return new WaitForSeconds(0.2f);
                foreach (MeshRenderer mesh in meshs)
                    mesh.material.color = Color.white;
            }
            else
            {
                foreach (MeshRenderer mesh in meshs)
                    mesh.material.color = Color.gray;

                gameObject.layer = 14;
                isDead = true;
                isChase = false;
                nav.enabled = false;
                anim.SetTrigger("doDie");
                gameObject.GetComponent<BoxCollider>().enabled = false;

                Player.instance.score += score;
                int ranCoin = Random.Range(0, 3);
                int ranPotion = Random.Range(0, 3);
                Instantiate(coins[ranCoin], transform.position, Quaternion.identity);
                Instantiate(potions[ranPotion], transform.position, Quaternion.identity);
                Debug.Log(this.name + "Dead");
                switch (enemyType)
                {
                    case Type.A:
                        manager.enemyCntA--;
                        break;
                    case Type.B:
                        manager.enemyCntB--;
                        break;
                    case Type.C:
                        manager.enemyCntC--;
                        break;
                    case Type.D:
                        manager.enemyCntD--;
                        while (bossNpc == null) { bossNpc = GameObject.Find("Zone(Ludo)").GetComponent<BossNPC>(); }
                        bossNpc.CatchMonster();
                        break;

                }

                if (isGrenade)
                {
                    reactVec = reactVec.normalized;
                    reactVec += Vector3.up * 3;
                    rigid.freezeRotation = false;
                    rigid.AddForce(reactVec * 5, ForceMode.Impulse);
                    rigid.AddTorque(reactVec * 15, ForceMode.Impulse);
                }
                else
                {
                    reactVec = reactVec.normalized;
                    reactVec += Vector3.up;
                    rigid.AddForce(reactVec * 5, ForceMode.Impulse);
                }

                Destroy(gameObject, 4);
                Player.instance.enemyCnt++;
            }
            isHit = false;
        }
    }
}
