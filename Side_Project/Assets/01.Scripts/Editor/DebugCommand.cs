using UnityEditor;

public class DebugCommand
{
    [MenuItem("MGSaveData", menuItem = "MenuItem/MGSaveData/Save")]
    public static void MGSave()
    {
        MGSaveData.instance.Save();
    }

    [MenuItem("MGSaveData", menuItem = "MenuItem/MGSaveData/Load")]
    public static void MGLoad()
    {
        MGSaveData.instance.Load();
    }

    [MenuItem("MGSaveData", menuItem = "MenuItem/MGSaveData/Delete")]
    public static void MGDelete()
    {
        MGSaveData.instance.Delete();
    }
}
