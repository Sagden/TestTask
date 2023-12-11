using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Все что касается рандомного движения и поворотов собаки!
public class Dog : NetworkBehaviour
{
    public float minSpeed = 0.1f;
    public float maxSpeed = 3f;

    private float currentSpeed;
    private Vector3 randomDirection;
    private float randomTimer;
    private float timer = 0;

    void Start()
    {
        GenerateRandom();
    }

    void Update()
    {
        if (!isOwned) return;

        transform.position = transform.position + randomDirection * currentSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(randomDirection, Vector3.up);
        timer += Time.deltaTime;

        if (timer > randomTimer)
        {
            GenerateRandom();
            timer = 0;
        }
    }

    void GenerateRandom()
    {
        randomTimer = Random.Range(1, 5);
        randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
