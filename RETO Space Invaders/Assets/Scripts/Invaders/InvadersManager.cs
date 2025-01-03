using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersManager : MonoBehaviour
{
    [SerializeField]
    GameObject projectile, newProjectile;

    [SerializeField]
    private float modelChangeTime = 1f;

    private float modelChangeTimer = 0f;

    private bool modelChanging = true;

    [SerializeField]
    int totalColumns = 9;

    [SerializeField]
    int totalRow = 4;

    [SerializeField]
    float invadersSpawnPosX = -50f;

    [SerializeField]
    float invadersSpawnPosY = 80f;

    [SerializeField]
    float invadersBetweenSpaceX = 2.5f;

    [SerializeField]
    float invadersBetweenSpaceY = 2.5f;

    [SerializeField]
    GameObject[] invaders;

    public List<List<GameObject>> matrizInvaders = new List<List<GameObject>>();

    [SerializeField]
    private int invadersCount = 0;

    [SerializeField]
    private float invadersSpeed = 2.5f;

    [SerializeField]
    private float invadersSpeedAcceleration = 0f;

    [SerializeField]
    private float accelerationFactor = 0.1f;

    [SerializeField]
    private float moveDownDistance = 1.5f;

    [SerializeField]
    private float invadersMovementLimit = 80f;

    private float invadersMovement;

    private bool limitReached = true;
    private bool movingRight = true;

    private float lastProjectileTimer = 0f;
    private float nextProjectileTimer;

    [SerializeField]
    private float minShootingSeconds = 1f;

    [SerializeField]
    private float maxShootingSeconds = 5f;



    [SerializeField]
    GameObject bigInvader;

    [SerializeField]
    public GameObject newBigInvader;

    private Vector3 bigInvaderSpawnPosition;

    [SerializeField]
    float bigInvaderSpawnPosX = -100f;

    [SerializeField]
    float bigInvaderSpawnPosY = 80f;

    [SerializeField]
    private float lastSpawnTimer;

    [SerializeField]
    private float nextSpawnTimer;

    [SerializeField]
    public bool rightSpawn = true;

    [SerializeField]
    private float minBigSpawnSeconds = 5f;

    [SerializeField]
    private float maxBigSpawnSeconds = 15f;

    [SerializeField]
    private float bigInvaderSpeed = 10f;

    public bool gameIsOver = false;

    //Singleton
    public static InvadersManager instance;

    private void Awake()
    {
        if (InvadersManager.instance == null)
        {
            InvadersManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        nextSpawnTimer = Random.Range(minBigSpawnSeconds, maxBigSpawnSeconds);

        nextProjectileTimer = Random.Range(minShootingSeconds, maxShootingSeconds);
    }
    private void Update()
    {
        modelChangeTimer = modelChangeTimer + Time.deltaTime;

        if (modelChangeTimer >= modelChangeTime)
        {
            modelChanging = !modelChanging;

            for (int i = 0; i < totalColumns; i++)
            {
                for (int j = 0; j < totalRow; j++)
                {
                    InvaderAnimation(matrizInvaders[i][j]);
                }
            }

            if (newBigInvader != null)
            {
                BigInvaderAnimation(newBigInvader);
            }

            modelChangeTimer = 0f;

        }

        invadersCount = InvadersCounter();

        if (invadersCount == 0)
        {
            gameIsOver = true;
            
            if (newBigInvader != null)
            {
                newBigInvader.SetActive(false);
            }

            GameManager.instance.PlayerVictory();
        }

        if (!GameManager.isRespawning)
        {
            lastProjectileTimer = lastProjectileTimer + Time.deltaTime;

            if (lastProjectileTimer >= nextProjectileTimer)
            {
                LastActiveInvaderShooting();

                lastProjectileTimer = 0f;
                nextProjectileTimer = Random.Range(minShootingSeconds, maxShootingSeconds);
            }

            InvadersSpeed();

            InvadersLateralMovement();

            if (newBigInvader == null && gameIsOver == false)
            {
                lastSpawnTimer = lastSpawnTimer + Time.deltaTime;

                if (lastSpawnTimer >= nextSpawnTimer && rightSpawn)
                {
                    BigInvaderRightSpawn();

                    lastSpawnTimer = 0f;
                    nextSpawnTimer = Random.Range(minBigSpawnSeconds, maxBigSpawnSeconds);
                }
                else if (lastSpawnTimer >= nextSpawnTimer && !rightSpawn)
                {
                    BigInvaderLeftSpawn();

                    lastSpawnTimer = 0f;
                    nextSpawnTimer = Random.Range(minBigSpawnSeconds, maxBigSpawnSeconds);
                }
            }

            if (newBigInvader != null && gameIsOver == false)
            {
                if (rightSpawn)
                {
                    newBigInvader.transform.Translate(Vector3.right * bigInvaderSpeed * Time.deltaTime);
                }
                else if (!rightSpawn)
                {
                    newBigInvader.transform.Translate(Vector3.left * bigInvaderSpeed * Time.deltaTime);
                }
            }
        }  
    }

    public void InvadersSpawn()
    {
        matrizInvaders.Clear();

        for (int i = 0; i < totalColumns; i++)
        {
            matrizInvaders.Add(new List<GameObject>());

            for (int j = 0; j < totalRow; j++)
            {
                Vector3 position = new Vector3(invadersSpawnPosX, invadersSpawnPosY, 0.0f);
                position.x = position.x + i * invadersBetweenSpaceX;
                position.y = position.y - j * invadersBetweenSpaceY;

                GameObject invaderSpawn = invaders[j];
                GameObject invader = Instantiate(invaderSpawn, position, Quaternion.identity);

                invader.name = "Invader(" + i.ToString() + "," + j.ToString() + ")";

                matrizInvaders[i].Add(invader);
            }
        }
    }

    private int InvadersCounter()
    {
        invadersCount = 0;

        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizInvaders[i][j].activeSelf == true)
                {
                    invadersCount = invadersCount + 1;
                }
            }
        }

        return invadersCount;

    }

    private void InvadersSpeed()
    {
        int invadersCount = InvadersCounter();

        invadersSpeedAcceleration = invadersSpeed + (totalColumns * totalRow - invadersCount) * accelerationFactor;

        invadersSpeedAcceleration = Mathf.Clamp(invadersSpeedAcceleration, 2.5f, 7.5f);
    }

    private void InvadersLateralMovement()
    {
        limitReached = false;

        // Encuentra los extremos activos
        int activeLeftColumn = -1;
        int activeRightColumn = -1;

        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizInvaders[i][j].activeSelf == true)
                {
                    if (activeLeftColumn == -1)
                    {
                        activeLeftColumn = i; // Primer invader activo a la izquierda
                    }

                    activeRightColumn = i; // Último invader activo a la derecha
                }

                GameObject invader = matrizInvaders[i][j];

                if (invader.activeSelf == true)
                {
                    if (movingRight)
                    {
                        invadersMovement = invadersSpeedAcceleration * Time.deltaTime;
                    }
                    else
                    {
                        invadersMovement = -invadersSpeedAcceleration * Time.deltaTime;
                    }

                    Vector3 newInvadersMovement = invader.transform.position;

                    newInvadersMovement.x += invadersMovement;

                    // Detectar si el límite se alcanza para los extremos
                    if (i == activeRightColumn && newInvadersMovement.x >= invadersMovementLimit && movingRight)
                    {
                        limitReached = true;
                    }
                    else if (i == activeLeftColumn && newInvadersMovement.x <= -invadersMovementLimit && !movingRight)
                    {
                        limitReached = true;
                    }

                    invader.transform.position = newInvadersMovement;
                }
            }
        }

        if (limitReached)
        {
            InvadersDownMovement();
        }
    }

    private void InvadersDownMovement()
    {
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                GameObject invader = matrizInvaders[i][j];

                Vector3 newInvadersPosition = invader.transform.position;

                newInvadersPosition.y = newInvadersPosition.y - moveDownDistance;

                invader.transform.position = newInvadersPosition;
            }
        }

        movingRight = !movingRight;

    }

    private void LastActiveInvaderShooting()
    {
        int randX = Random.Range(0, totalColumns);
        int activeInvader = -1;

        for (int i = 0; i < totalRow; i++)
        {
            if (matrizInvaders[randX][i].activeSelf == true)
            {
                activeInvader = i;
            }
        }
        if (activeInvader != -1)
        {
            Vector3 activeEnemyPosition = matrizInvaders[randX][activeInvader].transform.position;

            newProjectile = Instantiate(projectile, activeEnemyPosition, projectile.transform.rotation);

            SoundsManager.instance.PlaySound(SoundsManager.instance.invaderShootingSound);

            //Debug.Log("Ultimo elemento activo " + randX + ", " + activeEnemy);
        }
    }

    public void ClearInvaders()
    {
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizInvaders[i][j] != null)
                {
                    Destroy(matrizInvaders[i][j]);
                }
            }
        }

        matrizInvaders.Clear();

        if (newBigInvader != null)
        {
            Destroy(newBigInvader);
        }

        newBigInvader = null;
        movingRight = true;
        gameIsOver = false;
    }

    public void DeactivateInvaders()
    {
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                if (matrizInvaders[i][j] != null)
                {
                    matrizInvaders[i][j].SetActive(false);
                }
            }
        }

        if (newBigInvader != null)
        {
            newBigInvader.SetActive(false);
        }

        gameIsOver = true;
    }

    private void BigInvaderRightSpawn()
    {
        bigInvaderSpawnPosition = new Vector3(bigInvaderSpawnPosX, bigInvaderSpawnPosY, 0f);
        newBigInvader = Instantiate(bigInvader, bigInvaderSpawnPosition, Quaternion.identity);
        SoundsManager.instance.bigInvaderSoundSource.Play();
    }

    private void BigInvaderLeftSpawn()
    {
        bigInvaderSpawnPosition = new Vector3(-bigInvaderSpawnPosX, bigInvaderSpawnPosY, 0f);
        newBigInvader = Instantiate(bigInvader, bigInvaderSpawnPosition, Quaternion.identity);
        SoundsManager.instance.bigInvaderSoundSource.Play();
    }

    private void InvaderAnimation(GameObject invader)
    {
        Transform invader1 = invader.transform.GetChild(0);
        Transform invader2 = invader.transform.GetChild(1);

        invader1.gameObject.SetActive(modelChanging);
        invader2.gameObject.SetActive(!modelChanging);
    }

    private void BigInvaderAnimation(GameObject bigInvader)
    {
        Transform bigInvader1 = bigInvader.transform.GetChild(0);
        Transform bigInvader2 = bigInvader.transform.GetChild(1);

        bigInvader1.gameObject.SetActive(modelChanging);
        bigInvader2.gameObject.SetActive(!modelChanging);
    }
}
