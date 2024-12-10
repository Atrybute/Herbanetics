using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MenuLoader", menuName = "Custom/MenuLoader")]
public class MenuLoader : ScriptableObject
{
    public LevelLoader levelLoader;
    
    public void LoadMenu(MenuData  menuData)
    {
        SceneManager.LoadScene(menuData.menuName);
    }

    public void PlayButtonPressed()
    {
        levelLoader.StartGame();
    }
}
