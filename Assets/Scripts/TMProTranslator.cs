using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using UnityEditor;

public class TMProTranslator : MonoBehaviour
{
    [HideInInspector] string language;
    void Start()
    {
        return;
        string path = Application.dataPath + "\\configs\\config.json";

        if(File.Exists(path))
        {
            string str = File.ReadAllText(path);
            JsonUtility.ToJson(str);
            language = JsonUtility.FromJson<string>("language");
            

            Debug.Log(language);
           

        }

        TMProTranslate[] tMPros;

        tMPros = FindObjectsOfType<TMProTranslate>();
    }

    void setLanguage(string str)
    {

    }

    void getLaneInFile(string key)
    {

    }
}
