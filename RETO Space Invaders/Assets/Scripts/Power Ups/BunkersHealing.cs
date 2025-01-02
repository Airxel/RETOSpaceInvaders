using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkersHealing : MonoBehaviour, IPowerUpsInterface
{
    public void PickUp()
    {
        Debug.Log("Refugios Restaurados");

        GameObject player = GameObject.FindWithTag("Player Ship");
        GameObject shelter = GameObject.FindWithTag("Shelter");

        if (player != null && shelter != null)
        {
            SheltersManager shelterState = shelter.GetComponent<SheltersManager>();

            if (shelterState != null)
            {
                shelterState.SheltersHealing();
                shelter.SetActive(true);
            }
        }

        Destroy(this.gameObject);
    }
}