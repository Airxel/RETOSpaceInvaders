using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheltersHealing : MonoBehaviour, IPowerUpsInterface
{
    public void PickUp()
    {
        Debug.Log("Refugios Restaurados");

        GameObject player = FindPlayerShip();

        if (player != null)
        {

            GameObject[] shelters = GameObject.FindGameObjectsWithTag("Shelter");

            for (int i = 0; i < shelters.Length; i++)
            {
                GameObject shelter = shelters[i];

                SheltersManager shelterState = shelter.GetComponent<SheltersManager>();

                if (shelterState != null)
                {
                    Debug.Log("Healing Shelter: " + shelter.name);
                    shelterState.SheltersHealing();
                    shelter.SetActive(true);
                }
            }
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottom Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private GameObject FindPlayerShip()
    {
        GameObject player = GameObject.FindWithTag("Fast Ship");

        if (player != null)
        {
            return player;
        }

        player = GameObject.FindWithTag("Balanced Ship");

        if (player != null)
        {
            return player;
        }

        player = GameObject.FindWithTag("Slow Ship");

        return player;
    }
}