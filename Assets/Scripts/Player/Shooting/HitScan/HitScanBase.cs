using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanBase : MonoBehaviour
{
    [SerializeField] Transform camera;
    [Header("Stats")]
    [SerializeField] int pelletcount;   
    [SerializeField] bool mag;
    [SerializeField] float minDamage;
    [SerializeField] float maxDamage;
    [SerializeField] float firerate;
    [SerializeField] float falloff;
    [SerializeField] float maxDistance;
    float appliedDamage;
    float hsMultiplier = 3f;
    [SerializeField] int maxAmmo;
    [SerializeField] int maxReserveAmmo;
    [SerializeField] int currentAmmo;
    [SerializeField] int reserveAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] float reloadDelay;
    [SerializeField] float inaccuracy;
    [Header("Objets")]
    [SerializeField] GameObject muzzle;
    [SerializeField] ParticleSystem muzzleflash;
    [SerializeField] GameObject bHole;
    [SerializeField] string fireSoundName;
    [SerializeField] string reloadSoundName;
    [SerializeField] string triggerSoundName;
    [Header("Keys")]
    [SerializeField] KeyCode fire;
    [SerializeField] KeyCode reload;
    [Header("Bool")]
    [SerializeField] bool isReloading, isShooting, readyToShoot;
    RaycastHit hit;
    HealthManager enemyHealth;
    void Start()
    {
        bool readyToShoot = true;
        bool isReloading = false;
        bool isShooting = false;
        currentAmmo = maxAmmo;
        reserveAmmo = maxReserveAmmo;
    }
    void Update()
    {
        if(Input.GetKeyDown(fire) && readyToShoot == true && isReloading == false && currentAmmo > 0)
        {
            isShooting = true;
            int i = 1;
            FindObjectOfType<AudioManager>().Play(fireSoundName);
            if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance))
                {
                    if(hit.collider.GetComponent<TeamAssign>() != null)
                    {
                        if(hit.collider.GetComponent<TeamAssign>().ReturnTeam() != this.transform.parent.parent.GetComponent<TeamAssign>().ReturnTeam())
                        DealDamage(hit);
                    }
                }
            while(i < pelletcount)
            {
                if(Physics.Raycast(camera.transform.position, GetShootingDirection(), out hit, maxDistance))
                {
                    if(hit.collider.GetComponent<TeamAssign>() != null)
                    {
                        if(hit.collider.GetComponent<TeamAssign>().ReturnTeam() != this.transform.parent.parent.GetComponent<TeamAssign>().ReturnTeam())
                        DealDamage(hit);
                    }
                }
                i++;
            }
            muzzleflash.Play();
            currentAmmo -= 1;
            if(readyToShoot)
            {
                Invoke("ResetShot", firerate);
                readyToShoot = false;
            }
        }
        if(Input.GetKeyDown(fire) && currentAmmo <= 0)
            {
                FindObjectOfType<AudioManager>().Play(triggerSoundName);
            }
        if(Input.GetKeyDown(reload) && reserveAmmo != 0 && currentAmmo != maxAmmo)
        {
            isShooting = false;
            Reloading();
        }
    }
    void ResetShot()
    {
        readyToShoot = true;
    }
    void Reloading()
    {
        FindObjectOfType<AudioManager>().Play(reloadSoundName);
        if(mag == true)
            isReloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    void ReloadFinished()
    {
        if(mag == true && reserveAmmo != 0 && currentAmmo < maxAmmo)
        {
            reserveAmmo -= maxAmmo - currentAmmo;
            currentAmmo += Mathf.Clamp(reserveAmmo, 0, maxAmmo);
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
            if(reserveAmmo < 0)
                reserveAmmo = 0;
            isReloading = false;
        }
        else if(reserveAmmo != 0 && currentAmmo < maxAmmo && isShooting == false)
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
    public void DealDamage(RaycastHit hit)
    {
        enemyHealth = hit.transform.gameObject.GetComponent<HealthManager>();
        float distance = (hit.point - camera.position).magnitude;
        appliedDamage = Mathf.Clamp((maxDamage - distance * falloff), minDamage, maxDamage);
        if(enemyHealth != null)
        {
            print(appliedDamage);
            enemyHealth.TakeDamage(System.Convert.ToInt32(appliedDamage));
        }
    }
    Vector3 GetShootingDirection()
    {
        Vector3 targetPos = camera.transform.position + camera.transform.forward;
        targetPos = new Vector3
        (
            targetPos.x + Random.Range(-inaccuracy, inaccuracy),
            targetPos.y + Random.Range(-inaccuracy, inaccuracy),
            targetPos.z + Random.Range(-inaccuracy, inaccuracy)
        );
        Vector3 direction = targetPos - camera.transform.position;
        return direction;
    }
}
