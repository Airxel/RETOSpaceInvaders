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

    /// <summary>
    /// Se establecen los valores bases, para poder usarlos con los power-ups, por ejemplo
    /// </summary>
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

            // Esto ocurre si el power-up de más velocidad de disparo está activo
            if (fasterShooting)
            {
                FasterShotSpeedIsActive();
            }

            if (shootingDelayTimer >= shootingDelayTime)
            {
                ShootingProjectile();
            }
        }
        else if (GameManager.isRespawning)
        {
            PlayerIsRespawning();
        }

        ShipMovement();
    }

    /// <summary>
    /// Función que controla la interfaz y las interacciones de la nave con otros elementos
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            EnemyHit();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyContact();
        }

        // Se llama a la interfaz, al interactuar con un power-up
        IPowerUpsInterface powerUp = other.GetComponent<IPowerUpsInterface>();

        if (powerUp != null)
        {
            powerUp.PickUp();
        }
    }

    /// <summary>
    /// Función que controla cuando el jugador es golpeado
    /// </summary>
    private void EnemyHit()
    {
        // Se pierde una vida
        lives = GameManager.instance.lives - 1;

        GameManager.instance.lives = lives;
        GameManager.instance.livesNumber.text = lives.ToString();

        SoundsManager.instance.PlaySound(SoundsManager.instance.playerDeadSound);

        // Dependiendo de las vidas, hace respawn o no
        if (lives <= 0)
        {
            GameManager.instance.PlayerDead();
        }
        else
        {
            PlayerRespawn();
        }
    }

    /// <summary>
    /// Función que controla si un invader golpea al jugador, terminando la partida
    /// </summary>
    private void EnemyContact()
    {
        GameManager.instance.PlayerDead();
    }

    /// <summary>
    /// Función que controla el respawn del jugador
    /// </summary>
    private void PlayerRespawn()
    {
        GameManager.isRespawning = true;

        this.transform.position = new Vector3(0f, 10f, 0f);
    }

    /// <summary>
    /// Función que controla el tiempo que dura el respawn del jugador
    /// </summary>
    private void PlayerIsRespawning()
    {
        respawnTimer = respawnTimer + Time.deltaTime;

        if (respawnTimer >= respawnTime)
        {
            GameManager.isRespawning = false;

            respawnTimer = 0f;
        }
    }

    /// <summary>
    /// Función que controla el disparo del jugador
    /// </summary>
    private void ShootingProjectile()
    {
        if (Input.GetButtonUp("Shoot"))
        {
            newProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);

            SoundsManager.instance.PlaySound(SoundsManager.instance.playerShootingSound);

            shootingDelayTimer = 0f;
        }
    }

    /// <summary>
    /// Función que controla el movimiento del jugador
    /// </summary>
    private void ShipMovement()
    {
        shipMovement = Input.GetAxis("Horizontal");

        // Esto ocurre si el power-up de más velocidad de movimiento está activo
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

        // Esto ocurre si la inversión de movimiento está activa
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

    /// <summary>
    /// Esta función controla si la inversión está activa y su duración
    /// </summary>
    /// <param name="powerUpDuration"></param>
    public void InvertMovement(float powerUpDuration)
    {
        invertMovement = true;
        invertMovementTimer = powerUpDuration;
    }

    /// <summary>
    /// Esta función controla si el aumento de velocidad de movimiento está activo y su duración
    /// </summary>
    /// <param name="powerUpDuration"></param>
    public void FasterMovement(float powerUpDuration)
    {
        fasterMovement = true;
        fasterMovementTimer = powerUpDuration;
    }

    /// <summary>
    /// Esta función controla si el aumento de velocidad de disparo está activo y su duración
    /// </summary>
    /// <param name="powerUpDuration"></param>
    public void FasterShotSpeed(float powerUpDuration)
    {
        fasterShooting = true;
        fasterShootingTimer = powerUpDuration;
    }

    private void FasterShotSpeedIsActive()
    {
        shootingDelayTime = baseShootingDelayTime * fasterShootingMultiplier;

        fasterShootingTimer = fasterShootingTimer - Time.deltaTime;

        if (fasterShootingTimer <= 0f)
        {
            shootingDelayTime = baseShootingDelayTime;
            fasterShooting = false;
        }
    }
}