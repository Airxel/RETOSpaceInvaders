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
            SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);

            GameManager.instance.AddPoints(points);
            this.gameObject.SetActive(false);
        }
    }

    private void BalancedProjectileHit()
    {
        hitPoints = hitPoints - balancedProjectileDamage;

        if (hitPoints <= 0)
        {
            SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);

            GameManager.instance.AddPoints(points);
            this.gameObject.SetActive(false);
        }
    }

    private void SlowProjectileHit()
    {
        hitPoints = hitPoints - slowProjectileDamage;

        if (hitPoints <= 0)
        {
            SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);

            GameManager.instance.AddPoints(points);
            this.gameObject.SetActive(false);
        }
    }
}
