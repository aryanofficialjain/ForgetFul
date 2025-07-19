using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerReset : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Danger"))
        {
            Debug.Log("Player touched danger. Resetting position.");
            transform.position = respawnPoint.position;
            SceneManager.LoadScene(0);

        }
    }
}