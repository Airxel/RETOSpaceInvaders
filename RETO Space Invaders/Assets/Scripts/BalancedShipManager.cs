using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BalancedShipManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 75f;

    [SerializeField]
    private float shipMovementLimit = 80f;

    private float shipMovement;

    [SerializeField]
    GameObject projectile, newProjectile;

    [SerializeField]
    private float shootingDelayTime = 1f;

    [SerializeField]
    private float shootingDelayTimer = 1f;

    private int lifes;

    [SerializeField]
    private float respawnTime = 3f;

    [SerializeField]
    private float respawnTimer;

    private BoxCollider shipCollider;

    private void Start()
    {
        shipCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!GameManager.isRespawning)
        {
            shootingDelayTimer = shootingDelayTimer + Time.deltaTime;

            if (shootingDelayTimer >= shootingDelayTime)
            {
                ShootingProjectile();
            }
        }
        else if (GameManager.isRespawning)
        {
            respawnTimer = respawnTimer + Time.deltaTime;

            if (respawnTimer >= respawnTime)
            {
                GameManager.isRespawning = false;

                shipCollider.enabled = true;

                respawnTimer = 0f;
            }
        }

        ShipMovement();

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
        lifes = GameManager.instance.lifes - 1;

        GameManager.instance.lifes = lifes;
        GameManager.instance.lifesNumber.text = lifes.ToString();

        if (lifes <= 0)
        {
            Debug.Log("DEAD");

            this.gameObject.SetActive(false);
        }
        else
        {
            PlayerRespawn();
            Debug.Log("HIT");
        }
    }

    private void PlayerRespawn()
    {
        GameManager.isRespawning = true;

        shipCollider.enabled = false;

        this.transform.position = new Vector3(0f, 10f, 0f);
    }

    private void ShootingProjectile()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            newProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);

            shootingDelayTimer = 0f;
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
