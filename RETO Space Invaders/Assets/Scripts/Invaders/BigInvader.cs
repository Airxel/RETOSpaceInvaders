using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigInvader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (InvadersManager.instance.rightSpawn == true)
        {
            if (other.gameObject.CompareTag("Fast Projectile"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = false;
            }
            else if (other.gameObject.CompareTag("Balanced Projectile"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = false;
            }
            else if (other.gameObject.CompareTag("Slow Projectile"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = false;
            }
            else if (other.gameObject.CompareTag("Right Wall"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = false;
            }
            else if (other.gameObject.CompareTag("Left Wall"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = false;
            }
        }
        else if (InvadersManager.instance.rightSpawn == false)
        {
            if (other.gameObject.CompareTag("Fast Projectile"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = true;
            }
            else if (other.gameObject.CompareTag("Balanced Projectile"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = true;
            }
            else if (other.gameObject.CompareTag("Slow Projectile"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = true;
            }
            else if (other.gameObject.CompareTag("Right Wall"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = true;
            }
            else if (other.gameObject.CompareTag("Left Wall"))
            {
                Destroy(this.gameObject);

                InvadersManager.instance.newBigInvader = null;

                InvadersManager.instance.rightSpawn = true;
            }
        }


    }
}
