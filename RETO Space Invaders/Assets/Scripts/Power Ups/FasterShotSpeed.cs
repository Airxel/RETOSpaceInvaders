using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterShotSpeed : MonoBehaviour, IPowerUpsInterface
{
    [SerializeField]
    private float powerUpDuration = 10f;

    /// <summary>
    /// Funci�n que se llama desde la interfaz
    /// </summary>
    public void PickUp()
    {
        // Se busca al jugador y seg�n el tipo que sea, se activa la inversi�n de movimiento en ese script
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

    /// <summary>
    /// Funci�n que controla las interacciones del power-up con el resto de elementos
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
    /// Funci�n que busca el tipo de jugador en escena, al ser 3 posibles, para luego activar el power-up en esa nave
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