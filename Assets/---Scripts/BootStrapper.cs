using UnityEngine;

public static class BootStrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Exectute() => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
 
}
