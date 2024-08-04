using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Binds")]
    [SerializeField] KeyCode fire;
    [SerializeField] KeyCode reload;
    [Header("Properties")]
    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileOrigin;
    [SerializeField] Transform Player;
    [Header("Stats")]
    [SerializeField] float firerate;
    //[SerializeField] float projectileSpeed;
    [SerializeField] float shootForce, upwardForce;
    [SerializeField] bool gravity;
    [SerializeField] float gravityStrength;
    [SerializeField] float inaccuracy;
    [SerializeField] float reloadTime;
    [SerializeField] int maxAmmo;
    [SerializeField] int maxReserveAmmo;
    int currentAmmo;
    int reserveAmmo;
    bool isReloading, isShooting, readyToShoot;
    public bool allowInvoke = true;
    //RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        reserveAmmo = maxReserveAmmo;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputTrigger();
    }
    void Shoot(Vector3 direction)
    {
        isShooting = true;
        GameObject currentprojectile = Instantiate(projectile, projectileOrigin.position, Quaternion.identity, Player);
        currentprojectile.transform.forward = direction.normalized;
        currentprojectile.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
        currentprojectile.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        currentAmmo -= 1;
        if(allowInvoke)
        {
            Invoke("ResetShot", firerate);
            allowInvoke = false;
        }

    }
    void InputTrigger()
    {
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Input.GetKeyDown(fire) && readyToShoot == true && /*isReloading == false &&*/ currentAmmo != 0)
        {
            /*if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity))
            {
                Shoot(GetDir(fpsCam.transform.position, hit.transform.position));
            }
            else
            {*/
            Shoot(GetDir(fpsCam.transform.position, ray.GetPoint(75)));
            //}
        }
        if(Input.GetKeyDown(reload) && reserveAmmo != 0 && currentAmmo < maxAmmo && isReloading == false)
        {
            Reloading();
        }
    }
    Vector3 GetDir(Vector3 origin, Vector3 hit)
    {
        Vector3 direction = hit - origin;
        Vector3 directionWithSpread = new Vector3
        (
            direction.x + Random.Range(-inaccuracy, inaccuracy),
            direction.y + Random.Range(-inaccuracy, inaccuracy),
            direction.z + Random.Range(-inaccuracy, inaccuracy)
        );
        return directionWithSpread;
    }
    void ResetShot()
    {
        allowInvoke = true;
        readyToShoot = true;
        isShooting = false;
    }
    void Reloading()
    {
        //FindObjectOfType<AudioManager>().Play(reloadSoundName);
        Invoke("ReloadFinished", reloadTime);
    }
    void ReloadFinished()
    {
        if(reserveAmmo != 0 && currentAmmo < maxAmmo && isShooting == false)
        {
            reserveAmmo -= 1;
            currentAmmo++;
            isReloading = false;
        }
    }
    public int ReturnCurAmmo()
    {
        return currentAmmo;
    }
    public int ReturnReserveAmmo()
    {
        return reserveAmmo;
    }
    public int ReturnMaxReserveAmmo()
    {
        return maxReserveAmmo;
    }
    public void AmmoRefill(int amount)
    {
        reserveAmmo += amount;
        if(reserveAmmo >= maxReserveAmmo)
            reserveAmmo = maxReserveAmmo;
    }
}