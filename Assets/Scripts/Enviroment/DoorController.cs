using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private Animator anim;
    private Collider2D wall;

    // Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        wall = GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("OpenObject");
            anim.SetBool("isOpen", true);
            Destroy(wall);
            Destroy(this);
        }
    }

    //para llave: mantener el collider de trigger desactivado, asi la puerta se mantendra cerrada -> al tener la llave una funcion activa ese collider: if (player has key) enable.collider
}
