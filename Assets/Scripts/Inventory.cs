using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float cash;

    List<Item> items;
    // while -1 then inventory is unlimited
    public int inventorySize=-1;

    // ITEMS FUNCS
    public bool addItem(Item i)
    {
        if (inventorySize == -1 || inventorySize > items.Count + 1)
        {
            items.Add(i);
            return true;
        }
        return false;
    }

    public bool removeItem(Item i)
    {
        return items.Remove(i);
    }

    public bool removeItem(int i)
    {
        if (i > items.Count)
        {
            Debug.LogWarning("List<Items> doesn't contain" + i + " item");
            return false;
        }
        items.RemoveAt(i);
        return true;
    }

    public void removeAllItems()
    {
        items.Clear();
    }

    public Item getItem(int i)
    {
        if(i>items.Count)
        {
            Debug.LogWarning("List<Items> doesn't contain" + i + " item");
            return null;
        }
        else
        {
            return items[i];
        }
    }

    public List<Item> getItems()
    {
        return items;
    }

    public int findItem(string name)
    {
        for(int i=0; i<items.Count; i++)
        {
            if(items[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
}
