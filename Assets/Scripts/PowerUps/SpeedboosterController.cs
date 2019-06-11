using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedboosterController : MonoBehaviour {

    private PlayerController playerController;
    //private float pInitialSpeed;
    private PlayerModel pModel;
    private Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && target == null)
        {
            target = collision.gameObject.transform;
            pModel = GameManager.instance.pModel;
            GameManager.instance.UpdatePlayerSpeed(pModel.flashSpeed);
            pModel.isFlash = true;
            Invoke("SpeedCooldown", pModel.flashTime);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void SpeedCooldown()
    {
        target = null;
        GameManager.instance.UpdatePlayerSpeed(pModel.normalSpeed);
        pModel.isFlash = false;
        Destroy(gameObject);
    }
}
