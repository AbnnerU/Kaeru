using UnityEngine;

public class SimpleGravity : MonoBehaviour
{
    [Header("Raycast Config")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform raycastStartPosition;

    [SerializeField] private float raycastSize;

    [Header("Gravity Simulation")]

    [SerializeField] private float gravitySpeed;

    [SerializeField] private bool playOnAwake;

    private void Awake()
    {
        if (playOnAwake == false)
        {
            DisableScript();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastStartPosition.position, -transform.up, raycastSize, groundLayer))
        {
            DisableScript();
        }
        else
        {
            transform.position -= Vector3.up * gravitySpeed * Time.deltaTime; 
        }

    }


    public void DisableScript()
    {
        SimpleGravity simpleGravity = this;
        simpleGravity.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(raycastStartPosition.position, new Vector3(raycastStartPosition.position.x, raycastStartPosition.position.y - raycastSize, raycastStartPosition.position.z));
    }
}
