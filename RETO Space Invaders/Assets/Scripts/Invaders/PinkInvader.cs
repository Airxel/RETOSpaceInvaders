using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkInvader : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 3;

    [SerializeField]
    private float points = 30f;

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
    }

    private void FastProjectileHit()
    {
        hitPoints = hitPoints - fastProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    private void BalancedProjectileHit()
    {
        hitPoints = hitPoints - balancedProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    private void SlowProjectileHit()
    {
        hitPoints = hitPoints - slowProjectileDamage;

        if (hitPoints <= 0)
        {
            DeadInvader();
        }
    }

    private void SpawnPowerUp()
    {
        randomSpawn = Random.Range(0, 100);

        if (randomSpawn < spawnChance)
        {
            int randomIndex = Random.Range(0, powerUps.Length);

            Instantiate(powerUps[randomIndex], this.transform.position, Quaternion.identity);
        }
    }

    private void DeadInvader()
    {
        SpawnPowerUp();

        SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);
        GameManager.instance.AddPoints(points);

        this.gameObject.SetActive(false);
    }
}
