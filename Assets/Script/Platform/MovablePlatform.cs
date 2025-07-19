using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool startMovingRight = true;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private bool movingRight;

    private void Start()
    {
        startPosition = transform.position;
        movingRight = startMovingRight;
        SetTargetPosition();
        lastPosition = transform.position;
    }

    private void Update()
    {
        // Move the platform
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Switch direction at target
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingRight = !movingRight;
            SetTargetPosition();
        }
    }

    private void LateUpdate()
    {
        // Store current position for next frame
        lastPosition = transform.position;
    }

    private void SetTargetPosition()
    {
        float direction = movingRight ? 1f : -1f;
        targetPosition = startPosition + new Vector3(direction * moveDistance, 0f, 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Move player with platform based on delta
            Vector3 deltaMovement = transform.position - lastPosition;
            collision.transform.position += deltaMovement;
        }
    }
}