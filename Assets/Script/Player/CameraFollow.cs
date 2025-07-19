using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    [Header("Dead Zone Offsets")]
    public float horizontalOffset = 2f;
    public float verticalOffset = 1.5f;


    void Start()
    {
        transform.position = new Vector3(0.7f, 3.21f, -10f);
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentPosition;

        // Horizontal dead zone logic
        float playerX = player.position.x;
        float leftBound = currentPosition.x - horizontalOffset;
        float rightBound = currentPosition.x + horizontalOffset;

        if (playerX > rightBound)
            targetPosition.x = playerX - horizontalOffset;
        else if (playerX < leftBound)
            targetPosition.x = playerX + horizontalOffset;

        // Lock Y position to 3.21
        targetPosition.y = 3.21f;

        // Lock Z position to -10 (optional)
        targetPosition.z = -10f;


        // Ensure camera doesn't move left beyond -0.23
targetPosition.x = Mathf.Max(targetPosition.x, 0.7f);

        // Smoothly follow
        transform.position = Vector3.Lerp(currentPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }
}