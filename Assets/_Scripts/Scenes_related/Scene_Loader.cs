using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Scene_Loader 
{



public enum Scene{
    Loading,
    Camper, 
    Esterno, 
    StartMenu,
    Studio1,
    Studio2

}

    private static Action onLoaderCallBack;


    //"Load" riceve la target scene we want to load    
    public static void Load(Scene scene)
    {
        //set the callback action to load the target scene
        onLoaderCallBack = () =>
        {
     
            SceneManager.LoadScene(scene.ToString());
        };

        //set the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());   
    }


    public static void LoaderCallback()
    {
        //execute the loader callback action which will load the target scene
        if(onLoaderCallBack!=null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }

        
    }



}
