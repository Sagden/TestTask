using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : NetworkBehaviour
{
    [SerializeField] private float force;

    private Rigidbody body;
    private bool hitToTarget = false;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        if (body == null)
        {
            Debug.LogError("Rigidbody отсутствует!");
        }
        else
        {
            body.AddForce(transform.forward * force);
        }
    }

    private void Update()
    {
        if (transform.position.y < -100)
            NetworkServer.Destroy(gameObject);

        if (!hitToTarget)
        {
            transform.rotation = Quaternion.LookRotation(body.velocity, Vector3.up);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Dog")
        {
            body.velocity = Vector3.zero;
            Destroy(body);
            transform.SetParent(collision.transform, true);
            hitToTarget = true;
        }
    }
}
