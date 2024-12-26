using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    private void Update()
    {
        this.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fast Ship"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Balanced Ship"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Slow Ship"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Shelter"))
        {
            Destroy(this.gameObject);
        }
    }
}
