using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    private Transform explosionHitbox;

    /// <summary>
    /// Función que controla el movimiento del proyectil especial
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
        if (other.gameObject.CompareTag("Top Wall") ||
            other.gameObject.CompareTag("Shelter") ||
            other.gameObject.CompareTag("Big Invader"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            // Se establece el hijo que controla la hitbox de la explosión
            explosionHitbox = this.transform.GetChild(0);

            // Se activa esa hitbox
            explosionHitbox.gameObject.SetActive(true);

            SoundsManager.instance.PlaySound(SoundsManager.instance.specialExplosionSound);

            // Se destruye el proyectil
            Destroy(this.gameObject, 0.025f);
        }
    }
}
