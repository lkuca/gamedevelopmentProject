using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum WeaponType
    {
        Null,
        weapons_0,
        weapons_1
    }

    public WeaponType weaponType;
}
