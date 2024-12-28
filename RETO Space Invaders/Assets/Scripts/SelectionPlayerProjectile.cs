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
        GameManager.instance.playerSelection.SetActive(false);
        GameManager.instance.playerSelectionUI.SetActive(false);
        GameManager.instance.informationUI.SetActive(true);
        GameManager.instance.mainShipCollection.SetActive(false);
        GameManager.instance.sheltersCollection.SetActive(true);
        GameManager.instance.enemiesSpawner.SetActive(true);
    }

    private void FastShipSelected()
    {
        GameManager.instance.fastShipCollection.SetActive(true); 
    }

    private void BalancedShipSelected()
    {
        GameManager.instance.balancedShipCollection.SetActive(true);
    }

    private void SlowShipSelected()
    {
        GameManager.instance.slowShipCollection.SetActive(true);
    }
}
