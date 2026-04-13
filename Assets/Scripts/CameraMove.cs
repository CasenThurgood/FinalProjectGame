using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform playerPosition;
    public Transform cameraPosition;

    public float localPosX;
    public float localPosY;

    [Header("Bounds")]
    public float leftBound;
    public float rightBound;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        cameraPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        localPosX = playerPosition.position.x - cameraPosition.position.x;
        localPosY = playerPosition.position.y - cameraPosition.position.y;

        if (localPosX > rightBound)
        {
            cameraPosition.position = new Vector3(cameraPosition.position.x + 21.6f, cameraPosition.position.y, cameraPosition.position.z);
        }
        else if (localPosX < leftBound)
        {
            cameraPosition.position = new Vector3(cameraPosition.position.x - 21.6f, cameraPosition.position.y, cameraPosition.position.z);
        }
    }
}
