using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] KeyCode primarySwitch;
    [SerializeField] KeyCode secondarySwitch;
    [SerializeField] KeyCode meleeSwitch;
    [SerializeField] KeyCode switchother;
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public GameObject meleeWeapon;
    GameObject currentWeapon;

    void Start()
    {
        currentWeapon = primaryWeapon;
        primaryWeapon.SetActive(true);
        secondaryWeapon.SetActive(false);
    }
    void Update()
    {
        
        if(Input.GetKeyDown(primarySwitch) && currentWeapon != primaryWeapon)
        {
            SwitchWeapons(primaryWeapon);
            currentWeapon = primaryWeapon;
        }
        if(Input.GetKeyDown(secondarySwitch) && currentWeapon != secondaryWeapon)
        {
            SwitchWeapons(secondaryWeapon);
            currentWeapon = secondaryWeapon;
        }
        if(Input.GetKeyDown(meleeSwitch) && currentWeapon != meleeWeapon)
        {
            SwitchWeapons(meleeWeapon);
            currentWeapon = meleeWeapon;
        }
    }
    void SwitchWeapons(GameObject weapon)
    {
        primaryWeapon.SetActive(false);
        secondaryWeapon.SetActive(false);
        meleeWeapon.SetActive(false);
        weapon.SetActive(true);
    }
    public GameObject ReturnCurWeapon()
    {
        return currentWeapon;
    }
}
