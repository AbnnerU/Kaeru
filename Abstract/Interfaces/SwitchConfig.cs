using UnityEngine;


public interface IDisableMovement
{
    void MovementScript(bool active);
}

public interface IOnSwitchActions
{
    void OnDeselectedAction();

    void OnSelectedAction();
}


public interface IOnSwitchAnimations
{
    void OnSwitchAnimation();

    void OnSelectedAnimation();
}