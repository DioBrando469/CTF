using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5 : HitScanBase
{
    [SerializeField] Transform playerCamera;
    [Header("Stats")]
    [SerializeField] double minDamage;
    [SerializeField] double maxDamage;
    [SerializeField] float firerate;
    [SerializeField] double falloff;
    [SerializeField] float maxDistance;
    [SerializeField] double minDistance;
    double appliedDamage;
    [SerializeField] bool fall;
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
    float shottimer = 0;
    float reloadtimer = 0;
    RaycastHit hit;
    EnemyHealth enemyHealth;

    void Update()
    {
        if(Input.GetKeyDown(fire) && shottimer >= firerate && reloadtimer >= reloadTime && currentAmmo != 0)
        {
            FindObjectOfType<AudioManager>().Play(fireSoundName);
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity))
            {
                DealDamage();
            }
            muzzleflash.Play();
            shottimer = 0;
            currentAmmo -= 1;
        }
        else if(Input.GetKey(fire) && shottimer >= firerate && reloadtimer >= reloadTime && currentAmmo != 0)
        {
            FindObjectOfType<AudioManager>().Play(fireSoundName);
            if(Physics.Raycast(playerCamera.transform.position, GetShootingDirection(), out hit, Mathf.Infinity))
            {
                DealDamage();
            }
            muzzleflash.Play();
            shottimer = 0;
            currentAmmo -= 1;
        }
        else if(Input.GetKeyDown(fire) && currentAmmo <= 0)
            {
                FindObjectOfType<AudioManager>().Play(triggerSoundName);
            }
        else if(Input.GetKeyDown(reload) && reserveAmmo != 0)
        {
            FindObjectOfType<AudioManager>().Play(reloadSoundName);
            reloadtimer = 0;
            reserveAmmo -= maxAmmo - currentAmmo;
            currentAmmo += Mathf.Clamp(reserveAmmo, 0, maxAmmo);
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
            if(reserveAmmo < 0)
                reserveAmmo = 0;
            
        }
        shottimer += Time.deltaTime;
        reloadtimer += Time.deltaTime;
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
    public void DealDamage()
    {
        enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();
        if(fall == true)
        {
            var distance = (hit.point - playerCamera.position).magnitude;
            if(distance >= minDistance)
            {
                appliedDamage = maxDamage - (distance - minDistance) * falloff;
            }
            else
            {
                appliedDamage = maxDamage;
            }
            if(appliedDamage >= maxDamage)
            {
                appliedDamage = maxDamage;
            }
            else if(appliedDamage <= minDamage)
            {
                appliedDamage = minDamage;
            }
        }
        else
        {
            appliedDamage = maxDamage;
        }
        if(enemyHealth != null)
        {
            print(appliedDamage);
            enemyHealth.TakeDamage(System.Convert.ToInt32(appliedDamage));
        }
    }
    Vector3 GetShootingDirection()
    {
        Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward;
        targetPos = new Vector3
        (
            targetPos.x + Random.Range(-inaccuracy, inaccuracy),
            targetPos.y + Random.Range(-inaccuracy, inaccuracy),
            targetPos.z + Random.Range(-inaccuracy, inaccuracy)
        );
        Vector3 direction = targetPos - playerCamera.transform.position;
        return direction.normalized;
    }
}
