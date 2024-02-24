using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : SingletonBehaviour<UIScreenManager>
{
    [SerializeField]
    private GameObject UIPanel;
    // Start is called before the first frame update
    void Start()
    {
        UIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableGameOVerUI()
    {
        UIPanel.SetActive(true);
    }

    public void OnClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
