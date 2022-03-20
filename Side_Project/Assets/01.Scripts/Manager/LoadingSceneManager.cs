using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] 
    private RectTransform loadingImg;

    [SerializeField]
    private float rotateSpeed;
    [SerializeField] 
    private string SceneName;

    [SerializeField]
    private float waitTime;

    private float time;

    void Start()
    {
        StartCoroutine(LoadAsynSceneCoroutine());
    }

    IEnumerator LoadAsynSceneCoroutine()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        operation.allowSceneActivation = false;



        while (!operation.isDone)
        {

            time =+ Time.time;

            loadingImg.Rotate(0,0,-rotateSpeed * Time.deltaTime);

            if (time > waitTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

   
}
