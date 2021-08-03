using System;
using UnityEngine.SceneManagement;
using UnityEngine;
//using UnityEngine.Events;

//[Serializable] public class OnDeathEvent : UnityEvent { }

public class Death : MonoBehaviour
{
    [SerializeField] private LayerMask layerToDetect;

    //public OnDeathEvent onDeathEvent;

    private CanvasManeger_Game canvasManeger;

    private void Start()
    {
        canvasManeger = FindObjectOfType<CanvasManeger_Game>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((layerToDetect & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Die();
        }
    }

    public void Die()
    {
        print("OLLAAA");
        canvasManeger.StartPanelTrasition();
    }
}
