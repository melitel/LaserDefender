using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCorouting;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCorouting == null)
        {
            firingCorouting = StartCoroutine(FireContinuosly());
        }
        else if(!isFiring && firingCorouting != null)
        {
            StopCoroutine(firingCorouting);
            firingCorouting = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                transform.position,
                                Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null) 
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(GetRandomSpawnTime());
        } 
        
    }

    float GetRandomSpawnTime()
    {
        float timeToNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance,
                                        baseFiringRate + firingRateVariance);

        return Mathf.Clamp(baseFiringRate, minFiringRate, float.MaxValue);
    }
}
