using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<PlayerWeapon> playerWeaponList;
    private Transform playerTrans;

    public void Init()
    {
        this.playerTrans = GameObject.Find("Player").transform;
        this.playerWeaponList = new List<PlayerWeapon>();
        this.SpawnPlayerWeapon(2000);
        //this.playerWeaponList;
    }

    private void SpawnPlayerWeapon(int id)
    {
        var weaponData = DataManager.instance.GetData<WeaponData>(id);
        GameObject weaponGo = Instantiate(Resources.Load<GameObject>(weaponData.prefab_name), Vector3.zero, Quaternion.identity);
        var weapon = weaponGo.GetComponent<PlayerWeapon>();
        Debug.Log("ddddddd : " + playerTrans);
        weapon.Init(weaponData, playerTrans);
    }
    public void WeaponUpgrade(int id)
    {
        PlayerWeapon weapon = this.playerWeaponList.Find(x => x.id == id);
        if (weapon == null)
        {
            this.SpawnPlayerWeapon(id);
            this.playerWeaponList.Add(weapon);
        }
        else
        {
            weapon.Upgrade();
        }
    }
}
