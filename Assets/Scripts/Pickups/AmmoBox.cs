using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    HitScanBase playerHS;
    ProjectileBase playerPJ;
    int weaponmaxammo;
    [SerializeField] int ammorestore = 2;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "PlayerGun")
        {
            playerHS = collision.gameObject.GetComponent<HitScanBase>();
            playerPJ = collision.gameObject.GetComponent<ProjectileBase>();
            if(playerHS != null)
            {
                weaponmaxammo = playerHS.ReturnMaxReserveAmmo();
                playerHS.AmmoRefill(weaponmaxammo / ammorestore);
                Destroy(gameObject);
            }
            else if(playerPJ != null)
            {
                weaponmaxammo = playerPJ.ReturnMaxReserveAmmo();
                playerPJ.AmmoRefill(weaponmaxammo / ammorestore);
                Destroy(gameObject);
            }
        }
    }
}
