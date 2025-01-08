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
        // Se establecen los hijos del prefab, para poder controlarlos por separado
        fullShelter = shelter.transform.GetChild(0);
        dyingShelter = shelter.transform.GetChild(1);
        earthShelter = shelter.transform.GetChild(2);

        // Se establece el collider, para poder controlarlo
        shelterCollider = GetComponent<BoxCollider>();
    }

    /// <summary>
    /// Función que controla las interacciones de los shelters con otros elementos
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Función que controla las vidas de los shelters y como se ven según tengan más o menos
    /// </summary>
    private void ShelterState()
    {
        hitPoints = hitPoints - 1;

        // Según la vida que tengan los shelters, se activa un modelo u otro y se cambia la hitbox de los mismos
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

    /// <summary>
    /// Función que controla si un invader colisiona fisicamente con un shelter
    /// </summary>
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

    /// <summary>
    /// Función que se llama cuando se activa el power-up de curación
    /// </summary>
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