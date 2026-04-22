using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    private float endHeight;
    private float startHeight;
    public GameObject player;
    public float speed = 10f;
    private float endPoint;
    private float startPoint;
    private float distance;
    private float nextX;
    private float Yarc;
    private float currentHeight;
    private bool destinationReached = false;
    private Vector3 direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startHeight = transform.position.y;
        startPoint = transform.position.x;
        player = GameObject.FindWithTag("Player");
        endHeight = player.transform.position.y;
        endPoint = player.transform.position.x;
        distance = endPoint - startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < startHeight - 14 || transform.position.y > startHeight + 14 || transform.position.x > startPoint + 18 || transform.position.x < startPoint - 18)
        {
            Destroy(gameObject);
        }
        else if(!destinationReached)
        {
            nextX = Mathf.MoveTowards(transform.position.x, endPoint, speed * Time.deltaTime);
            // nextX determines where the x value of the projectile will go to next.
            Yarc = Mathf.Lerp(startHeight, endHeight, nextX - startPoint);
            // Yarc uses Lerp to make the projectile arc.
            // Yarc uses the nextX value to determine where in the arc the projectile should be.
            currentHeight = 2 * (nextX - startPoint) * (nextX - endPoint) / (-0.25f * distance * distance);
            // brings everything together to add the additional height to the arc itself.

            Vector3 movePosition = new Vector3(nextX, Yarc + currentHeight, transform.position.z);
            // uses vector3 instead of vector2 as unity does position movement through vector3 due to being a 3d engine

            transform.position = movePosition;
            
            if(transform.position.x == endPoint)
            {
                destinationReached = true;
                direction = movePosition;
            }
        }
        else
        {
            //transform.position = direction.normalized * speed * Time.deltaTime;
        }
    }
    
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Destroy(collision.gameObject);
    //     }
    // }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
