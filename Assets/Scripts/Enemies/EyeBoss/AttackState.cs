using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour {

    public float shootPeriod = 4f;
    public GameObject fireBall;
    //public float rotationSpeed = 40f;

    private Transform spawnPoint;
    private Transform target;

    // Use this for initialization
    void Start()
    {
        spawnPoint = transform.Find("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //Vector2 ctoP = target.position - transform.position;

        //Quaternion rot = Quaternion.LookRotation(transform.forward, ctoP);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (target == null))
        {
            target = collision.gameObject.transform;
            InvokeRepeating("Shoot", 0.2f, shootPeriod);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
            CancelInvoke();
        }
    }

    private void Shoot()
    {
        Instantiate(fireBall, spawnPoint.position, transform.rotation);
    }
}
