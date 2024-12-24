using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
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
    float initialPosX = -50f;

    [SerializeField]
    float initialPosY = 80f;

    [SerializeField]
    float spaceBetweenElementsX = 2.5f;

    [SerializeField]
    float spaceBetweenElementsY = 2.5f;

    [SerializeField]
    GameObject[] enemies;

    List<List<GameObject>> matrizEnemies = new List<List<GameObject>>();

    [SerializeField]
    float speed = 2.5f;

    [SerializeField]
    float moveDownDistance = 1.5f;

    [SerializeField]
    float enemiesMovementLimit = 80f;

    float enemiesMovement;

    private bool limitReached = true;
    private bool movingRight = true;

    private float lastProjectileTimer = 0f;
    private float nextProjectileTimer;

    [SerializeField]
    private float minSeconds = 1f;

    [SerializeField]
    private float maxSeconds = 5f;

    private void Start()
    {
        EnemiesSpawn();

        nextProjectileTimer = Random.Range(minSeconds, maxSeconds);

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
                    InvaderAnimation(matrizEnemies[i][j]);
                }
            }

            modelChangeTimer = 0f;

        }

        lastProjectileTimer = lastProjectileTimer + Time.deltaTime;

        if (lastProjectileTimer >= nextProjectileTimer)
        {
            LastActiveEnemyShooting();

            lastProjectileTimer = 0f;
            nextProjectileTimer = Random.Range(minSeconds, maxSeconds);
        }

        EnemiesLateralMovement();

    }

    private void EnemiesLateralMovement()
    {
        limitReached = false;

        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                GameObject enemy = matrizEnemies[i][j];

                if (movingRight)
                {
                    enemiesMovement = speed * Time.deltaTime;
                }
                else if (!movingRight)
                {
                    enemiesMovement = -speed * Time.deltaTime;
                }

                Vector3 newEnemiesMovement = enemy.transform.position;

                newEnemiesMovement.x = newEnemiesMovement.x + enemiesMovement;

                if (newEnemiesMovement.x >= enemiesMovementLimit && movingRight)
                {
                    limitReached = true;
                }
                else if (newEnemiesMovement.x <= -enemiesMovementLimit && !movingRight)
                {
                    limitReached = true;
                }

                enemy.transform.position = newEnemiesMovement;

            }
        }
        if (limitReached)
        {
            EnemiesDownMovement();
        }
    }

    private void EnemiesDownMovement()
    {
        for (int i = 0; i < totalColumns; i++)
        {
            for (int j = 0; j < totalRow; j++)
            {
                GameObject enemy = matrizEnemies[i][j];

                Vector3 newEnemiesPosition = enemy.transform.position;

                newEnemiesPosition.y = newEnemiesPosition.y - moveDownDistance;

                enemy.transform.position = newEnemiesPosition;
            }
        }

        movingRight = !movingRight;

    }

    private void LastActiveEnemyShooting()
    {
        int randX = Random.Range(0, totalColumns);
        int activeEnemy = -1;

        for (int i = 0; i < totalRow; i++)
        {
            if (matrizEnemies[randX][i].activeSelf == true)
            {
                activeEnemy = i;
            }
        }
        if (activeEnemy != -1)
        {
            Vector3 activeEnemyPosition = matrizEnemies[randX][activeEnemy].transform.position;

            newProjectile = Instantiate(projectile, activeEnemyPosition, projectile.transform.rotation);

            Debug.Log("Ultimo elemento activo " + randX + ", " + activeEnemy);
        }
    }

    private void EnemiesSpawn()
    {
        for (int i = 0; i < totalColumns; i++)
        {
            matrizEnemies.Add(new List<GameObject>());

            for (int j = 0; j < totalRow; j++)
            {
                Vector3 position = new Vector3(initialPosX, initialPosY, 0.0f);
                position.x = position.x + i * spaceBetweenElementsX;
                position.y = position.y - j * spaceBetweenElementsY;

                GameObject enemySpawn = enemies[j];
                GameObject enemy = Instantiate(enemySpawn, position, Quaternion.identity);

                enemy.name = "Alien(" + i.ToString() + "," + j.ToString() + ")";

                matrizEnemies[i].Add(enemy);
            }
        }
    }

    private void InvaderAnimation(GameObject enemy)
    {
        Transform invader1 = enemy.transform.GetChild(0);
        Transform invader2 = enemy.transform.GetChild(1);

        invader1.gameObject.SetActive(modelChanging);
        invader2.gameObject.SetActive(!modelChanging);
    }

    private void RandomDeactivator()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            int randX = Random.Range(0, totalColumns);
            int randY = Random.Range(0, totalRow);
            matrizEnemies[randX][randY].SetActive(!matrizEnemies[randX][randY].activeSelf);
        }
    }
}
