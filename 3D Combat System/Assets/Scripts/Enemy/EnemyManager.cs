using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float EnemyDetectionRange = 25;
    [SerializeField] float EnemyDirection = 5;
    [SerializeField] List<EnemyStateMachine> Enemies;
    private EnemyStateMachine currentTarget;
    public LayerMask layerMask;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        //using a mask to only check enemies/npcs find the object that collided with the player
        if (other.gameObject.GetComponent<EnemyStateMachine>() != null) 
        {
            Debug.Log("Add " + other.gameObject.ToString());
            Enemies.Add(other.gameObject.GetComponent<EnemyStateMachine>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyStateMachine>() != null)
        {
            if (Enemies.Contains(other.gameObject.GetComponent<EnemyStateMachine>()))
            {
                Debug.Log("Remove "+other.gameObject.ToString());
                Enemies.Remove(other.gameObject.GetComponent<EnemyStateMachine>());
            }
        }
    }

    private void Update()
    {
        if (Enemies.Count > 0)
            EnemyDetection();
    }


    private void EnemyDetection() 
    {
        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();



        RaycastHit info;

        if (Physics.SphereCast(camera.transform.position, 3f, camera.transform.forward, out info, EnemyDetectionRange, layerMask))
        {
            currentTarget = info.collider.transform.GetComponent<EnemyStateMachine>();
            currentTarget.gameObject.layer = LayerMask.NameToLayer("Highlighted");
        }
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, EnemyDetectionRange);

        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Camera.main.transform.forward) * EnemyDetectionRange;
        Gizmos.DrawRay(Camera.main.transform.position, direction);

    }
}
