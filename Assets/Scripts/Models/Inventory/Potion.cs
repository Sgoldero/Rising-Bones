using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Potion", menuName ="Models/Inventory/Potion")]
public class Potion : Item
{
    public float duration; //Si = 0 es for ever
    public float healthUp;
    public float powerUp;
    public bool isFlash;
    public float coolDown;
}
