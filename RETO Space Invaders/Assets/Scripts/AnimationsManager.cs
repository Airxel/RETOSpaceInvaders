using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{

    [SerializeField]
    GameObject fastShip1, fastShip2, balancedShip1, balancedShip2, slowShip1, slowShip2;

    [SerializeField]
    GameObject leftShelter1, leftShelter2, middleLeftShelter1, middleLeftShelter2, middleRightShelter1, middleRightShelter2, rightShelter1, rightShelter2;

    [SerializeField]
    private float modelChangeTime = 1f;
    private float modelChangeTimer = 0f;

    private bool modelChanging = true;

    /// <summary>
    /// Función que controla continuamente el cambio entre un modelo y otro, controlado por un timer
    /// </summary>
    private void Update()
    {
        modelChangeTimer = modelChangeTimer + Time.deltaTime;

        if (modelChangeTimer >= modelChangeTime)
        {
            modelChanging = !modelChanging;

            FastShipAnimation();
            BalancedShipAnimation();
            SlowShipAnimation();
            ShelterAnimation();

            modelChangeTimer = 0f;
        }
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void FastShipAnimation()
    {
        fastShip1.SetActive(modelChanging);
        fastShip2.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void BalancedShipAnimation()
    {
        balancedShip1.SetActive(modelChanging);
        balancedShip2.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void SlowShipAnimation()
    {
        slowShip1.SetActive(modelChanging);
        slowShip2.SetActive(!modelChanging);
    }

    /// <summary>
    /// Función que activa y desactiva los modelos, animando el objeto
    /// </summary>
    private void ShelterAnimation()
    {
        leftShelter1.SetActive(modelChanging);
        leftShelter2.SetActive(!modelChanging);

        middleLeftShelter1.SetActive(modelChanging);
        middleLeftShelter2.SetActive(!modelChanging);

        middleRightShelter1.SetActive(modelChanging);
        middleRightShelter2.SetActive(!modelChanging);

        rightShelter1.SetActive(modelChanging);
        rightShelter2.SetActive(!modelChanging);
    }
}
