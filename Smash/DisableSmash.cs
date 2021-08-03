using UnityEngine;

public class DisableSmash : MonoBehaviour
{
    [SerializeField] private SmashDetectionManeger smashDetectionManeger;

    public void EnableSmash(bool enable)
    {
        smashDetectionManeger.SetCanVerify(enable);
    }
}
