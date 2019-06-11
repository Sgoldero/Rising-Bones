using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    public GameObject effect;
    private Transform player;
    public float HealingNumber = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Use()
    {
        Instantiate(effect, player.position, Quaternion.identity);
        if (GameManager.instance.GetHealth()<100)
        {
            GameManager.instance.UpdatePlayerHealth(HealingNumber);
        }
        
        Destroy(gameObject);
    }
}
