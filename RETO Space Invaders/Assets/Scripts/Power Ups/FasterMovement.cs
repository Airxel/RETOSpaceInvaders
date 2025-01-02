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

        GameObject player = GameObject.FindWithTag("Player Ship");

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
}