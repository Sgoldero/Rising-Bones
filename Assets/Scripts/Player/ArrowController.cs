using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float speed = 4f;
    public float rotationSpeed = 40f;
    private Rigidbody2D rb2D;
    public float arrowDamage = 50f;


    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Invoke("DestroyArrow", 3f);
    }

    private void FixedUpdate()
    {
        rb2D.transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallsBorder")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            print("peto");
            print(GameManager.instance.GetBossHealth());
            // DAÑO AL ENEMIGO
            GameManager.instance.UpdateBossHealth(-arrowDamage);
            Destroy(gameObject);
        }
    }

    private void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
