using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{



    public static MonoBehaviour GetCreateObject<T>(ObjectPooling<MonoBehaviour> objectPool)
    {
        return objectPool.GetOrCreate();
    }

      public static MonoBehaviour GetCreateObjects<T>(ObjectPooling<MonoBehaviour>[] objectPool , int num)
    {
        return objectPool[num].GetOrCreate();
    }
}
