using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    public UnityEvent updateStockEvent = new UnityEvent();

    public class ItemSelectionEvent : UnityEvent<Item> { };
    public ItemSelectionEvent itemSelectionEvent = new ItemSelectionEvent();

    List<Item> inventory = new List<Item>();

    public int size
    {
        get
        {
            return inventory.Count;
        }
    }

    public int Add(Item item, int quantity)
    {
        int i= inventory.FindIndex(x => x.name == item.name);
        if (i!=-1)
        {
            inventory[i].quantity += quantity;
            updateStockEvent.Invoke();
            return inventory[i].quantity;
        }
        else
        {
            item.quantity = quantity;
            inventory.Add(item);
            updateStockEvent.Invoke();
            return quantity;
        }
    }

    public int Consume(int i, int quantity)
    {
        if (i < inventory.Count)
        {
            int realQ = Mathf.Min(quantity, inventory[i].quantity);
            inventory[i].quantity -= realQ;
            updateStockEvent.Invoke();
            itemSelectionEvent.Invoke(inventory[i]);
            return inventory[i].quantity;
        }
        else
        {
            return 0;
        }
    }

    public int Consume(Item item, int quantity)
    {
        int i = inventory.FindIndex(x => x.name == item.name);
        if (i != -1)
        {
            int realQ = Mathf.Min(quantity, inventory[i].quantity);
            inventory[i].quantity -= realQ;
            updateStockEvent.Invoke();
            itemSelectionEvent.Invoke(inventory[i]);
            return inventory[i].quantity;
        }
        else
        {
            return 0;
        }
    }

    public Item GetInfo(int i)
    {
        return inventory[i];
    }
}
