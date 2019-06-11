using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBallController : MonoBehaviour
{

    public float speed = 3f;
    public float fireballDamage = 10f;
    public GameObject explosion, player;

    private Vector3 target, dir;
    private Rigidbody2D rb2D;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        target = player.transform.position;
        dir = (target - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb2D.transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);

            FindObjectOfType<AudioManager>().Play("Explosion");

            GameManager.instance.UpdatePlayerHealth(-fireballDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "WallsBorder")
        {
            Instantiate(explosion, transform.position, transform.rotation);

            FindObjectOfType<AudioManager>().Play("Explosion");

            Destroy(gameObject);
        }
    }
}
