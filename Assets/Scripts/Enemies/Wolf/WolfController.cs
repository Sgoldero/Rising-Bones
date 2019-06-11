using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : EnemyController
{
 
    public bool howled = false;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;

    Vector3 initialPosition;

    void Start()
    {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Por defecto nuestro target siempre será nuestra posición inicial
        Vector3 target = initialPosition;

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

        // Aquí podemos debugear el Raycast
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        if (GameManager.instance.GetHealth() > 0 && !anim.GetBool("isDead"))
        {

            // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
            if (target != initialPosition && distance < attackRadius)
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
            // En caso contrario nos movemos hacia él
            else
            {

                if (distance > rangeVision)
                {
                    if (!anim.GetBool("howled"))
                    {
                        anim.SetTrigger("howl"); // Si el player entra en rango de vision del lobo este aulla
                    }
                }

                if (anim.GetBool("howled")) // Despues de aullar si sigue en rango de vision persigue al player
                {
                    rb2d.transform.position += dir * chaseSpeed * Time.deltaTime;
                    anim.SetFloat("moveX", dir.x);
                    anim.SetFloat("moveY", dir.y);
                    anim.SetBool("isMoving", true);
                    anim.SetBool("isAttacking", false);
                }
            }

            // Una última comprobación para evitar bugs forzando la posición inicial
            if (target == initialPosition && distance < 0.02f)
            {
                transform.position = initialPosition;
                // Y cambiamos la animación de nuevo a Idle
                anim.SetBool("isMoving", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("howled", false);
                anim.SetBool("howl", false);
            }
        }
        else // Si el player muere lo dejamos de atacar
        {
            target = initialPosition;
            dir = (target - transform.position).normalized;
            rb2d.transform.position += dir * chaseSpeed * Time.deltaTime;
            anim.SetBool("isMoving", true);
            anim.SetBool("isAttacking", false);

        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
    }

    private void stopHowl()
    {
        anim.SetBool("howled", true);
        callWolves();
    }

    private void callWolves()
    {
        spawnPoint1.SetActive(true);
        spawnPoint2.SetActive(true);     
    }

    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeVision);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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