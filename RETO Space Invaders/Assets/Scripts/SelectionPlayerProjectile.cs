using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    /// <summary>
    /// Función que controla el movimiento del proyectil de selección de personaje
    /// </summary>
    private void Update()
    {
        this.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Función que controla las interacciones del proyectil con otros elementos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fast Ship"))
        {
            PlayerSelectedUI();
            FastShipSelected();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Balanced Ship"))
        {
            PlayerSelectedUI();
            BalancedShipSelected();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Slow Ship"))
        {
            PlayerSelectedUI();
            SlowShipSelected();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Top Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Función común, que se encarga de activar y desactivar los elementos necesarios, dando paso al spawn de los invaders
    /// </summary>
    private void PlayerSelectedUI()
    {
        GameManager.instance.playerSelection.SetActive(false);
        GameManager.instance.playerSelectionUI.SetActive(false);
        GameManager.instance.informationUI.SetActive(true);
        GameManager.instance.mainShipCollection.SetActive(false);
        GameManager.instance.sheltersCollection.SetActive(true);
        GameManager.instance.enemiesSpawner.SetActive(true);

        InvadersManager.instance.InvadersSpawn();
    }

    /// <summary>
    /// Función que activa la nave correspondiente
    /// </summary>
    private void FastShipSelected()
    {
        GameManager.instance.fastShipCollection.SetActive(true); 
    }

    /// <summary>
    /// Función que activa la nave correspondiente
    /// </summary>
    private void BalancedShipSelected()
    {
        GameManager.instance.balancedShipCollection.SetActive(true);
    }

    /// <summary>
    /// Función que activa la nave correspondiente
    /// </summary>
    private void SlowShipSelected()
    {
        GameManager.instance.slowShipCollection.SetActive(true);
    }
}
