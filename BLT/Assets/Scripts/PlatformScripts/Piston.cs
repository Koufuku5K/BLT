using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public GameObject piston;
    private Vector3 startPos;
    private Vector3 endPos;
    private float elapsedTime;
    private float desiredDuration = 1f;
    private bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = piston.transform.localPosition;
        endPos = new Vector3(0.0f, startPos.y, startPos.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator MovePiston(Vector3 fromPos, Vector3 toPos)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < desiredDuration)
        {
            piston.transform.localPosition = Vector3.Lerp(fromPos, toPos, elapsedTime / desiredDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        piston.transform.localPosition = toPos;
    }

    private IEnumerator ExtendAndRetract()
    {
        yield return StartCoroutine(MovePiston(startPos, endPos));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(MovePiston(endPos, startPos));
        isTriggered = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isTriggered)
        {
            Vector3 contactNormal = other.contacts[0].normal;

            // Trigger only if the collision normal is nearly perfectly upwards
            if (-contactNormal.y > 0.9f)
            {
                isTriggered = true;
                StartCoroutine(ExtendAndRetract());
            }
        }
    }
}
