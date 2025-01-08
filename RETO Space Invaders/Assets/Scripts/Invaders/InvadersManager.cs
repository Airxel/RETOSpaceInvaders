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
        // Se establece el primer valor para que el big invader haga spawn
        nextSpawnTimer = Random.Range(minBigSpawnSeconds, maxBigSpawnSeconds);

        // Se establece el primer valor para que comiencen a disparar los invaders
        nextProjectileTimer = Random.Range(minShootingSeconds, maxShootingSeconds);
    }
    private void Update()
    {
        // Se controla la animación de los invaders
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

        // Se controla la cantidad de invaders que quedan, para saber si la partida termina
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

        // Si el jugador no está haciendo respawn
        if (!GameManager.isRespawning)
        {
            // Se controla el tiempo de disparo del os invaders
            lastProjectileTimer = lastProjectileTimer + Time.deltaTime;

            if (lastProjectileTimer >= nextProjectileTimer)
            {
                LastActiveInvaderShooting();

                lastProjectileTimer = 0f;
                nextProjectileTimer = Random.Range(minShootingSeconds, maxShootingSeconds);
            }

            InvadersSpeed();

            InvadersLateralMovement();

            // Si no existe el big invader, se crea
            if (newBigInvader == null && gameIsOver == false)
            {
                // Se controla el tiempo de spawn del big invader
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

            // Si existe el big invader, se controla su movimiento, según el lado donde haga spawn
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

    /// <summary>
    /// Función que controla el spawn de los invader, creando una lista de listas o matriz, poniéndoles nombre y añadiéndolos
    /// </summary>
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

    /// <summary>
    /// Función que controla el número de invaders activos en la escena, dando un int como resultado
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Función que controla la velocidad y aceleración de los invaders, según la cantidad que queden
    /// </summary>
    private void InvadersSpeed()
    {
        int invadersCount = InvadersCounter();

        invadersSpeedAcceleration = invadersSpeed + (totalColumns * totalRow - invadersCount) * accelerationFactor;

        invadersSpeedAcceleration = Mathf.Clamp(invadersSpeedAcceleration, 0f, 7.5f);
    }

    /// <summary>
    /// Función que controla el movimiento lateral de los invaders
    /// </summary>
    private void InvadersLateralMovement()
    {
        limitReached = false;

        // Se encuentra los extremos activos de la matriz en cada momento
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
                        // El primero que se encuentra activo es el más a la izquierda
                        activeLeftColumn = i;
                    }

                    // Continúa recorriendo la matriz, el último que se encuentra activo es el más a la derecha
                    activeRightColumn = i;
                }

                GameObject invader = matrizInvaders[i][j];

                // Si hay invaders activos
                if (invader.activeSelf == true)
                {
                    // Y se están moviendo a la derecha, el movimiento es positivo, hacia la derecha
                    if (movingRight)
                    {
                        invadersMovement = invadersSpeedAcceleration * Time.deltaTime;
                    }

                    // Si no, el movimiento es negativo, hacia la izquierda
                    else
                    {
                        invadersMovement = -invadersSpeedAcceleration * Time.deltaTime;
                    }

                    // Se crea el vector con la dirección de ese movimiento
                    Vector3 newInvadersMovement = invader.transform.position;

                    newInvadersMovement.x = newInvadersMovement.x + invadersMovement;

                    // Si el invader activo más a la derecha, se está moviendo a la derecha y llega al límite, se ha alcanzado ese límite
                    if (i == activeRightColumn && newInvadersMovement.x >= invadersMovementLimit && movingRight)
                    {
                        limitReached = true;
                    }

                    // Si el invader activo más a la izquierda, se está moviendo a la izquierda y llega al límite, se ha alcanzado ese límite
                    else if (i == activeLeftColumn && newInvadersMovement.x <= -invadersMovementLimit && !movingRight)
                    {
                        limitReached = true;
                    }

                    // Se establece el movimiento final de los invaders
                    invader.transform.position = newInvadersMovement;
                }
            }
        }

        // Cuando se llega al limite, los invaders bajan de posición
        if (limitReached)
        {
            InvadersDownMovement();
        }
    }

    /// <summary>
    /// Función que controla el movimiento hacia abajo de los invaders
    /// </summary>
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

        // Cuando los invaders bajan de fila, significa que el límite se alcanzo y hay que cambiar la dirección del movimiento de los invaders
        movingRight = !movingRight;

    }

    /// <summary>
    /// Función que busca el invader activo que esté más abajo en cada columna, para luego disparar desde ese invader, cada cierto tiempo
    /// </summary>
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
        }
    }

    /// <summary>
    /// Función que se llama desde otro script para desactivar los invaders restantes, una vez el jugador sea eliminado
    /// </summary>
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

        // Si se ha llamado a esta función, significa que el juego ha acabado
        gameIsOver = true;
    }

    /// <summary>
    /// Función que controla el spawn derecho del big invader
    /// </summary>
    private void BigInvaderRightSpawn()
    {
        bigInvaderSpawnPosition = new Vector3(bigInvaderSpawnPosX, bigInvaderSpawnPosY, 0f);
        newBigInvader = Instantiate(bigInvader, bigInvaderSpawnPosition, Quaternion.identity);
        SoundsManager.instance.bigInvaderSoundSource.Play();
    }

    /// <summary>
    /// Función que controla el spawn izquierdo del big invader
    /// </summary>
    private void BigInvaderLeftSpawn()
    {
        bigInvaderSpawnPosition = new Vector3(-bigInvaderSpawnPosX, bigInvaderSpawnPosY, 0f);
        newBigInvader = Instantiate(bigInvader, bigInvaderSpawnPosition, Quaternion.identity);
        SoundsManager.instance.bigInvaderSoundSource.Play();
    }

    /// <summary>
    /// Función que controla la animación de los invaders, buscando los hijos de cada padre que se instancia en la matriz
    /// </summary>
    /// <param name="invader"></param>
    private void InvaderAnimation(GameObject invader)
    {
        Transform invader1 = invader.transform.GetChild(0);
        Transform invader2 = invader.transform.GetChild(1);

        invader1.gameObject.SetActive(modelChanging);
        invader2.gameObject.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que controla la animación del big invader, buscando los hijos de cada padre que se intancia
    /// </summary>
    /// <param name="bigInvader"></param>
    private void BigInvaderAnimation(GameObject bigInvader)
    {
        Transform bigInvader1 = bigInvader.transform.GetChild(0);
        Transform bigInvader2 = bigInvader.transform.GetChild(1);

        bigInvader1.gameObject.SetActive(modelChanging);
        bigInvader2.gameObject.SetActive(!modelChanging);
    }

    


    /// <summary>
    /// Función de prueba para limpiar la matriz al pulsar el botón de volver a juagr
    /// </summary>
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
}
