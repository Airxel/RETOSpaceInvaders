using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BigInvader : MonoBehaviour
{
    [SerializeField]
    private float points = 100f;

    [SerializeField]
    GameObject particles, newParticles;

    [SerializeField]
    private float particlesDelay = 0.5f;

    /// <summary>
    /// Funci�n que controla las interacciones del big invader con otros elementos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Right Wall") ||
            other.gameObject.CompareTag("Left Wall"))
        {
            if (InvadersManager.instance.rightSpawn)
            {
                BigInvaderHitRight();
            }
            else
            {
                BigInvaderHitLeft();
            }
        }
        if (other.gameObject.CompareTag("Fast Projectile") ||
            other.gameObject.CompareTag("Balanced Projectile") ||
            other.gameObject.CompareTag("Slow Projectile") ||
            other.gameObject.CompareTag("Special Projectile"))
        {
            SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);

            GameManager.instance.AddPoints(points);

            newParticles = Instantiate(particles, this.transform.position, Quaternion.identity);

            Destroy(newParticles, particlesDelay);

            if (InvadersManager.instance.rightSpawn)
            {
                BigInvaderHitRight();
            }
            else
            {
                BigInvaderHitLeft();
            }
        }
    }

    /// <summary>
    /// Funci�n que controla si el big invader es eliminado y cambia su spawn a la izquierda
    /// </summary>
    private void BigInvaderHitRight()
    {
        Destroy(this.gameObject);

        InvadersManager.instance.newBigInvader = null;

        InvadersManager.instance.rightSpawn = false;
    }

    /// <summary>
    /// Funci�n que controla si el big invader es eliminado y cambia su spawn a la derecha
    /// </summary>
    private void BigInvaderHitLeft()
    {
        Destroy(this.gameObject);

        InvadersManager.instance.newBigInvader = null;

        InvadersManager.instance.rightSpawn = true;
    }
}
