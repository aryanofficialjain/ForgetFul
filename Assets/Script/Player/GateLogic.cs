using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GateLogic : MonoBehaviour
{
    private bool hasKey = false;

    [Header("Optional Small Gate Reference")]
    [SerializeField] private GameObject smallGate; // Drag your smallGate object here in Inspector

    public void CollectKey()
    {
        hasKey = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gate"))
        {
           
            {
                collision.gameObject.SetActive(false); // Open main gate
                StartCoroutine(LoadNextSceneAfterDelay(0.01f));
            }
           
        }
        else if (collision.gameObject.CompareTag("SmallGate"))
        {
            if (hasKey)
            {
                Debug.Log("Passing through small gate...");
                collision.gameObject.SetActive(false); // Temporarily disable small gate
                hasKey = false; // Reset key

                StartCoroutine(ReactivateSmallGate(collision.gameObject, 2f)); // Reactivate after 2 seconds
            }
            else
            {
                Debug.Log("You need a key to pass this small gate.");
            }
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("This is the last level.");
        }
    }

    private IEnumerator ReactivateSmallGate(GameObject gate, float delay)
    {
        yield return new WaitForSeconds(delay);
        // gate.SetActive(true);
    }
}