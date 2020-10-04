using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slime : MonoBehaviour
{

    DrawManager dm;
    Game_Manager gm;
    Enemy_ai ai;

    public string slime_color;
    public int life;
    //public GameObject life_text_obj_prefab;
    //public GameObject life_text_obj;
    public TextMesh life_text;
    int randx;
    int randy;





    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DrawManager>();
        gm = FindObjectOfType<Game_Manager>();
        ai = gameObject.GetComponent<Enemy_ai>();
        //life_text_obj.transform.position = this.transform.position;
        life_text.text = life.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void capture()
    {
        if(life == 0)
        {
            gm.slimes = gm.slimes - 1;
            gm.win_fail_state();
            Destroy(gameObject);
            
        }
    }

    public void looped()
    {
        Debug.Log("Looped a Slime");
        life = life - 1;
        life_text.text = life.ToString();
        capture();
        if ((slime_color == "Red" || slime_color == "Meta") && ai.speed < 10)
        {
            ai.speed = ai.speed + 1;
        }

        if(slime_color == "Gold")
        {
            gm.lines = gm.lines + 6;
        }

        gm.slime_audio.PlayOneShot(gm.slime_looped);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Line"){

            Debug.Log("A Slime hit the line!");
            dm.Line_Break();
            Instantiate(dm.Line_break_ps, this.transform.position, this.transform.rotation);
        }

        if (collision.gameObject.tag == "Slime" && (slime_color == "Green" || slime_color == "Meta" ))
        {
            Slime other_slime_class = collision.gameObject.GetComponent<Slime>();

            if (other_slime_class.life < 6)
            {
                other_slime_class.life = other_slime_class.life + 1;
                other_slime_class.life_text.text = other_slime_class.life.ToString();
            }

            
        }

        if (collision.gameObject.tag == "Slime" && (slime_color == "Orange" || slime_color == "Meta"))
        {
            Enemy_ai other_ai_class = collision.gameObject.GetComponent<Enemy_ai>();

            if (other_ai_class.speed < 8)
            {
                other_ai_class.speed = other_ai_class.speed + 1;
                //other_slime_class.life_text.text = other_slime_class.life.ToString();
                //Spawn Orange Heart
            }


        }

        if (collision.gameObject.tag == "Slime_Food")
        {
            //turn around
            //Debug.Log("Touched Box");
            if (ai.target_position.x > 0)
            {
                randx = Random.Range(-7, 0);
            }
            if (ai.target_position.x <= 0)
            {
                randx = Random.Range(0, 8);
            }
            if (ai.target_position.y > 0)
            {
                randy = Random.Range(-4, 0);
            }
            if (ai.target_position.y <= 0)
            {
                randy = Random.Range(0, 4);
            }
            
            ai.target_position = new Vector3(randx, randy, 0);
        }
    }
}
