using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public SaveData inst;
    Data data;
    string filePath;
    void awake()
    {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        data = new Data();
        filePath = Application.dataPath + "/Save.json";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            WriteSave();
            
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene( ReadSave().mostRecentLevel); 
        }
    }
    public void WriteSave()
    {
        if (File.Exists(filePath))
        {
            // if there is a file at the path, dont make a new one,
            // Open the file in write mode and overwrite its contents
            FileStream fileStream = File.Open(filePath, FileMode.Truncate);
            fileStream.Close();
        }

        data.mostRecentLevel = SceneManager.GetActiveScene().buildIndex;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }
    public void WriteSave(Data data)
    {   
        if (File.Exists(filePath))
        {
            // if there is a file at the path, dont make a new one,
            // Open the file in write mode and overwrite its contents
            FileStream fileStream = File.Open(filePath, FileMode.Truncate);
            fileStream.Close();
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }
    public Data ReadSave()
    {
        Data tempData = null;
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if(json != null)
                {
                    tempData = JsonUtility.FromJson<Data>(json);
                   
                }

            }
            else
            {
                tempData = new Data();
                tempData.mostRecentLevel = 0;
                WriteSave(tempData);

            }
        }
        catch 
        {
            Debug.LogError("Error reading file at: " + filePath);
            
        }
        return tempData;
        
    }
}
[System.Serializable]
public class Data
{
    public int mostRecentLevel;
}
