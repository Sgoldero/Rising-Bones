using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private Inventorys inventory;
    public GameObject itemButton;
    public GameObject effect;

    void Start ()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventorys>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
