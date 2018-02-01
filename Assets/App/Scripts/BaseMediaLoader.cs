using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class BaseMediaLoader : MonoBehaviour
{
    public string MediaUrl;

    private string _currentMediaUrl;

    protected virtual void Update()
    {
        if (_currentMediaUrl != MediaUrl)
        {
            _currentMediaUrl = MediaUrl;
            StartCoroutine(StartLoadMedia());
        }
    }

    protected abstract IEnumerator StartLoadMedia();


    protected IEnumerator ExecuteRequest(string url, DownloadHandler handler)
    {
        var request = UnityWebRequest.Get(url);
        request.downloadHandler = handler;
        yield return request.SendWebRequest();
    }
}
