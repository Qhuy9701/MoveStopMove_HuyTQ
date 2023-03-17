using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WeaponAsset", menuName = "ScriptableObjects/WeaponAsset", order = 1)]
public class WeaponDataAsset : ScriptableObject
{
    public List<WeaponData> lst;

    public GameObject GetWeaponByTpe(WeaponType type)
    {
        return lst.Find(x => x.type == type).weaponObj;
    }


}

[Serializable]
public class WeaponData
{
    public WeaponType type;
    public GameObject weaponObj;
}
