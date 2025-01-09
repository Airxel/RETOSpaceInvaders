using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpecialProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    private Transform explosionHitbox;

    [SerializeField]
    GameObject explosion, newExplosion;

    [SerializeField]
    private float explosionDelay = 0.25f;

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

            newExplosion = Instantiate(explosion, this.transform.position, Quaternion.identity);

            Destroy(newExplosion,explosionDelay);

            SoundsManager.instance.PlaySound(SoundsManager.instance.specialExplosionSound);

            // Se destruye el proyectil
            Destroy(this.gameObject, 0.025f);
        }
    }
}
