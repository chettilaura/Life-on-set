using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndGame : MonoBehaviour
{
    public PlayableDirector PlayableDirector;

    public void Update()
    {
        StartCoroutine(StopGame());
    }

    public IEnumerator StopGame()
    {
        yield return new WaitForSecondsRealtime(40f);
        PlayableDirector.Stop();
        Application.Quit();
    }
}
