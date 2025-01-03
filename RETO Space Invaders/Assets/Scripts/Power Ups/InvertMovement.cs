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

        GameObject player = FindPlayerShip();

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