using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterMovement : MonoBehaviour, IPowerUpsInterface
{
    [SerializeField]
    private float powerUpDuration = 10f;

    /// <summary>
    /// Función que se llama desde la interfaz
    /// </summary>
    public void PickUp()
    {
        // Se busca al jugador y según el tipo que sea, se activa la inversión de movimiento en ese script
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

    /// <summary>
    /// Función que controla las interacciones del power-up con el resto de elementos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Función que busca el tipo de jugador en escena, al ser 3 posibles, para luego activar el power-up en esa nave
    /// </summary>
    /// <returns></returns>
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