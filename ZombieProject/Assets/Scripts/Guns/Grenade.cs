using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GunScriptable grenadeScriptable;
    [SerializeField] private float delay;
    [SerializeField] private GameObject grenadeExplosion;
    [SerializeField] private float radius;
    [SerializeField] private float explosionForce;
    [SerializeField] private bool showRaidus;

    private float countdown;
    private bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        //Efecto
        GameObject explosionClone = Instantiate(grenadeExplosion, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }

            if (nearbyObject.gameObject.CompareTag("Player"))
            {
                nearbyObject.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            }

            if (nearbyObject.gameObject.CompareTag("zombie"))
            {
                nearbyObject.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(grenadeScriptable.damage);
            }
        }

        Destroy(explosionClone, 2f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (showRaidus)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}
