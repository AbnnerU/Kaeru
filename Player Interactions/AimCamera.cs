using Cinemachine;
using UnityEngine;

public class AimCamera : MonoBehaviour
{
    [SerializeField] private InteractionRaycast interactionRaycast;

    [SerializeField] private GhostMovement movement;

    [SerializeField] private CinemachineFreeLook characterCameraMain;

    [SerializeField] private CinemachineFreeLook characterCameraAim;

    [Header("Camera LookAt Config")]
    [SerializeField] private Transform cameraLookAtReference;

    [SerializeField] private Vector3 whenAimingReferencePos;

    [Header("Camera Config")]
    [SerializeField] private float aimingFielOfView;   

    //[Header("Y Axis")]
    //[SerializeField] private bool invertY;

    //[SerializeField] private float maxSpeedY;

    //[SerializeField] private float accelTimeY;

    //[Header("X Axis")]
    //[SerializeField] private float maxSpeedX;

    //[SerializeField] private float accelTimeX;

    //[Header("Orbits")]

    //[SerializeField] private CinemachineFreeLook.Orbit[] orbits;
    //[SerializeField] private float topRigHeigth;

    //[SerializeField] private float topRigRadius;

    //[SerializeField] private float middleRigHeigth;

    //[SerializeField] private float middleRigRadius;

    //[SerializeField] private float bottomRigHeigth;

    //[SerializeField] private float bottomRigRadius;




    private Vector3 defalftReferencePosition;

    //private float defaltFielOfView;


    private void Awake()
    {
        //interactionRaycast.OnAim += InteractionRaycast_OnAim;

        defalftReferencePosition = cameraLookAtReference.localPosition;

        //defaltFielOfView = characterCamera.m_Lens.FieldOfView;

    }

    private void InteractionRaycast_OnAim(bool aiming)
    {
        if (aiming)
        {           
            //characterCameraMain.m_Lens.FieldOfView = aimingFielOfView;

            AimingCamera();

            movement.SetDefaltMomevemt(false);
        }
        else
        {
            SetNotAiming();
        }
    }

    public void SetNotAiming()
    {
        cameraLookAtReference.localPosition = defalftReferencePosition;
      
        //characterCamera.m_Lens.FieldOfView = defaltFielOfView;

        characterCameraMain.m_XAxis.Value = characterCameraAim.m_XAxis.Value;

        characterCameraMain.m_YAxis.Value = characterCameraAim.m_YAxis.Value;

        characterCameraAim.Priority = -1;

        movement.SetDefaltMomevemt(true);
    }

    public void AimingCamera()
    {
        cameraLookAtReference.localPosition = whenAimingReferencePos;
        ////Y Axis 
        //characterCamera.m_YAxis.m_InvertInput = invertY;
        //characterCamera.m_YAxis.m_MaxSpeed = maxSpeedY;
        //characterCamera.m_YAxis.m_AccelTime = accelTimeY;

        ////X Axis
        //characterCamera.m_XAxis.m_MaxSpeed = maxSpeedX;
        //characterCamera.m_XAxis.m_AccelTime = accelTimeX;

        ////Orbits
        characterCameraAim.m_XAxis.Value = characterCameraMain.m_XAxis.Value;

        characterCameraAim.m_YAxis.Value = characterCameraMain.m_YAxis.Value;

        characterCameraAim.Priority = 2;


    }
}
