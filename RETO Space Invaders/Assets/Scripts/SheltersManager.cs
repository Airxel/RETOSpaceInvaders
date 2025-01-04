using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheltersManager : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 15;

    [SerializeField]
    GameObject shelter;

    private Transform fullShelter;
    private Transform dyingShelter;
    private Transform earthShelter;

    private BoxCollider shelterCollider;

    private void Start()
    {
        fullShelter = shelter.transform.GetChild(0);
        dyingShelter = shelter.transform.GetChild(1);
        earthShelter = shelter.transform.GetChild(2);

        shelterCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            ShelterState();
        }
        else if (other.gameObject.CompareTag("Fast Projectile"))
        {
            ShelterState();
        }
        else if (other.gameObject.CompareTag("Balanced Projectile"))
        {
            ShelterState();
        }
        else if (other.gameObject.CompareTag("Slow Projectile"))
        {
            ShelterState();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            ShelterEnemyContact();
        }
    }

    private void ShelterState()
    {
        hitPoints = hitPoints - 1;

        if (hitPoints > 10)
        {
            fullShelter.gameObject.SetActive(true);
            dyingShelter.gameObject.SetActive(false);
            earthShelter.gameObject.SetActive(false);
        }
        else if (hitPoints > 5)
        {
            fullShelter.gameObject.SetActive(false);
            dyingShelter.gameObject.SetActive(true);
            earthShelter.gameObject.SetActive(false);
        }
        else if (hitPoints > 0)
        {
            fullShelter.gameObject.SetActive(false);
            dyingShelter.gameObject.SetActive(false);
            earthShelter.gameObject.SetActive(true);

            shelterCollider.size = new Vector3(14.5f, 14.5f, 1f);
            shelterCollider.center = Vector3.zero;
        }
        else
        {
            fullShelter.gameObject.SetActive(false);
            dyingShelter.gameObject.SetActive(false);
            earthShelter.gameObject.SetActive(false);

            shelterCollider.size = new Vector3(30.5f, 16.5f, 1f);
            shelterCollider.center = new Vector3(0f, 1f, 0f);

            this.gameObject.SetActive(false);
        }
    }

    private void ShelterEnemyContact()
    {
        hitPoints = 0;

        fullShelter.gameObject.SetActive(false);
        dyingShelter.gameObject.SetActive(false);
        earthShelter.gameObject.SetActive(false);

        shelterCollider.size = new Vector3(30.5f, 16.5f, 1f);
        shelterCollider.center = new Vector3(0f, 1f, 0f);

        this.gameObject.SetActive(false);
    }

    public void SheltersHealing()
    {
        hitPoints = 15;

        fullShelter.gameObject.SetActive(true);
        dyingShelter.gameObject.SetActive(false);
        earthShelter.gameObject.SetActive(false);

        shelterCollider.size = new Vector3(30.5f, 16.5f, 1f);
        shelterCollider.center = new Vector3(0f, 1f, 0f);
    }
}