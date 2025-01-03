using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterShotSpeed : MonoBehaviour, IPowerUpsInterface
{
    [SerializeField]
    private float powerUpDuration = 10f;

    public void PickUp()
    {
        Debug.Log("Aumentando Velocidad de Disparo");

        GameObject player = FindPlayerShip();

        if (player != null)
        {
            BalancedShipManager balancedShip = player.GetComponent<BalancedShipManager>();

            if (balancedShip != null)
            {
                balancedShip.FasterShotSpeed(powerUpDuration);
            }

            FastShipManager fastShip = player.GetComponent<FastShipManager>();

            if (fastShip != null)
            {
                fastShip.FasterShotSpeed(powerUpDuration);
            }

            SlowShipManager slowShip = player.GetComponent<SlowShipManager>();

            if (slowShip != null)
            {
                slowShip.FasterShotSpeed(powerUpDuration);
            }
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private GameObject FindPlayerShip()
    {
        GameObject player = GameObject.FindWithTag("Fast Ship");

        if (player != null)
        {
            return player;
        }

        player = GameObject.FindWithTag("Balanced Ship");

        if (player != null)
        {
            return player;
        }

        player = GameObject.FindWithTag("Slow Ship");

        return player;
    }
}