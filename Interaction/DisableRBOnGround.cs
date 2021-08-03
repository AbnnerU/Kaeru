using UnityEngine;

public class DisableRBOnGround : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDisable;

    [SerializeField] private Rigidbody rb;

    private bool active;

    private void OnEnable()
    {
        active = true;
    }

    private void OnDisable()
    {
        active = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (active)
        {
            GameObject obj = collision.gameObject;

            if ((layerToDisable & 1 << obj.layer) == 1 << obj.layer)
            {
                rb.velocity = Vector3.zero;

                rb.isKinematic = true;

                this.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            GameObject obj = other.gameObject;

            if ((layerToDisable & 1 << obj.layer) == 1 << obj.layer)
            {
                rb.velocity = Vector3.zero;

                rb.isKinematic = true;

                this.enabled = false;
            }
        }
    }
}
