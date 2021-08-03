using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ConfigsManeger : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;

    [SerializeField] private ConfigsApplier configsApplier;
    [SerializeField] private int maxCamValues=10;
    [SerializeField] private Slider camSensiSliderX;
    [SerializeField] private Slider camSensiSliderY;
    [SerializeField] private Slider camAimSliderX;
    [SerializeField] private Slider camAimSliderY;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle bgToggle;
    [SerializeField] private Toggle esToggle;


    [SerializeField] private TMP_Text camTextX;
    [SerializeField] private TMP_Text camTextY;
    [SerializeField] private TMP_Text camAimTextX;
    [SerializeField] private TMP_Text camAimTextY;

    private float camSensiX;
    private float camSensiY;

    private float camAimSensiX;
    private float camAimSensiY;

    private float brightnessValue;

    private int bgAudio;
    private int esAudio;
    private float soundVolume;


    private void Awake()
    {
        volume = GameObject.FindGameObjectWithTag("Brilho").GetComponent<Volume>();

        brightnessValue = ConfigsSave.GetBrigtnessValue();

        bgAudio = ConfigsSave.GetBGSound();

        esAudio = ConfigsSave.GetEffectSound();

        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        soundVolume = ConfigsSave.GetSoundsVolume();
        
    }

    public void ApllyVisualChanges()
    {
        //Camera
        float bValue = ConfigsSave.GetBrigtnessValue();

        Vector2 values= ConfigsSave.GetCamAccelValues();
        Vector2 valuesAim = ConfigsSave.GetCamGhosthAccelValues();

        camSensiSliderX.value = Mathf.Abs(values.x - maxCamValues);
        camSensiSliderY.value = Mathf.Abs(values.y - maxCamValues);

        camAimSliderX.value = Mathf.Abs(valuesAim.x - maxCamValues);
        camAimSliderY.value = Mathf.Abs(valuesAim.y - maxCamValues);

        camTextX.text = Mathf.Abs(values.x - maxCamValues).ToString();
        camTextY.text = Mathf.Abs(values.y - maxCamValues).ToString();

        camAimTextX.text = Mathf.Abs(valuesAim.x - maxCamValues).ToString();
        camAimTextY.text = Mathf.Abs(valuesAim.y - maxCamValues).ToString();

        brightnessSlider.value = bValue;
        brightnessValue = bValue;

        bgAudio = ConfigsSave.GetBGSound();
        esAudio = ConfigsSave.GetEffectSound();

        bgToggle.isOn = IntToBool(bgAudio);
        esToggle.isOn = IntToBool(esAudio);

        soundVolume = ConfigsSave.GetSoundsVolume();

        volumeSlider.value = soundVolume;

    }

    private bool IntToBool(int value)
    {
        if (value == 0)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }

    public void ApplyConfigsChanges()
    {
        //Camera

        Vector2 sensiValues = new Vector2(camSensiX, camSensiY);
        Vector2 sensiAimValues = new Vector2(camAimSensiX, camAimSensiY);

        ConfigsSave.SetCamAccel(sensiValues);
        ConfigsSave.SetAimCamAccel(sensiAimValues);
        ConfigsSave.SetBrightnessValue(brightnessValue);

       

        //Brilho
        colorAdjustments.postExposure.value = brightnessValue;

        //Som

        ConfigsSave.SetBGSound(bgAudio);
        ConfigsSave.SetEffectSound(esAudio);
        ConfigsSave.SetSoundsVolume((int)soundVolume);

        if (configsApplier != null)
        {
            configsApplier.SetConfigs();
        }

    }

    public void SetBrightness(float value)
    {
        brightnessValue = value;
    }

    #region Camera
    public void SetCamSensiX(float value)
    {
        float finalValue = (Mathf.Round(value * 100)) / 100;

        camTextX.text = finalValue.ToString();

        camSensiX = maxCamValues - finalValue;
    }

    public void SetCamSensiY(float value)
    {
        float finalValue = (Mathf.Round(value * 100)) / 100;

        camTextY.text = finalValue.ToString();

        camSensiY = maxCamValues - finalValue;
    }

    public void SetCamAimSensiX(float value)
    {
        float finalValue = (Mathf.Round(value * 100)) / 100;

        camAimTextX.text = finalValue.ToString();

        camAimSensiX = maxCamValues - finalValue;
    }

    public void SetCamAimSensiY(float value)
    {
        float finalValue = (Mathf.Round(value * 100)) / 100;

        camAimTextY.text = finalValue.ToString();

        camAimSensiY = maxCamValues - finalValue;
    }
    #endregion

    #region Audio

    public void SetBGValue(bool value)
    {
        if (value)
        {
            bgAudio = 1;
        }
        else
        {
            bgAudio = 0;
        }
    }

    public void SetESValue(bool value)
    {
        if (value)
        {
            esAudio = 1;
        }
        else
        {
            esAudio = 0;
        }
    }

    public void SetVolume(float value)
    {
        soundVolume = value;
    }

    #endregion
}
