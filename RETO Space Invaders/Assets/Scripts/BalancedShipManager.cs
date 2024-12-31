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

    private int lives;

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
        lives = GameManager.instance.lives - 1;

        GameManager.instance.lives = lives;
        GameManager.instance.livesNumber.text = lives.ToString();

        SoundsManager.instance.PlaySound(SoundsManager.instance.playerDeadSound);

        if (lives <= 0)
        {
            GameManager.instance.PlayerDead();
        }
        else
        {
            PlayerRespawn();
        }
    }

    private void PlayerRespawn()
    {
        GameManager.isRespawning = true;

        this.transform.position = new Vector3(0f, 10f, 0f);
    }

    private void ShootingProjectile()
    {
        if (Input.GetButtonUp("Shoot"))
        {
            newProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);

            SoundsManager.instance.PlaySound(SoundsManager.instance.playerShootingSound);

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
