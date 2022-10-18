using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<PlayerWeapon> playerWeaponList;

    private Transform playerTrans;

    public void Init(int id)
    {
        this.playerTrans = GameObject.Find("Player").transform;
        this.playerWeaponList = new List<PlayerWeapon>();

        this.SpawnPlayerWeapon(id);
    }

    private void SpawnPlayerWeapon(int id)
    {
        var weaponData = DataManager.instance.GetData<WeaponData>(id);
        GameObject weaponGo = Instantiate(Resources.Load<GameObject>(weaponData.prefab_name), Vector3.zero, Quaternion.identity);
        var weapon = weaponGo.GetComponent<PlayerWeapon>();
        weapon.Init(weaponData, playerTrans);
        this.playerWeaponList.Add(weapon);
        weaponGo.transform.parent = this.transform;
    }
    public void WeaponUpgrade(int id)
    {
        var weapon = this.playerWeaponList.Find(x => x.id == id);
        if (weapon == null)
        {
            this.SpawnPlayerWeapon(id);
        }
        else
        {
            weapon.Upgrade();
        }
    }
    public int GetWeaponLevel(int id)
    {
        int level = 0;
        var weapon = this.playerWeaponList.Find(x => x.id == id);
        if (weapon != null)
        {
            level = weapon.level;
        }

        return level;
    }
}
