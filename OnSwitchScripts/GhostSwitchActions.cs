using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSwitchActions : MonoBehaviour,IOnSwitchActions
{
    [SerializeField] private GhostOnLightActions ghostOnLight;
    [SerializeField] private InteractionRaycast interactionRaycast;
    //[SerializeField] private AimCamera aimCamera;
    [SerializeField] private CanvasAim canvasAim;

    [SerializeField] private GhostMovement ghostMovement;
    [SerializeField] private Transform spawnPointReference;
    [SerializeField] private Transform parentOnSelected;
    [SerializeField] private GameObject mesh;
    [SerializeField] private Collider ghostCollider;

    [SerializeField] private AudioListener audioListener;
    [SerializeField] private AudioSource audioSource;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        OnDeselectedAction();
    }

    public void OnSelectedAction()
    {
        audioSource.Play();

        interactionRaycast.enabled = true;

        audioListener.enabled = true;

        //aimCamera.SetNotAiming();

        _transform.position = spawnPointReference.position;

        _transform.SetParent(parentOnSelected);
        //transform.localPosition = Vector3.zero;
        SetActive(true);

    }

    public void OnDeselectedAction()
    {
        ghostOnLight.StopExecutingCoroutaines();

        interactionRaycast.StopInteraction();

        audioListener.enabled = false;

        interactionRaycast.enabled = false;        

        //aimCamera.SetNotAiming();

        canvasAim.InteractionRaycast_OnEnableInteraction(false);

        SetActive(false);
    }

    public void SetActive(bool active)
    {
        mesh.SetActive(active);
        ghostCollider.enabled = active;

        ghostMovement.enabled = active;
    }

    
}
