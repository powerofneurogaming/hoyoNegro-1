using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //public Animator transition;
    public float transitionTime;
    public Image callToBlack;
    public bool load;


    public void LoadNextLevel( bool load)
    {
        float time = 1f;
        float start = 0;
        float finish = 0;
        if (load)
        {
            start = 0; finish = 1;
        }
        else
        {
            start = 1; finish = 0;
        }

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, time,start, finish, load));
        //updating = true;
    }

    IEnumerator LoadLevel(int levelIndex,float time, float start, float finish,bool load)
    {
        //transition.SetTrigger("Start");
        float timeKeep = 0f;
        float startTime = Time.time;
        float elapsed = 0f;
        while(timeKeep<= time)
        {
            elapsed = Time.time - startTime;
            timeKeep += Time.deltaTime;
            var color = callToBlack.color;
            color.a = Mathf.Lerp(start,finish, elapsed / time);
            //Debug.Log(color.a);
            callToBlack.color= color;
            //Debug.Log(callToBlack.color.a);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if(load == true) {
        SceneManager.LoadScene(levelIndex);
        }
    }
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "CaveScene")
        {
            LoadNextLevel(false);
        }
    }
}

