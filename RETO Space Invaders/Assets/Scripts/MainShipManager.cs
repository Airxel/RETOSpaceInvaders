using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipManager : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private float mainShipMovement;

    void Start()
    {
        
    }

    void Update()
    {
        mainShipMovement = Input.GetAxis("Horizontal");
        this.transform.position = this.transform.position + new Vector3(mainShipMovement, 0f, 0f) * speed * Time.deltaTime;
    }
}
