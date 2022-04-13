using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public  class MGSaveData : MonoBehaviour
{
    private static MGSaveData _instance;
    public static MGSaveData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(MGSaveData)) as MGSaveData;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<MGSaveData>();
                }
            }

            return _instance;
        }
    }

    private string fileName = "svDt.dat";

    private SaveData saveData;

    private static BinaryFormatter _binaryFormatter;
    private static FileStream _fileStream;

    public void generate()
    {
        Debug.LogWarning("-------------------");
        Load();
        GameManager.Instance.SetText();

    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);


    }

    private void Start()
    {
        generate();
    }

    [ContextMenu("SAVE")]
    public void Save()
    {
        Debug.LogWarning("데이터를 저장합니다.");
        _binaryFormatter = new BinaryFormatter();
        _fileStream = File.Create(getFilePath(fileName));

        _binaryFormatter.Serialize(_fileStream, saveData);

        _fileStream.Close();
        GameManager.Instance.SetText();
    }

    [ContextMenu("LOAD")]
    public void Load()
    {
        saveData = new SaveData();
        int classVer = saveData.version;

        try
        {
            if (File.Exists(getFilePath(fileName)))
            {
                _binaryFormatter = new BinaryFormatter();
                _fileStream = File.Open(getFilePath(fileName), FileMode.Open);

                saveData = (SaveData)_binaryFormatter.Deserialize(_fileStream);

                int fileVer = saveData.version;

                // 세이브 파일 버전 체크
                if (classVer != fileVer)
                {
                    // 바뀐 버전에 맞는 처리를 해줘야 함
                    Debug.LogFormat("Savefile version : class:{0},file:{1}", classVer, fileVer);
                }



                _fileStream.Close();
            }
            else
            {
                // Null File => Create File 
                Debug.LogWarning("Create New SaveFile!");
                Save();
            }

            Debug.LogWarning("데이터를 로드합니다.");

        }
        catch (System.Exception e)
        {
            Debug.LogError("Error Loading Save " + e);
            File.Delete(getFilePath(fileName));
            Save();
        }
    }

    [ContextMenu("Delete")]
    public  void Delete()
    {
        if(File.Exists(getFilePath(fileName)))
        {
            try
            {
                File.Delete(getFilePath(fileName));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

    }

    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public SaveData GetSaveData()
    {
        return saveData;
    }

    public UserInfoData GetUserInfoData()
    {
        return saveData._myUserInfoDt;
    }




}
