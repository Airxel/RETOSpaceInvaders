using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 100f;

    private void Update()
    {
        this.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || 
            other.gameObject.CompareTag("Top Wall") || 
            other.gameObject.CompareTag("Shelter") ||
            other.gameObject.CompareTag("Big Invader"))
        {
            Destroy(this.gameObject);
        }
    }
}
