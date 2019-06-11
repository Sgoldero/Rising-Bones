using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour {

    public GameObject effect;
    private Transform player;
    public float SpeedNumber = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Use()
    {
        Instantiate(effect, player.position, Quaternion.identity);
        GameManager.instance.UpdatePlayerSpeed(SpeedNumber);
        Destroy(gameObject);
    }
}
