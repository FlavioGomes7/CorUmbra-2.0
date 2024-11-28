using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item ", order = 0)]
public class Item : ScriptableObject
{
    public ItemType Type;
    public string Id;
    public string Name;
    public string Description;
    public int Amount;

    public GameObject modelPrefab;
}
