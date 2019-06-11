using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    public Item item;
    public int quantity=1;

	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = item.displayImage;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            GameManager.instance.inv.Add(item, quantity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        GetComponent<SpriteRenderer>().sprite = item.displayImage;
    }
}
