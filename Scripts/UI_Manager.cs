using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    Game_Manager gm;


    public TextMeshProUGUI lines;
    public TextMeshProUGUI slimes;
    public GameObject game_over_panel;



    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<Game_Manager>();
        game_over_panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        lines.text = gm.lines.ToString();
        slimes.text = gm.slimes.ToString();
        //call_game_over_panel();
    }

    public void call_game_over_panel()
    {
        StartCoroutine("game_over_delay");
    }

    IEnumerator game_over_delay()
    {
        yield return new WaitForSeconds(0.75f);

        if (gm.lines <= 0 && gm.slimes != 0)
        {
            game_over_panel.SetActive(true);
        }
    }
}
