using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyModel", menuName = "Models/Characters/Enemy")]
public class EnemyModel : CharacterModel
{
    [Range(0.5f, 10f)] public float rangeVision = 2f;
    [Range(0, 1)] public float FOV = 0.5f;
    public float patrolSpeed = 0.5f;
    public float timeToPerception = 0.2f;
    public float chaseSpeed = 1f;
    public float attackDistance = 0.5f;
    public float damage = 10;
}
