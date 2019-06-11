using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Models/Inventory/Weapon")]
public class Weapon : Item
{

    public float range;
    public float damage;
    public int chargerCapacity;
    public float timeToRecharge;
    public float shootCoolDown;
}
