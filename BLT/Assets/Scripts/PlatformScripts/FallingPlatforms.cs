using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private float shakeDuration = 1f;
    private float shakeMagnitude = 0.3f;
    private float breakDelay = 0.5f;
    private float reactivateDelay = 2f;

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Fall!");
            if (!isShaking)
            {
                StartCoroutine(ShakeAndBreak());
            }
        }
    }

    private IEnumerator ShakeAndBreak()
    {
        isShaking = true;
        float shakeElapsed = 0.0f;

        while (shakeElapsed < shakeDuration)
        {
            Vector3 randomPoint = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = new Vector3(randomPoint.x, originalPosition.y, randomPoint.z);
            shakeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
        
        yield return new WaitForSeconds(breakDelay - shakeDuration);

        gameObject.SetActive(false);
    }
}
