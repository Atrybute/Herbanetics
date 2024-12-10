using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BuildDisplayer : MonoBehaviour
{
    private TextMeshProUGUI buildText;

    private void Awake()
    {
        buildText = GetComponent<TextMeshProUGUI>();
        ResourceRequest request = Resources.LoadAsync("Build", typeof(BuildScriptableObject));
        request.completed += Request_Completed;

    }

    private void Request_Completed(AsyncOperation obj)
    {
        BuildScriptableObject buildScriptableObject = ((ResourceRequest)obj).asset as BuildScriptableObject;

        if (buildScriptableObject == null)
        {
            Debug.LogError("Build scriptable object not found in resources directory! Check Build log for errors");
        }
        else
        {
            buildText.SetText($"Build:{Application.version}.{buildScriptableObject.BuildNumber}");
        }

    }


}
