using Unity.VisualScripting;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private CameraMove cameraScript;
    
    private float screenWidth;
    private float screenHeight;

    [SerializeField] private Animator animator;

    void Start()
    {
        screenWidth = Mathf.Abs(cameraScript.leftBound) + Mathf.Abs(cameraScript.rightBound);
        screenHeight = Mathf.Abs(cameraScript.topBound) + Mathf.Abs(cameraScript.bottomBound);
    }

    // Update is called once per frame
    void Update()
    {
        moveCanvas();
    }

    private void moveCanvas()
    {
        float correctedX = (cameraScript.localPosX + screenWidth/2) / (screenWidth) * Screen.width + 5;
        float correctedY = (cameraScript.localPosY + screenHeight/2) / (screenHeight) * Screen.height - 5;
        transform.position = new Vector3(correctedX, correctedY, transform.position.z);

        if (playerScript.isDie)
        {
            animator.SetBool("Dead", true);
        }
        else
        {
            animator.SetBool("Dead", false);
        }
    }
}
