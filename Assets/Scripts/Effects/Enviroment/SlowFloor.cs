using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFloor : MonoBehaviour {

    [Range(0, 1)] public float slowFactor = 0.5f;

    private Transform target = null;
    private PlayerModel pModel;

    private void Start()
    {
        pModel = GameManager.instance.pModel;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(BoxCollider2D)
            && (collision.gameObject.tag == "Player")
            && target == null)
        {
            target = collision.gameObject.transform;


            if (!pModel.isFlash)
            {
                GameManager.instance.UpdatePlayerSpeed(pModel.slowSpeed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            target = null;
            if (!pModel.isFlash)
            {
                GameManager.instance.UpdatePlayerSpeed(pModel.normalSpeed);
            }
        }
    }
}
