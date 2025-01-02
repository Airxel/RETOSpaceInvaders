using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMovement : MonoBehaviour, IPowerUpsInterface
{
    [SerializeField]
    private float powerUpDuration = 10f;

    public void PickUp()
    {
        Debug.Log("Invirtiendo Controles");

        GameObject player = GameObject.FindWithTag("Player Ship");

        if (player != null)
        {
            BalancedShipManager balancedShip = player.GetComponent<BalancedShipManager>();

            if (balancedShip != null)
            {
                balancedShip.InvertMovement(powerUpDuration);
            }

            FastShipManager fastShip = player.GetComponent<FastShipManager>();

            if (fastShip != null)
            {
                fastShip.InvertMovement(powerUpDuration);
            }

            SlowShipManager slowShip = player.GetComponent<SlowShipManager>();

            if (slowShip != null)
            {
                slowShip.InvertMovement(powerUpDuration);
            }
        }

        Destroy(this.gameObject);
    }
}