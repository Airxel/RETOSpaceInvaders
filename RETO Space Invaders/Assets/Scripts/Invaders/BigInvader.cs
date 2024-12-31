using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigInvader : MonoBehaviour
{
    [SerializeField]
    private float points = 100f;

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
            other.gameObject.CompareTag("Slow Projectile"))
        {
            SoundsManager.instance.PlaySound(SoundsManager.instance.invaderDeadSound);

            GameManager.instance.AddPoints(points);

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

    private void BigInvaderHitRight()
    {
        Destroy(this.gameObject);

        InvadersManager.instance.newBigInvader = null;

        InvadersManager.instance.rightSpawn = false;
    }

    private void BigInvaderHitLeft()
    {
        Destroy(this.gameObject);

        InvadersManager.instance.newBigInvader = null;

        InvadersManager.instance.rightSpawn = true;
    }
}
