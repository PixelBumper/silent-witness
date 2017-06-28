using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        MainController.SwitchScene("1_GrabGun");
    }

    public void FreeGame()
    {
        MainController.SwitchScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}