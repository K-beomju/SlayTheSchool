using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

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


    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
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

            if(time + 1 >= waitTime)
            {
                rotateSpeed = 0;
            }

            if (time > waitTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

   
}
