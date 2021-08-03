
using UnityEngine;

public abstract class WaterFlowBasics : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] protected BoxCollider boxCollider;

    [SerializeField] protected float waterSpeed;

    [SerializeField] protected float maxObjectSpeedOnWater;

    protected Transform _transform;

    protected float waterLevel;

    protected virtual void Awake()
    {
        _transform = GetComponent<Transform>();

        waterLevel = _transform.position.y + (boxCollider.size.y / 2);

    }
}
