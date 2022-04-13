using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugCommand : MonoBehaviour
{
    [MenuItem("MGSaveData", menuItem = "Debug/MGSaveData/Save")]
    public static void MGSave()
    {
        MGSaveData.instance.Save();
    }

     [MenuItem("MGSaveData", menuItem = "Debug/MGSaveData/Load")]
    public static void MGLoad()
    {
        MGSaveData.instance.Load();
    }

       [MenuItem("MGSaveData", menuItem = "Debug/MGSaveData/Delete")]
    public static void MGDelete()
    {
        MGSaveData.instance.Delete();
    }
}
