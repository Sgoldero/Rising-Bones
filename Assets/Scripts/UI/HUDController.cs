using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public Slider sldr;
    public Text txt;
    public Text score;
    public float pts;


    // Use this for initialization
    void Start ()
    {
        
        UpdateSlider();
        GameManager.instance.UpdatePlayerHealthEvent.AddListener(UpdateSlider);
	}

    private void Update()
    {
        UpdateScore();
    }

    public void UpdateSlider()
    {
        sldr.value = GameManager.instance.GetHealth() / GameManager.instance.pModel.health;
        pts = GameManager.instance.GetPlayerPoints();
        if (sldr.value > 0) {
            int hp = (int) GameManager.instance.GetHealth();
            txt.text = hp.ToString();
        }
        else
        {
            txt.text = "0";
        }
        
    }

    public void UpdateScore()
    {
        score.text = GameManager.instance.GetPlayerPoints().ToString();
    }
}
