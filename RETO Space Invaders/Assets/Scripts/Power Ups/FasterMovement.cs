using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterMovement : MonoBehaviour, IPowerUpsInterface
{
    [SerializeField]
    private float powerUpDuration = 10f;

    public void PickUp()
    {
        Debug.Log("Aumentando Velocidad");

        GameObject player = FindPlayerShip();

        if (player != null)
        {
            BalancedShipManager balancedShip = player.GetComponent<BalancedShipManager>();

            if (balancedShip != null)
            {
                balancedShip.FasterMovement(powerUpDuration);
            }

            FastShipManager fastShip = player.GetComponent<FastShipManager>();

            if (fastShip != null)
            {
                fastShip.FasterMovement(powerUpDuration);
            }

            SlowShipManager slowShip = player.GetComponent<SlowShipManager>();

            if (slowShip != null)
            {
                slowShip.FasterMovement(powerUpDuration);
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