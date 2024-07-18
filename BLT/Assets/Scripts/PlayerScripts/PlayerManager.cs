using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerMovement;
    private PlayerMovement pm;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        pm = playerMovement.GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }

    private void Respawn()
    {
        transform.position = spawnPoint.position;
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pm.isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pm.isGrounded = false;
        }
    }

    // If player falls into the water, player dies
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Die();
            Respawn();
        }
    }
}
