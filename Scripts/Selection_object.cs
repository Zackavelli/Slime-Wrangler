using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_object : MonoBehaviour
{

    Game_Manager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<Game_Manager>();
        StartCoroutine("go_away");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slime")
        {
            //Debug.Log(collision.gameObject);
            Slime slime_class;
            slime_class = collision.GetComponent<Slime>();
            slime_class.looped();
        }

        if (collision.tag == "Slime_Food")
        {
            Slime_Food sf = collision.gameObject.GetComponent<Slime_Food>();
            sf.loop_food();
            gm.lines = gm.lines - 5;
            gm.win_fail_state();
        }
    }

    IEnumerator go_away()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

}
