using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    [SerializeField] private MovePath movePath;

    [SerializeField] private ParticleSystem particleEffect;

    //[SerializeField] private ParticleSystem nInteracting;

    //[SerializeField] private ParticleSystem interacting;

    private void Awake()
    {
        //MovePath_OnMove(false);

        //movePath.OnMove += MovePath_OnMove;

        movePath.OnPathEnd += MovePath_OnPathEnd;
    }

    //private void MovePath_OnMove(bool moving)
    //{
    //    if (moving)
    //    {
    //        //print("Particle on");
    //        nInteracting.Stop();
    //        interacting.Play();
    //    }
    //    else
    //    {
    //        //print("Particle off");
    //        interacting.Stop();
    //        nInteracting.Play();
    //    }
    //}

    private void MovePath_OnPathEnd()
    {
        particleEffect.Stop();
    }

}
