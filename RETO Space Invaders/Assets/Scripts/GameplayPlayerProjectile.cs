using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    /// <summary>
    /// Función que controla el movimiento del proyectil del jugador
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
        if (other.gameObject.CompareTag("Enemy") || 
            other.gameObject.CompareTag("Top Wall") || 
            other.gameObject.CompareTag("Shelter") ||
            other.gameObject.CompareTag("Big Invader"))
        {
            Destroy(this.gameObject);
        }
    }
}
