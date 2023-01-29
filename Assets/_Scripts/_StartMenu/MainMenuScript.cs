using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image loadingProgressBar;


    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    void StartGame()
    {
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }


    public void HideMenu()
    {
        menu.SetActive(false);
    }


    public void ShowLoadingScreen(){
        loadingInterface.SetActive(true);

    }
    
    IEnumerator LoadingScreen(){

        float totalProgress = 0f;
        for(int i=0; i<scenesToLoad.Count; i++){
            while(!scenesToLoad[i].isDone){
                totalProgress += scenesToLoad[i].progress;
                //loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }

        }

    public void QuitGame()
    {
        Application.Quit();


    }

}



