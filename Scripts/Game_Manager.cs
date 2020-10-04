using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    UI_Manager ui;
    public int level;

    public int lines;
    public int slimes;
    public bool game_over;
    public AudioSource slime_audio;
    public AudioClip slime_looped;
    public AudioClip win_level;


    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game_over == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(level);
            }
        }

        if (lines <= 0 && slimes != 0)
        {
            Debug.Log("Game_Over");
            game_over = true;
            ui.call_game_over_panel();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(level);
        }
    }

    public void win_fail_state()
    {
        

        if(slimes <= 0)
        {
            
            Debug.Log("you_win");
            if (level != 11)
            {
                //load next level
                StartCoroutine("new_level_delay");
            }
        }

        
    }

    IEnumerator new_level_delay()
    {
        //yield return new WaitForSeconds(0.5f);
        slime_audio.PlayOneShot(win_level);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level + 1);
    }



}
