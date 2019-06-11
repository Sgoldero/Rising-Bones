using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {


    /*
     * =========================================================================
     * 
     * HAY QUE MEJORAR ESTE SCRIPT, ES MUY "TOSCO" DE MOMENTO
     * 
     * =========================================================================
     */

    [Header("Knockback variables")]
    public float knockbackPower = 3f;
    public float knockbackTime = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb2d = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Rigidbody2D>();

        //rb2d.isKinematic = false;
        Vector2 diff = rb2d.transform.position - transform.position;
        diff = diff.normalized * knockbackPower;
        rb2d.AddForce(diff, ForceMode2D.Impulse);

        StartCoroutine(KnockbackTarget(rb2d));
    }

    public IEnumerator KnockbackTarget(Rigidbody2D target)
    {
        //TODO: FALTA PULIR TODO ESTE CÓDIGO!!!!!!!!
        if (target != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            target.velocity = Vector2.zero;
            //target.isKinematic = true;
        }
    }//Knockback()
}

