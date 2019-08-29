using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverTextobj;
    [SerializeField] GameObject gameClearTextobj;
    public void GameOver()
    {
        gameOverTextobj.SetActive(true);
        Invoke("ReStartThisScene",1f);
        //ReStartThisScene();
    }

    public void GameClear()
    {
        gameClearTextobj.SetActive(true);
        Invoke("ReStartThisScene", 1f);
        //ReStartThisScene();
    }

    void ReStartThisScene()
    {
        Scene ThisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(ThisScene.name);
    }
}
