using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "Models/Characters/Player")]
public class PlayerModel : CharacterModel
{

    public float slowSpeed = 1f;
    public float points = 0;


    public float normalSpeed = 5f;
    public float normalForce = 5f;

    public float flashSpeed = 10f;
    public float flashForce = 10f;

    public float flashTime = 0.1f;

    public bool isFlash = false;
    public LayerMask flasHiddenLayer;
}
