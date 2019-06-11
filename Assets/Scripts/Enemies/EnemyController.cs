using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float enemyHealth;
    public float enemyPoints;
    //public float damageTaken;
    public float patrolSpeed;
    public float baseDamage;
    public float timeToPerception = 0.2f;
    public float chaseSpeed;
    public float destroyTime = 10.0f;

    [Range(0.5f, 10f)] public float rangeVision;
    [Range(0.5f, 10f)] public float attackRadius;

     public GameObject player;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public LayerMask playerLayer;
    [HideInInspector] public bool isDead = false;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
	}

    public void damage(float dmg)
    {
        enemyHealth -= dmg;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0f;
            anim.SetBool("isDead", true);
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttacking", false);
            if (!isDead)
            {
                GameManager.instance.UpdatePlayerPoints(enemyPoints);
                isDead = true;
                print("Puntos del jugador: " + GameManager.instance.GetPlayerPoints());
            }
        }
    }
}
