using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField]
    GameObject selectionMainShip1, selectionMainShip2, selectionFastShip1, selectionFastShip2, selectionBalancedShip1, selectionBalancedShip2, selectionSlowShip1, selectionSlowShip2, projectile, newProjectile;

    [SerializeField]
    GameObject mainShipCollection;

    [SerializeField]
    private float modelChangeTime = 1f;
    private float modelChangeTimer = 0f;

    private bool modelChanging = true;

    [SerializeField]
    private float mainShipSpeed = 50f;

    [SerializeField]
    private float mainShipMovementLimit = 80f;

    private float mainShipMovement;

    private void Update()
    {
        MainShipMovement();

        modelChangeTimer = modelChangeTimer + Time.deltaTime;

        if (modelChangeTimer >= modelChangeTime)
        {
            modelChanging = !modelChanging;

            SelectionMainShipAnimation();
            SelectionFastShipAnimation();
            SelectionBalancedShipAnimation();
            SelectionSlowShipAnimation();

            modelChangeTimer = 0f;
        }

        ShootingProjectile();
    }

    /// <summary>
    /// Función que controla el movimiento de la nave de selección de personaje
    /// </summary>
    private void MainShipMovement()
    {
        // Movimiendo según las teclas A y D o el joystick del mando
        mainShipMovement = Input.GetAxis("Horizontal");

        Vector3 newMainShipMovement = mainShipCollection.transform.position + new Vector3(mainShipMovement, 0f, 0f) * mainShipSpeed * Time.deltaTime;

        // Límite lateral de movimiento
        newMainShipMovement.x = Mathf.Clamp(newMainShipMovement.x, -mainShipMovementLimit, mainShipMovementLimit);

        mainShipCollection.transform.position = newMainShipMovement;
    }

    /// <summary>
    /// Función que controla el disparo de la nave de selección de personaje
    /// </summary>
    private void ShootingProjectile()
    {
        // Se dispara la darle a espacio o al botón A en el mando, si no hay otro proyectil en escena y si está activa la nave
        if (Input.GetButtonUp("Shoot") && newProjectile == null && mainShipCollection.activeSelf)
        {
            newProjectile = Instantiate(projectile, mainShipCollection.transform.position, Quaternion.identity);

            SoundsManager.instance.PlaySound(SoundsManager.instance.playerShootingSound);
        }
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void SelectionMainShipAnimation()
    {
        selectionMainShip1.SetActive(modelChanging);
        selectionMainShip2.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void SelectionFastShipAnimation()
    {
        selectionFastShip1.SetActive(modelChanging);
        selectionFastShip2.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void SelectionBalancedShipAnimation()
    {
        selectionBalancedShip1.SetActive(modelChanging);
        selectionBalancedShip2.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void SelectionSlowShipAnimation()
    {
        selectionSlowShip1.SetActive(modelChanging);
        selectionSlowShip2.SetActive(!modelChanging);
    }
}
