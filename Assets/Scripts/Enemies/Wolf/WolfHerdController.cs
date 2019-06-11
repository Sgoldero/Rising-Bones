using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHerdController : EnemyController {
    Vector3 target = new Vector3();


    // Use this for initialization
    void Start () {
        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position;

        Collider2D col = Physics2D.OverlapCircle(transform.position, rangeVision, playerLayer.value);

        // Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            // etoP,
            player.transform.position - transform.position,
            rangeVision,
             1 << LayerMask.NameToLayer("Player")
        // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
        // También poner al objeto Attack y al Prefab Slash una Layer Attack 
        // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
        if (distance < attackRadius)
        {
            

            anim.SetFloat("moveX", dir.x);
            anim.SetFloat("moveY", dir.y);
            anim.SetBool("isAttacking", true);
            anim.SetBool("isMoving", false);

            if (anim.GetBool("isAttacking"))
            {
                // GameManager.instance.UpdatePlayerHealth(-wolfDamage * Time.deltaTime);
            }

            if (GameManager.instance.GetHealth() <= 0)
            {
                anim.SetBool("isMoving", true);
                anim.SetBool("isAttacking", false);
            }

        }
        else
        {
                //rb2d.transform.position += dir * chaseSpeed * Time.deltaTime;

                anim.SetFloat("moveX", dir.x);
                anim.SetFloat("moveY", dir.y);
                anim.SetBool("isMoving", true);
                anim.SetBool("isAttacking", false);          
        }
        
    }

    public void PlayAttackSound()
    {
        FindObjectOfType<AudioManager>().Play("WolfAttack");
    }

    public void PlayChaseSound()
    {
        FindObjectOfType<AudioManager>().Play("WolfChase");
    }

    public void PlayHowlSound()
    {
        FindObjectOfType<AudioManager>().Play("WolfHowl");
    }

    public void PlayDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("WolfDeath");
    }
}
