using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<PlayerWeapon> playerWeaponList;

    public void Init()
    {
        this.SpawnPlayerWeapon(2000);
        //this.playerWeaponList;
    }

    public void SpawnPlayerWeapon(int id)
    {
        var weaponData = DataManager.instance.GetData<WeaponData>(id);
        GameObject weaponGo = Instantiate(Resources.Load<GameObject>(weaponData.prefab_name), Vector3.zero, Quaternion.identity);
        var weapon = weaponGo.GetComponent<PlayerWeapon>();
        //weapon.Init();
    }
}
