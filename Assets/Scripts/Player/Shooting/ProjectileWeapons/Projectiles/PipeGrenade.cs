using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGrenade : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] float coef;
    [SerializeField] float power;
    [SerializeField] float radius;
    [SerializeField] float verticalPower;
    [SerializeField] float damage;
    [SerializeField] float explodeTime;
    
    HealthManager health;
    void Start()
    {
        Invoke("Explode", explodeTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Player") && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy"))
        {
            Explode();
        }
    }
    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, verticalPower);
            health = hit.GetComponent<HealthManager>();
            if (health != null)
            {
                health.TakeDamage(System.Convert.ToInt32(damage - (Vector3.Distance(transform.position, hit.transform.position) * coef)));
            }
            else if(hit.gameObject == this.transform.parent)
            {
                health.TakeDamage(System.Convert.ToInt32((damage - (Vector3.Distance(transform.position, hit.transform.position)) * coef) * 0.7f));
            }
        }
        Destroy(gameObject);
    }
}
