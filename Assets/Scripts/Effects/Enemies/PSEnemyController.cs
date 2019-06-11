using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSEnemyController : MonoBehaviour
{

    enum States
    { BLOOD };
    States state;
    private ParticleSystem psBlood;

    // Use this for initialization
    void Start()
    {
        psBlood = GameObject.Find("PS_Blood").GetComponent<ParticleSystem>();

        ChangeState(States.BLOOD);
    }

    private void ChangeState(States newState)
    {
        if (newState == state)
            return;

        switch (state)
        {
            case States.BLOOD:
                psBlood.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                break;
        }

        switch (newState)
        {
            case States.BLOOD:
                if (psBlood.isStopped)
                    psBlood.Play();
                break;
        }

        state = newState;
    }
}
