using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    /// <summary>
    /// Función que controla el movimiento del proyectil de los invaders
    /// </summary>
    private void Update()
    {
        this.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Función que controla las interacciones del proyectil con otros elementos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fast Ship") ||
            other.gameObject.CompareTag("Balanced Ship") ||
            other.gameObject.CompareTag("Slow Ship") ||
            other.gameObject.CompareTag("Shelter") ||
            other.gameObject.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
