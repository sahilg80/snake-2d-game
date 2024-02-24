using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScreenUIHandler : MonoBehaviour
{

    public void OnClickStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
