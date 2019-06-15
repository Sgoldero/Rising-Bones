using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    public GameObject player;
    public GameObject effect;
    private Transform playerTrans;
    public float SpeedNumber = 10f;
    //private bool potionUsed = false;
    //private float normalSpeed = GameManager.instance.GetPlayerSpeed();
    //private float i = 0f;

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Use()
    {
        Instantiate(effect, playerTrans.position, Quaternion.identity);
        GameManager.instance.SpeedUp(SpeedNumber);
        Destroy(gameObject);
    }

    //private void Update()
    //{
    //    if (potionUsed == true)
    //    {
    //        i += Time.deltaTime;
    //        if (i >= tiempoPocionUso)
    //        {
    //            SetDefaultSpeed();
    //        }
    //    }
    //}

    //public void SetDefaultSpeed()
    //{
    //    GameManager.instance.UpdatePlayerSpeed(normalSpeed);
    //    Destroy(gameObject);
    //}
}
