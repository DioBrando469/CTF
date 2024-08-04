using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] Transform camera;
    [Header("Stats")]
    [SerializeField] float maxDamage;
    [SerializeField] float minDamage;
    [SerializeField] float firerate;
    [SerializeField] float falloff;
    [SerializeField] float maxDistance;
    //[SerializeField] double minDistance;
    [SerializeField] int pelletcount;
    double appliedDamage;
    //  float fall;
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
    float reloadDelayTimer = 0;
    RaycastHit hit;
    /*RaycastHit hit1;
    RaycastHit hit2;
    RaycastHit hit3;
    RaycastHit hit4;
    RaycastHit hit5;
    RaycastHit hit6;
    RaycastHit hit7;
    RaycastHit hit8;
    RaycastHit hit9;*/
    HealthManager enemyHealth;
    double damageoutput = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(fire) && shottimer >= firerate && reloadtimer >= reloadTime && currentAmmo != 0)
        {
            int i = 1;
            damageoutput = 0;
            FindObjectOfType<AudioManager>().Play("Shot");
            if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxDistance))
                {
                    DealDamage(hit);
                    Debug.DrawRay(transform.position, transform.forward, Color.red, 10);
                }
            while(i < pelletcount)
            {
                if(Physics.Raycast(camera.transform.position, GetShootingDirection(), out hit, maxDistance))
                {
                    DealDamage(hit);
                    Debug.DrawRay(transform.position, GetShootingDirection(), Color.green, 10);
                }
                i++;
            }
            print(damageoutput);
            muzzleflash.Play();
            
            damageoutput = 0;
            shottimer = 0;
            currentAmmo -= 1;
        }
        else if(Input.GetKeyDown(fire) && currentAmmo <= 0)
            {
                FindObjectOfType<AudioManager>().Play("Trigger");
            }
        else if(Input.GetKeyDown(reload) && reserveAmmo != 0)
        {
            if(currentAmmo < maxAmmo && reloadDelayTimer < 0)
            {
                reloadDelayTimer = reloadDelay;
                Invoke("Reload", 0.5f);
            }
        }
        shottimer += Time.deltaTime;
        reloadtimer += Time.deltaTime;
        reloadDelayTimer -= Time.deltaTime;
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
            damageoutput += appliedDamage;
            enemyHealth.TakeDamage(System.Convert.ToInt32(appliedDamage));
        }
    }
    public void Reload()
    {
        if(currentAmmo < maxAmmo && reserveAmmo > 0)
        {
            FindObjectOfType<AudioManager>().Play("Reload");
            
            reloadtimer = 0;
            reserveAmmo -= 1;
            currentAmmo += 1;
            if(currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;
            if(reserveAmmo < 0)
                reserveAmmo = 0;
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
        /*Vector3 targetPos = camera.transform.forward;
        Quaternion spreadAngle = Quaternion.AngleAxis(Random.Range(-inaccuracy, inaccuracy), Vector3.right);
        Vector3 direction = spreadAngle + targetPos;*/
        return direction;
    }
}
