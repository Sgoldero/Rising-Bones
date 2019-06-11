using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunsetController : MonoBehaviour {

    enum NightStates
    { INACTIVE, SUNSET, NIGHT, SUNRISE};

    NightStates state;
    SpriteRenderer[] sprs;
    SpriteMask sprmsk;
    float timeState;
    GameObject target;

    [Range(0.5f,1)]public float nightDarkness = 0.8f;
    public float timeToSet = 2f, timeToRise = 2f;
    [HideInInspector] public float nightTime = 5f;
    public float freqLight = 5f;
    public float ampLight = 0.03f;

    private void OnEnable()
    {
        if (sprs == null)
        {
            return; // En aixo aconseguim q la primera vegada que s'execute l'objecte (quan s'inicia el nivell) no s'active l'efecte
        }
        state = NightStates.SUNSET;
        timeState = 0;

        EnableSprites();
    }

    private void EnableSprites()
    {
        foreach (SpriteRenderer spr in sprs)
        {
            spr.enabled = true;
            sprmsk.enabled = true;
        }
    }

    private void DisableSprites()
    {
        foreach (SpriteRenderer spr in sprs)
        {
            spr.enabled = false;
            sprmsk.enabled = false;
        }
    }

    private void Awake()
    {
        state = NightStates.INACTIVE;

        sprs = GetComponentsInChildren<SpriteRenderer>(); // Torna els spr d'este gamobject i els seus fills
        sprmsk = GetComponentInChildren<SpriteMask>();

        DisableSprites();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch (state)
        { 
            case NightStates.SUNSET:
                UpdateSunset();
                break;
            case NightStates.NIGHT:
                UpdateNight();
                break;
            case NightStates.SUNRISE:
                UpdateSunrise();
                break;
        }
    }

    private void UpdateNight()
    {

        timeState += Time.deltaTime;
        if (timeState >= nightTime)
        {
            state = NightStates.SUNRISE;
            timeState = 0;
        }
    }

    private void UpdateSunrise()
    {
        Color c = new Color(1, 1, 1, Mathf.Lerp(nightDarkness, 0, timeState / timeToSet)); // Aixi es va fent fosc progresivament sgons els valors indicats
        foreach (SpriteRenderer spr in sprs)
        {
            spr.color = c;
        }



        timeState += Time.deltaTime;
        if (timeState >= timeToRise)
        {
            state = NightStates.INACTIVE;
            DisableSprites();
            this.enabled = false;
        }
    }

    private void UpdateSunset()
    {
        Color c = new Color(1, 1, 1, Mathf.Lerp(0, nightDarkness, timeState/timeToSet)); // Aixi es va fent fosc progresivament sgons els valors indicats
        foreach(SpriteRenderer spr in sprs)
        {
            spr.color = c;
        }



        timeState += Time.deltaTime;
        if (timeState >= timeToSet)
        {
            state = NightStates.NIGHT;
            timeState = 0;
        }
    }

    private void updatePosition()
    {
       // transform.position = target.position;
        float variation = ampLight * Mathf.Sin(freqLight * 2 * Mathf.PI * Time.time);
        transform.localScale = new Vector3(1f + variation, 1f + variation, 1);
    }
}
