using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField]
    GameObject mainShip1, mainShip2, fastShip1, fastShip2, balancedShip1, balancedShip2, slowShip1, slowShip2, projectile;

    [SerializeField]
    GameObject fastShipCollection, balancedShipCollection, slowShipCollection, mainShipCollection;

    private GameObject newProjectile;

    [SerializeField]
    private float modelChangeTime = 1f;
    private float modelChangeTimer = 0f;

    private bool modelChanging = true;

    [SerializeField]
    private float mainShipSpeed = 50f;

    [SerializeField]
    private float projectileSpeed = 100f;

    private float mainShipMovement;

    private void Start()
    {
        LeanTween.moveY(fastShipCollection, 55f, 1f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(balancedShipCollection, 55f, 1.25f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
        LeanTween.moveY(slowShipCollection, 55f, 1.5f).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();
    }

    private void Update()
    {
        mainShipMovement = Input.GetAxis("Horizontal");
        mainShipCollection.transform.position = mainShipCollection.transform.position + new Vector3(mainShipMovement, 0f, 0f) * mainShipSpeed * Time.deltaTime;

        modelChangeTimer = modelChangeTimer + Time.deltaTime;

        if (modelChangeTimer >= modelChangeTime)
        {
            modelChanging = !modelChanging;

            mainShip1.SetActive(modelChanging);
            mainShip2.SetActive(!modelChanging);

            fastShip1.SetActive(modelChanging);
            fastShip2.SetActive(!modelChanging);

            balancedShip1.SetActive(modelChanging);
            balancedShip2.SetActive(!modelChanging);

            slowShip1.SetActive(modelChanging);
            slowShip2.SetActive(!modelChanging);

            modelChangeTimer = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && newProjectile == null)
        {
            newProjectile = Instantiate(projectile, mainShipCollection.transform.position, Quaternion.identity);
        }
        if (newProjectile)
        {
            newProjectile.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        }
    }
}
