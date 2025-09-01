using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 camFollow;
    private Transform playerBall, winPlatform;

    private void Awake()
    {
        playerBall = FindAnyObjectByType<PlayerBall>().transform;   // Will find object which has this scrit attched to it
    }

    private void Update()
    {
        // finding winPlatform like this because it will be instantiated after game starts
        if (winPlatform == null)
            winPlatform = GameObject.Find("Win(Clone)").GetComponent<Transform>();

        if (transform.position.y > playerBall.transform.position.y && transform.position.y > winPlatform.position.y + 2f)
            camFollow = new Vector3(transform.position.x, playerBall.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, camFollow.y, -5);
    }
}
