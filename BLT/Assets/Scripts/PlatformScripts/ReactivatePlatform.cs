using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactivatePlatform : MonoBehaviour
{
    public static ReactivatePlatform Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReactivatePlatform(GameObject platform, float delay)
    {
        StartCoroutine(Reactivate(platform, delay));
    }

    private IEnumerator Reactivate(GameObject platform, float delay)
    {
        yield return new WaitForSeconds(delay);
        platform.SetActive(true);
    }
}
