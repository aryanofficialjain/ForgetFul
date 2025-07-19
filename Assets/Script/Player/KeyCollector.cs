using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    private GateLogic gateLogic;

    private void Awake()
    {
        gateLogic = GetComponent<GateLogic>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            if (gateLogic != null)
            {
                gateLogic.CollectKey();
                Destroy(collision.gameObject);
            }
        }
    }
}