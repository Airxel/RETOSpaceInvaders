using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fast Ship"))
        {
            Debug.Log("Fast Ship Selected");
        }
        else if (collision.gameObject.CompareTag("Balanced Ship"))
        {
            Debug.Log("Balanced Ship Selected");
        }
        else if (collision.gameObject.CompareTag("Slow Ship"))
        {
            Debug.Log("Slow Ship Selected");
        }

        Destroy(this.gameObject);

    }
}
