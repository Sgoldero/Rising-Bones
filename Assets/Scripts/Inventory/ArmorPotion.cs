using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPotion : MonoBehaviour
{

    public GameObject effect;
    private Transform player;
    public float tiempoPocionUso = 10f;
    private bool potionUsed = false;
    private float i = 0f;

    void Start()
    {
    }

    public void Use()
    {

    }

    private void Update()
    {

    }

    public void SetDefaultSpeed()
    {
        Destroy(gameObject);
    }
}
