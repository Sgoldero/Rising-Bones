using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{

    public GameObject effect;
    private Transform player;
    public float SpeedNumber = 10f;
    public float tiempoPocionUso = 10f;
    private bool potionUsed = false;
    private float normalSpeed = GameManager.instance.GetPlayerSpeed();
    private float i = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Use()
    {
        Instantiate(effect, player.position, Quaternion.identity);
        GameManager.instance.UpdatePlayerSpeed(SpeedNumber);
        potionUsed = true;
        
    }

    private void Update()
    {
        if (potionUsed == true)
        {
            i += Time.deltaTime;
            if (i >= tiempoPocionUso)
            {
                SetDefaultSpeed();
            }
        }
    }

    public void SetDefaultSpeed()
    {
        GameManager.instance.UpdatePlayerSpeed(normalSpeed);
        Destroy(gameObject);
    }
}
