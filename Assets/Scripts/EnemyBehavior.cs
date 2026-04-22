using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchpoint;
    public float launchForce = 10f;
    public bool inRange = false;
    public float coolDown = 5f;
    private float readyFire = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && Time.time >= readyFire)
        {
            ShootProjectile();
            readyFire = Time.time + coolDown;
        }
    }

    void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, launchpoint.position, launchpoint.rotation);

        // Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        // if (rb != null)
        // {
        //     rb.AddForce(launchpoint.forward * launchForce);
        // }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
