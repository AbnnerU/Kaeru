using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save
{
    #region SaveLevel
    public static void SaveLevelName(LevelName levelName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "level.level";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, levelName);

        stream.Close();
    }

    public static LevelName LoadLevel()
    {
        string path = Application.persistentDataPath + "level.level";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            LevelName name = formatter.Deserialize(stream) as LevelName;

            stream.Close();

            return name;
        }
        else
        {
            Debug.LogError("Não exite");

            return null;
        }
    }
    #endregion

    #region DefaltObjects
    public static void SaveObjectsPositions(ObjectsData objectsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "savableObjects.level";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, objectsData);

        stream.Close();
    }

    public static ObjectsData LoadObjectsPosition()
    {
        string path = Application.persistentDataPath + "savableObjects.level";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            ObjectsData objectsData = formatter.Deserialize(stream) as ObjectsData;

            stream.Close();

            return objectsData;
        }
        else
        {
            Debug.LogError("Não exite objetos posição");

            return null;
        }
    }
    #endregion

    #region Plataforms
    public static void SavePlataformsPositions(PlataformsData plataformsData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "plataformsSave.level";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, plataformsData);

        stream.Close();
    }

    public static PlataformsData LoadPlataformsPosition()
    {
        string path = Application.persistentDataPath + "plataformsSave.level";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlataformsData plataformsData = formatter.Deserialize(stream) as PlataformsData;

            stream.Close();

            return plataformsData;
        }
        else
        {
            Debug.LogError("Não exite plataformas posição");

            return null;
        }
    }

    #endregion

    public static void DeletePositionsData()
    {
        string path = Application.persistentDataPath + "savableObjects.level";

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        string path2 = Application.persistentDataPath + "plataformsSave.level";

        if (File.Exists(path2))
        {
            File.Delete(path2);
        }

        
    }

    public static void DeleteAllData()
    {
        DeletePositionsData();

        string path = Application.persistentDataPath + "level.level";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}


