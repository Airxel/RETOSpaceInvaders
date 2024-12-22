using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShipManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;

    [SerializeField]
    private float shipMovementLimit = 80f;

    private float shipMovement;

    [SerializeField]
    GameObject projectile, newProjectile;

    private void Update()
    {
        ShipMovement();
        ShootingProjectile();
    }

    private void ShootingProjectile()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            newProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);
        }
    }

    private void ShipMovement()
    {
        shipMovement = Input.GetAxis("Horizontal");

        Vector3 newShipMovement = this.transform.position + new Vector3(shipMovement, 0f, 0f) * speed * Time.deltaTime;

        newShipMovement.x = Mathf.Clamp(newShipMovement.x, -shipMovementLimit, shipMovementLimit);

        this.transform.position = newShipMovement;
    }
}
