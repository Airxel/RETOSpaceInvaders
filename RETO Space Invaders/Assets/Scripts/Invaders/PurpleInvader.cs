using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleInvader : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 4;

    [SerializeField]
    private float points = 50f;

    [SerializeField]
    private int fastProjectileDamage = 1;

    [SerializeField]
    private int balancedProjectileDamage = 2;

    [SerializeField]
    private int slowProjectileDamage = 3;

    [SerializeField]
    private GameObject[] powerUps;

    [SerializeField]
    private int spawnChance = 25;

    [SerializeField]
    private int randomSpawn;

    [SerializeField]
    private int specialProjectileDamage = 5;

    [SerializeField]
    GameObject particles, newParticles;

    [SerializeField]
    private float particlesDelay = 0.5f;

    /// <summary>
    /// Función que controla las interacciones del invader con otros elementos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fast Projectile"))
        {
            FastProjectileHit();
        }
        else if (other.gameObject.CompareTag("Balanced Projectile"))
        {
            BalancedProjectileHit();
        }
        else if (other.gameObject.CompareTag("Slow Projectile"))
        {
            SlowProjectileHit();
        }
        else if (other.gameObject.CompareTag("Special Projectile"))
        {
            SpecialProjectileHit();
        }
    }

    /// <summary>
    /// Función que se activa cuando es golpeado por un proyectil de la nave rápida
    /// </summary>
    private void FastProjectileHit()
    {
        hitPoints = hitPoints - fastProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    /// <summary>
    /// Función que se activa cuando es golpeado por un proyectil de la nave equilibrada
    /// </summary>
    private void BalancedProjectileHit()
    {
        hitPoints = hitPoints - balancedProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    /// <summary>
    /// Función que se activa cuando es golpeado por un proyectil de la nave lenta
    /// </summary>
    private void SlowProjectileHit()
    {
        hitPoints = hitPoints - slowProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    private void SpecialProjectileHit()
    {
        hitPoints = hitPoints - specialProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    /// <summary>
    /// Función que controla si un invader va a hacer spawn a un power-up
    /// </summary>
    private void SpawnPowerUp()
    {
        randomSpawn = Random.Range(0, 100);

        if (randomSpawn < spawnChance)
        {
            int randomIndex = Random.Range(0, powerUps.Length);

            Instantiate(powerUps[randomIndex], this.transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Función que controla si un invader es eliminado
    /// </summary>
    private void DeadInvader()
    {
        SpawnPowerUp();

        SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);

        GameManager.instance.AddPoints(points);

        newParticles = Instantiate(particles, this.transform.position, Quaternion.identity);

        Destroy(newParticles, particlesDelay);

        this.gameObject.SetActive(false);
    }
}
