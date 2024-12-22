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
        mainShipMovement = Input.GetAxis("Horizontal");

        Vector3 newMainShipMovement = mainShipCollection.transform.position + new Vector3(mainShipMovement, 0f, 0f) * mainShipSpeed * Time.deltaTime;

        newMainShipMovement.x = Mathf.Clamp(newMainShipMovement.x, -mainShipMovementLimit, mainShipMovementLimit);

        mainShipCollection.transform.position = newMainShipMovement;

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

    private void SelectionMainShipAnimation()
    {
        selectionMainShip1.SetActive(modelChanging);
        selectionMainShip2.SetActive(!modelChanging);
    }

    private void SelectionFastShipAnimation()
    {
        selectionFastShip1.SetActive(modelChanging);
        selectionFastShip2.SetActive(!modelChanging);
    }

    private void SelectionBalancedShipAnimation()
    {
        selectionBalancedShip1.SetActive(modelChanging);
        selectionBalancedShip2.SetActive(!modelChanging);
    }

    private void SelectionSlowShipAnimation()
    {
        selectionSlowShip1.SetActive(modelChanging);
        selectionSlowShip2.SetActive(!modelChanging);
    }

    private void ShootingProjectile()
    {
        if (Input.GetKeyUp(KeyCode.Space) && newProjectile == null && mainShipCollection.activeSelf)
        {
            newProjectile = Instantiate(projectile, mainShipCollection.transform.position, Quaternion.identity);
        }  
    }
}
