using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVPrint : MonoBehaviour
{
     void Awake() {
 
        List<Dictionary<string,object>> data = CSVReader.Read ("Dialog");
 
        for(var i=0; i < data.Count; i++)
        {
            print(data[i]["Content"].ToString());    
            
        }
 
    }
}
