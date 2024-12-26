using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    private void Update()
    {
        this.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fast Ship"))
        {
            PlayerSelectedUI();
            FastShipSelected();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Balanced Ship"))
        {
            PlayerSelectedUI();
            BalancedShipSelected();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Slow Ship"))
        {
            PlayerSelectedUI();
            SlowShipSelected();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Top Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void PlayerSelectedUI()
    {
        CanvasManager.instance.playerSelection.SetActive(false);
        CanvasManager.instance.playerSelectionUI.SetActive(false);
        CanvasManager.instance.informationUI.SetActive(true);
        CanvasManager.instance.mainShipCollection.SetActive(false);
        CanvasManager.instance.sheltersCollection.SetActive(true);
        CanvasManager.instance.enemiesSpawner.SetActive(true);
    }

    private void FastShipSelected()
    {
        CanvasManager.instance.fastShipCollection.SetActive(true); 
    }

    private void BalancedShipSelected()
    {
        CanvasManager.instance.balancedShipCollection.SetActive(true);
    }

    private void SlowShipSelected()
    {
        CanvasManager.instance.slowShipCollection.SetActive(true);
    }
}
