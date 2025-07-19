using UnityEngine;

public class BladeRotatorClockWise : MonoBehaviour
{
    public float rotationSpeed = 200f; // degrees per second

    void Update()
    {
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime); // Clockwise on Z-axis
    }
}