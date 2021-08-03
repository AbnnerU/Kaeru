using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConfigsApplier : MonoBehaviour
{
    private CinemachineFreeLook camPlayer;
    private CinemachineFreeLook camGhost;

    //public List<AudioSource> effectsAudios;
    //public List<AudioSource> backgroundAudios;

    private GameObject[] allESAudios;

    private GameObject[] allBGAudios;

    private void Start()
    {
        camPlayer = GameObject.FindGameObjectWithTag("Normal Cam")?.GetComponent<CinemachineFreeLook>();

        camGhost = GameObject.FindGameObjectWithTag("Ghost Cam")?.GetComponent<CinemachineFreeLook>();

        allESAudios = GameObject.FindGameObjectsWithTag("ES");
        //GameObject[] allESAudios2 = GameObject.FindGameObjectsWithTag("ES");

        allBGAudios = GameObject.FindGameObjectsWithTag("BG");

        //foreach(GameObject g in allESAudios)
        //{
        //    AudioSource source = g.GetComponent<AudioSource>();

        //    if (source != null)
        //    {
        //        effectsAudios.Add(source);
        //    }
        //}

        //foreach (GameObject g in allBGAudios)
        //{
        //    AudioSource source = g.GetComponent<AudioSource>();

        //    if (source != null)
        //    {
        //        backgroundAudios.Add(source);
        //    }
        //}

        SetConfigs();
    }


    public void SetConfigs()
    {
        if (camPlayer != null)
        {
            //Cameras
            Vector2 camPlayerValues = ConfigsSave.GetCamAccelValues();
            Vector2 camGhostValues = ConfigsSave.GetCamGhosthAccelValues();


            camPlayer.m_XAxis.m_AccelTime = camPlayerValues.x;
            camPlayer.m_YAxis.m_AccelTime = camPlayerValues.y;

            camGhost.m_XAxis.m_AccelTime = camGhostValues.x;
            camGhost.m_YAxis.m_AccelTime = camGhostValues.y;
        }
        //Audio

        if (ConfigsSave.GetBGSound() == 1)
        {
            foreach(GameObject g in allBGAudios)
            {
                AudioSource source = g.GetComponent<AudioSource>();

                source.enabled = true;

                SetVolume(g, source);
            }
        }
        else
        {
            foreach (GameObject g in allBGAudios)
            {
                AudioSource source = g.GetComponent<AudioSource>();

                source.enabled = false;
            }
        }


        if (ConfigsSave.GetEffectSound() == 1)
        {
            foreach (GameObject g in allESAudios)
            {
                AudioSource source = g.GetComponent<AudioSource>();

                source.enabled = true;

                SetVolume(g, source);
            }
        }
        else
        {
            foreach (GameObject g in allESAudios)
            {
                AudioSource source = g.GetComponent<AudioSource>();

                source.enabled = false;
            }
        }
    }

    private void SetVolume(GameObject obj,AudioSource source)
    {
        DefaltVolume defaltVolume = obj.GetComponent<DefaltVolume>();

        if (defaltVolume != null)
        {
            float defaltValue = defaltVolume.GetDefaltVolume();

            int volume = ConfigsSave.GetSoundsVolume();

            float newVolume = (defaltValue * volume) / 100;

            source.volume = newVolume;
        }
    }
}
