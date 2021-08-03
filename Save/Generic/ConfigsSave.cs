
using UnityEngine;

public class ConfigsSave : MonoBehaviour
{
    #region CamConfig
    public static Vector2 GetCamAccelValues()
    {
        if (PlayerPrefs.HasKey("CamAccelX") == true && PlayerPrefs.HasKey("CamAccelY") == true)
        {
            Vector2 accelValues = new Vector2(PlayerPrefs.GetFloat("CamAccelX"), PlayerPrefs.GetFloat("CamAccelY"));
            return accelValues;
        }
        else
        {
            PlayerPrefs.SetFloat("CamAccelX", 1f);
            PlayerPrefs.SetFloat("CamAccelY",2f);

            Vector2 accelValues = new Vector2(PlayerPrefs.GetFloat("CamAccelX"), PlayerPrefs.GetFloat("CamAccelY"));

            return accelValues;
        }
    }

    public static Vector2 GetCamGhosthAccelValues()
    {
        if (PlayerPrefs.HasKey("CamAimAccelX") == true && PlayerPrefs.HasKey("CamAimAccelY") == true)
        {
            Vector2 aimAccelValues = new Vector2(PlayerPrefs.GetFloat("CamAimAccelX"), PlayerPrefs.GetFloat("CamAimAccelY"));

            return aimAccelValues;
        }
        else
        {
            PlayerPrefs.SetFloat("CamAimAccelX", 1f);
            PlayerPrefs.SetFloat("CamAimAccelY", 2f);

            Vector2 aimAccelValues = new Vector2(PlayerPrefs.GetFloat("CamAimAccelX"), PlayerPrefs.GetFloat("CamAimAccelY"));

            return aimAccelValues;
        }
    }

   
    public static void SetCamAccel(Vector2 value)
    {
        PlayerPrefs.SetFloat("CamAccelX", value.x);
        PlayerPrefs.SetFloat("CamAccelY", value.y);
    }

    public static void SetAimCamAccel(Vector2 value)
    {
        PlayerPrefs.SetFloat("CamAimAccelX", value.x);
        PlayerPrefs.SetFloat("CamAimAccelY", value.y);
    }

    #endregion

    #region Brilho

    public static float GetBrigtnessValue()
    {
        if (PlayerPrefs.HasKey("Brightness"))
        {
            float value = PlayerPrefs.GetFloat("Brightness");

            return value;
        }
        else
        {
            PlayerPrefs.SetFloat("Brightness", 0);

            return 0;
        }
    }

    public static void SetBrightnessValue(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
    }

    #endregion

    #region Audio

    public static int GetBGSound()
    {
        if (PlayerPrefs.HasKey("BG"))
        {
            int value = PlayerPrefs.GetInt("BG");

            return value;
        }
        else
        {
            PlayerPrefs.SetInt("BG", 1);

            return 1;
        }
    }

    public static void SetBGSound(int value)
    {
        PlayerPrefs.SetInt("BG", value);
    }


    public static int GetEffectSound()
    {
        if (PlayerPrefs.HasKey("ES"))
        {
            int value = PlayerPrefs.GetInt("ES");

            return value;
        }
        else
        {
            PlayerPrefs.SetInt("ES", 1);

            return 1;
        }
    }

    public static void SetEffectSound(int value)
    {
        PlayerPrefs.SetInt("ES", value);
    }


    public static int GetSoundsVolume()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            int value = PlayerPrefs.GetInt("Volume");

            return value;
        }
        else
        {
            PlayerPrefs.SetInt("Volume", 100);

            return 1;
        }
    }

    public static void SetSoundsVolume(int value)
    {
        PlayerPrefs.SetInt("Volume", value);
    }


    #endregion

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
