using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowShipManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

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

    [SerializeField]
    private bool invertMovement = false;
    private bool fasterShooting = false;
    private bool fasterMovement = false;

    [SerializeField]
    private float fasterMovementMultiplier = 2f;
    private float baseSpeed;

    [SerializeField]
    private float fasterShootingMultiplier = 0.5f;
    private float baseShootingDelayTime;

    [SerializeField]
    private float invertMovementTimer;

    [SerializeField]
    private float fasterShootingTimer;

    [SerializeField]
    private float fasterMovementTimer;

    private void Start()
    {
        baseSpeed = speed;
        baseShootingDelayTime = shootingDelayTime;
    }

    private void Update()
    {
        if (!GameManager.isRespawning)
        {
            shootingDelayTimer = shootingDelayTimer + Time.deltaTime;

            if (fasterShooting)
            {
                shootingDelayTime = baseShootingDelayTime * fasterShootingMultiplier;

                fasterShootingTimer = fasterShootingTimer - Time.deltaTime;

                if (fasterShootingTimer <= 0f)
                {
                    shootingDelayTime = baseShootingDelayTime;
                    fasterShooting = false;
                }
            }

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

        IPowerUpsInterface powerUp = other.GetComponent<IPowerUpsInterface>();

        if (powerUp != null)
        {
            powerUp.PickUp();
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

        if (fasterMovement)
        {
            speed = baseSpeed * fasterMovementMultiplier;

            fasterMovementTimer = fasterMovementTimer - Time.deltaTime;

            if (fasterMovementTimer <= 0f)
            {
                speed = baseSpeed;
                fasterMovement = false;
            }
        }

        if (invertMovement)
        {
            shipMovement = shipMovement * -1f;

            invertMovementTimer = invertMovementTimer - Time.deltaTime;

            if (invertMovementTimer <= 0f)
            {
                invertMovement = false;
            }
        }

        Vector3 newShipMovement = this.transform.position + new Vector3(shipMovement, 0f, 0f) * speed * Time.deltaTime;

        newShipMovement.x = Mathf.Clamp(newShipMovement.x, -shipMovementLimit, shipMovementLimit);

        this.transform.position = newShipMovement;
    }

    public void InvertMovement(float powerUpDuration)
    {
        invertMovement = true;
        invertMovementTimer = powerUpDuration;
    }

    public void FasterMovement(float powerUpDuration)
    {
        fasterMovement = true;
        fasterMovementTimer = powerUpDuration;
    }

    public void FasterShotSpeed(float powerUpDuration)
    {
        fasterShooting = true;
        fasterShootingTimer = powerUpDuration;
    }
}