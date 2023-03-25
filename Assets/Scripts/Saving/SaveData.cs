using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Data
{

    public int mostRecentLevel;

}

public class SaveData : MonoBehaviour
{
    public SaveData inst;
    
    Data data;
    private string EncryptionKey = "1324354657687980";
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

    private string XOREncrypt(string str, string key)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            // XOR each character with the corresponding character in the key
            char c = (char)(str[i] ^ key[i % key.Length]);
            sb.Append(c);
        }
        return sb.ToString();
    }

    // Decrypts a string that was encrypted using XOR with a given key
    private string XORDecrypt(string str, string key)
    {
        return XOREncrypt(str, key); // XOR encryption and decryption are the same operation
    }
    public void WriteSave()
    {
        Data preSaveData = new Data();
        preSaveData.mostRecentLevel = SceneManager.GetActiveScene().buildIndex;
        try
        {
            // Serialize the data to JSON
            string json = JsonUtility.ToJson(preSaveData);

            // Encrypt the JSON string with XOR using the datetime as the key
            string encryptedJson = XOREncrypt(json, EncryptionKey);

            // Write the encrypted JSON string to the file
            
            File.WriteAllText(filePath, encryptedJson);

            Debug.Log("Data saved to file: " + filePath);
        }
        catch
        {
            Debug.LogError("Error saving data to file: " + filePath);
        }
    }
    public void WriteSave(Data data)
    {   
        try
        {
            // Serialize the data to JSON
            string json = JsonUtility.ToJson(data);

            // Encrypt the JSON string with XOR using the datetime as the key
            string encryptedJson = XOREncrypt(json, EncryptionKey);

            // Write the encrypted JSON string to the file
           
            File.WriteAllText(filePath, encryptedJson);

            Debug.Log("Data saved to file: " + filePath);
        }
        catch 
        {
            Debug.LogError("Error saving data to file: " + filePath);
        }

    }
    public Data ReadSave()
    {
        Data tempData = null;
    try
    {
        if (File.Exists(filePath))
        {
                
            // Read the encrypted JSON string from the file
            string encryptedJson = File.ReadAllText(filePath);

            // Decrypt the JSON string with XOR using the key
            string json = XORDecrypt(encryptedJson, EncryptionKey);

            // Deserialize the JSON string to a Data object
            tempData = JsonUtility.FromJson<Data>(json);
        }
        else
        {
            // Create a new instance of the Data class with default values
            tempData = new Data();
            tempData.mostRecentLevel = 0;
            WriteSave(tempData); // You can optionally save the default data to the file
        }
    }
    catch (Exception e)
    {
        Debug.LogError("Error reading file at: " + filePath + "\n" + e.Message);
    }
    return tempData;
        
    }
}

