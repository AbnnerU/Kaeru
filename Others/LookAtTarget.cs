
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]private Transform target;

    Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        Quaternion direction = Quaternion.LookRotation(target.position - _transform.position);
        
        direction.x = 0;
        direction.z = 0;


        _transform.rotation= Quaternion.RotateTowards(_transform.rotation, direction, 100 * Time.deltaTime);
    }
}
