using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {


    //private Collider2D trigger;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        //trigger = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

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
            Destroy(this);
        }
    }
}
