
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;


public class CameraMove : MonoBehaviour
{

    public Transform playerPosition;
    public PlayerMovement playerScript;
    public Transform cameraPosition;

    public float localPosX;
    public float localPosY;

    [Header("Bounds")]
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;
    public float screenWidth;
    public float screenHeight;

    [Header("Smooth Camera Movement")]
    public Transform target; // The player to follow
    public float smoothTime = 0.3f; // Time taken to reach the target
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset; // Set this in the inspector (e.g., 0, 0, -10)

    






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        cameraPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        localPosX = playerPosition.position.x - target.position.x;
        localPosY = playerPosition.position.y - target.position.y;

        if (localPosX > rightBound)
        {            
            target.position = new UnityEngine.Vector3(target.position.x + screenWidth, target.position.y, target.position.z);
        }
        else if (localPosX < leftBound)
        {
            target.position = new UnityEngine.Vector3(target.position.x - screenWidth,  target.position.y, target.position.z);
        }

        if (localPosY > topBound)
        {
            target.position = new UnityEngine.Vector3(target.position.x, target.position.y + screenHeight, target.position.z);
        }
        else if (localPosY < bottomBound)
        {
            target.position = new UnityEngine.Vector3(target.position.x, target.position.y - screenHeight, target.position.z);
        }
    }

    void LateUpdate() 
    {
        if(!playerScript.isDie)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            StartCoroutine(AnimationWait());
            
        }
    }

    IEnumerator AnimationWait()
    {
        yield return new WaitForSeconds(1f);
        Vector3 targetPosition = new Vector3(0, 0, -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0f);
    }
}
