using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static bool IsGameStarter = false;
    public GameObject Name, PlayImage;

    public void PlayStart()
    {
        IsGameStarter = true;
        Name.SetActive(false);
        PlayImage.SetActive(false);
    }
}
