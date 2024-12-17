using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelector : MonoBehaviour
{
    [SerializeField]
    private WeaponType Weapon;
    [SerializeField]
    private Transform WeaponParent;
    [SerializeField]
    private List<WeaponScripitableObject> Weapons;
    //[SerializeField]
    //private PlayerIK InverseKinematics;

    [Space]
    [Header("Runtime Filled")]
    public WeaponScripitableObject ActiveWeapon;

    public void Start()
    {
        WeaponScripitableObject weapon = Weapons.Find(weapon => weapon.type == Weapon);

        if (weapon == null)
        {
            Debug.LogError($"No WeaponScripitableObject found for GunType: {weapon}");
            return;
        }

        ActiveWeapon = weapon;
        weapon.Spawn(WeaponParent, this);

        Transform[] allChildren = WeaponParent.GetComponentsInChildren<Transform>();
        //InverseKinematics
    }
}
