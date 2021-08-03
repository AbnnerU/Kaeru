using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionSave:MonoBehaviour
{
    [SerializeField] private SavebleObject[] savebleObjects;

    [SerializeField] private PlataformStateSave[] plataformStateSaves;

    private int currentCheckPointNumber=0;

    private void Start()
    {
        savebleObjects = FindObjectsOfType<SavebleObject>();

        plataformStateSaves = FindObjectsOfType<PlataformStateSave>();

        LoadData();
    }

    #region Objects
    public void SaveCurrentPositions(int checkpointNumber)
    {
        currentCheckPointNumber = checkpointNumber;

        ObjectsData objectsData = new ObjectsData();

        objectsData.checkPointNumber = checkpointNumber;
        objectsData.objectName = new string[savebleObjects.Length];
        objectsData.positionX = new float[savebleObjects.Length];
        objectsData.positionY = new float[savebleObjects.Length];
        objectsData.positionZ = new float[savebleObjects.Length];

        for (int i = 0; i < savebleObjects.Length; i++)
        {
            Vector3 position = savebleObjects[i].GetPosition();

            objectsData.objectName[i] = savebleObjects[i].GetName();

            objectsData.positionX[i] = position.x;

            objectsData.positionY[i] = position.y;

            objectsData.positionZ[i] = position.z;

            
        }

        Save.SaveObjectsPositions(objectsData);
    }

    public void LoadPositions()
    {
        if (Save.LoadObjectsPosition()!=null)
        {
            ObjectsData objectsData = Save.LoadObjectsPosition();

            currentCheckPointNumber = objectsData.checkPointNumber;

            for (int i = 0; i < objectsData.objectName.Length; i++)
            {
                GameObject sceneObject = GameObject.Find(objectsData.objectName[i]);

                sceneObject.transform.position = new Vector3(objectsData.positionX[i], objectsData.positionY[i], objectsData.positionZ[i]);
            }

            
        }
    }
    #endregion

    #region Plataforms
    public void SaveCurrentPlataformsPositions()
    {
        PlataformsData plataformsData = new PlataformsData();

        plataformsData.objectName = new string[plataformStateSaves.Length];
        plataformsData.canMove = new bool[plataformStateSaves.Length];
        plataformsData.positionX = new float[plataformStateSaves.Length];
        plataformsData.positionY = new float[plataformStateSaves.Length];
        plataformsData.positionZ = new float[plataformStateSaves.Length];


        for (int i = 0; i < plataformStateSaves.Length; i++)
        {
            Vector3 position = plataformStateSaves[i].GetPosition();

            plataformsData.objectName[i] = plataformStateSaves[i].GetName();

            plataformsData.canMove[i] = plataformStateSaves[i].GetCanMove();

            plataformsData.positionX[i] = position.x;

            plataformsData.positionY[i] = position.y;

            plataformsData.positionZ[i] = position.z;


        }

        Save.SavePlataformsPositions(plataformsData);
    }

    public void LoadPlataformsPositions()
    {
        if (Save.LoadPlataformsPosition() != null)
        {
            PlataformsData plataformsData = Save.LoadPlataformsPosition();

            for (int i = 0; i < plataformsData.objectName.Length; i++)
            {               
                if (plataformsData.canMove[i] == true)
                {
                    GameObject sceneObject = GameObject.Find(plataformsData.objectName[i]);

                    sceneObject.GetComponent<MovePath>().SetPlataformLastOnPosition();

                    sceneObject.transform.position = new Vector3(plataformsData.positionX[i], plataformsData.positionY[i], plataformsData.positionZ[i]);
                }
            }
        }
    }
    #endregion

    public int GetCheckPointNumber()
    {
        return currentCheckPointNumber;
    }

    public void SaveData(int checkPoint)
    {
        SaveCurrentPositions(checkPoint);

        SaveCurrentPlataformsPositions();     
    }

    public void LoadData()
    {
        LoadPositions();

        LoadPlataformsPositions();
    }

    public void DeleteData()
    {
        Save.DeletePositionsData();
    }
}


[System.Serializable]
public class ObjectsData
{
    public int checkPointNumber;
    public string[] objectName;
    public float[] positionX;
    public float[] positionY;
    public float[] positionZ;  
}

[System.Serializable]
public class PlataformsData
{
    public string[] objectName;
    public bool[] canMove;
    public float[] positionX;
    public float[] positionY;
    public float[] positionZ;
}




