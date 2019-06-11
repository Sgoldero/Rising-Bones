using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{


    private Animator anim;
    private Transform p1, p2, currentP, spawnPoint;
    private Transform target = null;
    private float shootPeriod = 2f;

    public GameObject fireball;
    public float speed = 2f;

    private AudioSource eyeTheme;

    // Use this for initialization

    private void Awake()
    {
        eyeTheme = GetComponent<AudioSource>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();

        p1 = transform.Find("P1");
        p2 = transform.Find("P2");
        spawnPoint = transform.Find("SpawnPoint");

        p1.parent = null;
        p2.parent = null;

        currentP = p1;

        FindObjectOfType<AudioManager>().Play("EyeWiggle");

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetHealth() >= 0 && GameManager.instance.GetBossHealth() >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentP.position, speed * Time.deltaTime);

            float dist = (transform.position - currentP.position).sqrMagnitude;
            if (dist < Mathf.Epsilon)
            {
                currentP = (currentP == p1 ? p2 : p1);
            }
        }
        else
        {
            CancelInvoke();
        }
        if (GameManager.instance.GetBossHealth() <= 0)
        {         
            BossDied();
            CancelInvoke();
            return;
        }
       //print("VIDA BOSS ACTUAL: " + GameManager.instance.GetBossHealth());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.tag == "Player") && target == null)
        {
            target = collision.gameObject.transform;
            InvokeRepeating("Shoot", 0.2f, shootPeriod);

        }
    }

    private void Shoot()
    {
        Instantiate(fireball, spawnPoint.position, transform.rotation);

        FindObjectOfType<AudioManager>().Play("Spell");
    }

    void BossDied()
    {
        if (GameManager.instance.GetBossHealth() <= 0)
        {
            eyeTheme.Stop();

            FindObjectOfType<AudioManager>().Stop("EyeWiggle");

            anim.SetBool("isDead", true);

            FindObjectOfType<AudioManager>().Play("EyeDeath");
        }
        Destroy(this);
    }

}
