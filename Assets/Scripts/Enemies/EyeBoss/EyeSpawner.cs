using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSpawner : MonoBehaviour {

    public GameObject Eye;
    private Collider2D trigger;

    // Use this for initialization
    void Start()
    {
        trigger = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "Player")
        {
            Instantiate(Eye);
            Destroy(gameObject);
        }
    }
}
