using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayerPewPew : MonoBehaviour
{
    [SerializeField] Transform camera;
    [Header("Stats")]
    [SerializeField] float minDamage;
    [SerializeField] float maxDamage;
    [SerializeField] float firerate;
    [SerializeField] float falloff;
    //[SerializeField] float maxDistance;
    //[SerializeField] float minDistance;
    double appliedDamage;
    float fall;
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
    
    [Header("Keys")]
    [SerializeField] KeyCode fire;
    [SerializeField] KeyCode reload;
    float shottimer = 0;
    float reloadtimer = 0;
    RaycastHit hit;
    HealthManager enemyHealth;

    void Start()
    {
        currentAmmo = maxAmmo;
        reserveAmmo = maxReserveAmmo;
    }

    void Update()
    {
        if(Input.GetKeyDown(fire) && shottimer >= firerate && reloadtimer >= reloadTime && currentAmmo != 0)
        {
            FindObjectOfType<AudioManager>().Play("Shot");
            if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity))
            {
                DealDamage();
            }
            
            muzzleflash.Play();
            //Instantiate(muzzleflash, muzzle.transform.position, Quaternion.LookRotation(forward));
            print("pew");
            shottimer = 0;
            currentAmmo -= 1;
        }
        else if(Input.GetKeyDown(fire) && currentAmmo <= 0)
            {
                FindObjectOfType<AudioManager>().Play("Trigger");
            }
        else if(Input.GetKeyDown(reload) && reserveAmmo != 0)
        {
            FindObjectOfType<AudioManager>().Play("Reload");
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
        enemyHealth = hit.transform.gameObject.GetComponent<HealthManager>();
        float distance = (hit.point - camera.position).magnitude;
        appliedDamage = Mathf.Clamp((maxDamage - distance * falloff), minDamage, maxDamage);
        if(enemyHealth != null)
        {
            print(appliedDamage);
            enemyHealth.TakeDamage(System.Convert.ToInt32(appliedDamage));
        }
    }
}