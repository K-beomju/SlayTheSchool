using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityCommonClass
{
    public static void SetPositionData(Transform trm ,Vector3 pos, Quaternion rot)
    {
        trm.position = pos;
        trm.rotation = rot;
    }

    public static void SetDeactive(GameObject obj)
    {
        obj.SetActive(false);
    }
}
