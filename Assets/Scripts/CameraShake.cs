using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float countDown = shakeDuration;

        for (int i = 0; i < 1000; i++)
        {
            while (countDown >= 0)
            {
                transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
                countDown -= Time.smoothDeltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        transform.position = initialPosition;
    }
}
