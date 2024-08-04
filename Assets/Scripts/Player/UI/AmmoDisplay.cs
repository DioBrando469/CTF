using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] AmmoManager weapon;
    [SerializeField] GameObject currentWeapon;
    PlayerPewPew currentGun;
    [SerializeField] TextMeshProUGUI currentAmmo;
    [SerializeField] int curAmmo;
    [SerializeField] TextMeshProUGUI maxAmmo;
    [SerializeField] int mAmmo;

    void Start()
    {
        weapon = Player.GetComponent<AmmoManager>();
    }
    void Update()
    {
        GetInfo();
        currentAmmo.text = System.Convert.ToString(curAmmo);
        maxAmmo.text = System.Convert.ToString(mAmmo);
    }
    void GetInfo()
    {
        //currentGun = weapon.curWeapon;
        if(weapon != null)
        {
            curAmmo = weapon.ReturnCurrentAmmo();
            mAmmo = weapon.ReturnCurrentReserveAmmo();
        }
    }
}
