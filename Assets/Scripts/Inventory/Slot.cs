using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    private Inventorys inventory;
    public int i;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventorys>();
    }

    void Update()
    {
        if (transform.childCount < 1)
        {
            inventory.isFull[i] = false;
        }
    }
}