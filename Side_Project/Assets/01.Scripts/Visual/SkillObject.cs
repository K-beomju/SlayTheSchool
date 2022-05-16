using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SkillObject : MonoBehaviour
{
    public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

    public void SetDeactive()
    {
        gameObject.SetActive(false);
    }

   

    
}
