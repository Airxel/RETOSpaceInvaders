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

    private int lifes;

    private void Update()
    {
        ShipMovement();
        ShootingProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            EnemyHit();
        }
    }

    private void EnemyHit()
    {
        lifes = CanvasManager.instance.lifes - 1;

        CanvasManager.instance.lifes = lifes;
        CanvasManager.instance.lifesNumber.text = lifes.ToString();

        Debug.Log("HIT");

        if (lifes <= 0)
        {
            Debug.Log("DEAD");

            this.gameObject.SetActive(false);
        }
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
