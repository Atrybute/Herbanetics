using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildIncrementor : IPreprocessBuildWithReport
{
    public int callbackOrder => 1;

    public void OnPreprocessBuild(BuildReport report)
    {
        BuildScriptableObject buildScriptableObject = ScriptableObject.CreateInstance<BuildScriptableObject>();

        switch (report.summary.platform)
        {
            case BuildTarget.iOS:
                PlayerSettings.iOS.buildNumber = IncrementBuildNumber(PlayerSettings.iOS.buildNumber);
                buildScriptableObject.BuildNumber = PlayerSettings.iOS.buildNumber;
                break;
            case BuildTarget.Android:
                PlayerSettings.Android.bundleVersionCode++;
                buildScriptableObject.BuildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
                break;
        }
        AssetDatabase.DeleteAsset("Assets/Resources/Build.asset");
        AssetDatabase.CreateAsset(buildScriptableObject, "Assets/Resources/Build.asset");
        AssetDatabase.SaveAssets();

    }

    private string IncrementBuildNumber(string buildNumber)
    {
        int.TryParse(buildNumber, out int outputBuildNumber);
        return (outputBuildNumber + 1).ToString();
    }
}
