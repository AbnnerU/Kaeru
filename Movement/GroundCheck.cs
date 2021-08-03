using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDetect;
    private bool grounded;

    private void OnTriggerStay(Collider other)
    {
        if ((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            grounded = false;
    }

    public bool GetGrounded()
    {
        return grounded;
    }
}
