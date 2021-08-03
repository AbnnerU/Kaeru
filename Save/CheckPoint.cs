
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int checkPointNumber=1;

    public OnTouch onSave;

    private ObjectPositionSave positionSave;

    private void Awake()
    {
        if (checkPointNumber < 1)
        {
            checkPointNumber = 1;
        }
        positionSave = FindObjectOfType<ObjectPositionSave>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Colidindo");
        if(checkPointNumber > positionSave.GetCheckPointNumber())
        {
            positionSave.SaveData(checkPointNumber);

            gameObject.GetComponent<Collider>().enabled = false;

            onSave?.Invoke();
            //print("SALVO");
        }
    }
}
