using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{
    public Material SceneTransMat;
    public float TransTime = 1f;
    private string propertyName = "_Progress";
    public UnityEvent OnTransDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Transitioning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Transitioning()
    {
        float currentTime = 0f;
        while (currentTime < TransTime)
        {
            currentTime += Time.deltaTime;
            SceneTransMat.SetFloat(propertyName, Mathf.Clamp01(currentTime/TransTime));
            yield return null;
        }
        OnTransDone?.Invoke();
    }
}
