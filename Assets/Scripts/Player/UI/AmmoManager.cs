using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] GameObject curWeapon;
    [SerializeField] WeaponSwitch weaponswitch;
    HitScanBase hitscan;
    ProjectileBase projectile;
    void Update()
    {
        curWeapon = AssignCurWeapon(curWeapon);
        hitscan = curWeapon.GetComponent<HitScanBase>();
        projectile = curWeapon.GetComponent<ProjectileBase>();
    }
    public int ReturnCurrentAmmo()
    {
        if(projectile != null)
        {
            return projectile.ReturnCurAmmo();
        }
        if(hitscan != null)
        {
            return hitscan.ReturnCurAmmo();
        }
        else
        {
            return 0;
        }
    }
    public int ReturnCurrentReserveAmmo()
    {
        if(hitscan != null)
        {
            return hitscan.ReturnReserveAmmo();
        }
        if(projectile != null)
        {
            return projectile.ReturnReserveAmmo();
        }
        else
        {
            return 0;
        }
    }
    /*public int ReturnCurrentMaxAmmo()
    {
        if(beretta != null)
        {
            return beretta.ReturnMaxReserveAmmo();
        }
        if(shotty != null)
        {
            return shotty.ReturnMaxReserveAmmo();
        }
        else
        {
            return 0;
        }
    }*/
    public GameObject AssignCurWeapon(GameObject curweapon)
    {
        if(curWeapon != weaponswitch.ReturnCurWeapon())
        {
            curWeapon = weaponswitch.ReturnCurWeapon();
        }
        return curWeapon;
    }
}
