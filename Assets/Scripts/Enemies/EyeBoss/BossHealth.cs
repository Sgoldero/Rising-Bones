using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    public Slider sldr;
    

    // Use this for initialization
    void Start()
    {
        UpdateSlider();
        //GameManager.instance.UpdateBossHealthEvent.AddListener(UpdateSlider);
    }

    public void UpdateSlider()
    {
        //sldr.value = GameManager.instance.GetBossHealth() / GameManager.instance.archmageEye.health;
        
    }
}
