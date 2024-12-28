using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigInvader : MonoBehaviour
{
    [SerializeField]
    private float points = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fast Projectile") ||
            other.gameObject.CompareTag("Balanced Projectile") ||
            other.gameObject.CompareTag("Slow Projectile") ||
            other.gameObject.CompareTag("Right Wall") ||
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
    }

    private void BigInvaderHitRight()
    {
        GameManager.instance.AddPoints(points);

        Destroy(this.gameObject);

        InvadersManager.instance.newBigInvader = null;

        InvadersManager.instance.rightSpawn = false;
    }

    private void BigInvaderHitLeft()
    {
        GameManager.instance.AddPoints(points);

        Destroy(this.gameObject);

        InvadersManager.instance.newBigInvader = null;

        InvadersManager.instance.rightSpawn = true;
    }
}
