using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    [SerializeField] Transform camera;
    RaycastHit hit;
    EnemyHealth enemyHealth;
    [Header("Stats")]
    [SerializeField] float swingSpeed;
    [SerializeField] float swingRecharge;
    [SerializeField] int swingdamage;
    [SerializeField] float reach;
    float swingtimer;
    float swingspeedtimer;
    [Header("Binds")]
    [SerializeField] KeyCode fire;
    //[Header("Sounds")]

    // Start is called before the first frame update
    void Start()
    {
        
        //swingtimer = swingRecharge;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(fire) && swingtimer <= 0)
        {
            Invoke("Swing", swingSpeed);
            //swingtimer = swingRecharge;
        }
        //swingtimer -= Time.deltaTime;
    }
    public void MeleeDamage(int damage)
    {
        enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            print(damage);
            enemyHealth.TakeDamage(damage);
        }
    }
    void Swing()
    {
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, reach))
        {
            MeleeDamage(swingdamage);
        }
    }
}
