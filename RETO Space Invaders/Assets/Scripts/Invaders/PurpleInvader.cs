using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleInvader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Projectile"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
