using UnityEngine;

public class LinkScript : MonoBehaviour
{
    public void OpenExternalLink(string url)
    {
        Application.OpenURL(url);
    }
}
