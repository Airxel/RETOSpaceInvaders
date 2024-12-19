using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{

    [SerializeField]
    GameObject mainShip1, mainShip2, fastShip1, fastShip2, balancedShip1, balancedShip2, slowShip1, slowShip2;

    [SerializeField]
    GameObject leftShelter1, leftShelter2, middleLeftShelter1, middleLeftShelter2, middleRightShelter1, middleRightShelter2, rightShelter1, rightShelter2;

    [SerializeField]
    GameObject fastShipCollection, balancedShipCollection, slowShipCollection;

    [SerializeField]
    private float modelChangeTime = 1f;
    private float modelChangeTimer = 0f;

    private bool modelChanging = true;

    private void Start()
    {

    }

    private void Update()
    {
        modelChangeTimer = modelChangeTimer + Time.deltaTime;

        if (modelChangeTimer >= modelChangeTime)
        {
            modelChanging = !modelChanging;

            leftShelter1.SetActive(modelChanging);
            leftShelter2.SetActive(!modelChanging);

            middleLeftShelter1.SetActive(modelChanging);
            middleLeftShelter2.SetActive(!modelChanging);

            middleRightShelter1.SetActive(modelChanging);
            middleRightShelter2.SetActive(!modelChanging);

            rightShelter1.SetActive(modelChanging);
            rightShelter2.SetActive(!modelChanging);

            modelChangeTimer = 0f;
        }
    }
}
