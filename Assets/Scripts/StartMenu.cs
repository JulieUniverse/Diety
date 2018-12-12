using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    public Image soundIcon;
    public Sprite muteSprite;
    public Sprite unmuteSprite;
    public string sceneName;
    public void MuteOverride()
    {
        
        soundIcon.overrideSprite = muteSprite;

    }

    public void UnmuteOverride()
    {
        soundIcon.overrideSprite = unmuteSprite;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
