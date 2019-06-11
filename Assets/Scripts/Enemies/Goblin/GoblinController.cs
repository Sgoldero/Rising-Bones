using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinController : EnemyController
{

    [Range(0, 1)] public float FOV = 0.5f;
    [HideInInspector] public Transform target;

    private float FOVangle;
    private float cosFOVangle;
    private float currentCosFOV;
    private Vector2 patrolPoint;
    private Transform p1, p2, currentP;
    private Text healthText;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();

        healthText = gameObject.GetComponentInChildren<Text>();

        rb2d = GetComponent<Rigidbody2D>();
        target = null;

        p1 = transform.Find("P1");
        p2 = transform.Find("P2");

        p1.parent = null;
        p2.parent = null;

        currentP = p1;

        FOVangle = Mathf.Lerp(0, 360, FOV);
        cosFOVangle = Mathf.Cos(FOVangle * 0.5f * Mathf.Deg2Rad);

    }

    private void PerceptionService()
    {

        Collider2D col = Physics2D.OverlapCircle(transform.position, rangeVision, playerLayer.value);
        if (col == null)
        {
            target = null;
            return;
        }

        Vector2 etoP = col.gameObject.transform.position - transform.position;
        etoP.Normalize();


        currentCosFOV = Vector2.Dot(transform.position, -transform.up);
        if (currentCosFOV < cosFOVangle)
        {
            anim.SetBool("isAttacking", false);

            target = null;
            return;
        }


        int restOfLayers = ~(1 << gameObject.layer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, etoP, rangeVision, restOfLayers);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {

                target = hit.collider.gameObject.transform;

                Vector3 dir = (target.position - transform.position).normalized;
                anim.SetFloat("moveX", dir.x);
                anim.SetFloat("moveY", dir.y);
                if ((target.position - transform.position).magnitude < attackRadius)
                {
                    // GameManager.instance.UpdatePlayerHealth(-baseDamage * Time.deltaTime);

                    anim.SetBool("isAttacking", true);
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
                    anim.SetBool("isAttacking", false);
                }

                return;
            }
        }
        target = null;
    }

    private void SelectPatrolPoint()
    {

        Vector3 dir = (currentP.position - transform.position).normalized;
        anim.SetFloat("moveX", dir.x);
        anim.SetFloat("moveY", dir.y);
        if (dir.x != 0 || dir.y != 0)
        {
            //anim.SetBool("isMoving", true);
            transform.position = Vector3.MoveTowards(transform.position, currentP.position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        float dist = (transform.position - currentP.position).sqrMagnitude;
        if (dist < Mathf.Epsilon)
        {
            currentP = (currentP == p1 ? p2 : p1);
        }
        
    }

    private void Update()
    {
        Vector3 healthPos = Camera.main.WorldToScreenPoint(transform.position);
        healthText.transform.position = healthPos;
        healthText.text = enemyHealth.ToString();

        if (GameManager.instance.GetHealth() > 0 && !anim.GetBool("isDead"))
        {
            SelectPatrolPoint();
            PerceptionService();
        }
        if (isDead)      
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0)        
                Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        //FOVangle = Mathf.Lerp(0, 180, FOV);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, rangeVision);
        Gizmos.color = Color.red;

    }

    public void PlayAttackSound()
    {

        FindObjectOfType<AudioManager>().Play("GoblinAttack1");
    }

    public void PlayDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("GoblinDeath");
    }

    public void PlayWalkingSound()
    {
        FindObjectOfType<AudioManager>().Play("GeneralWalking");
    }

}
