using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ai : MonoBehaviour
{

    Slime slime_class;

    public Vector3 cur_position;
    public Vector3 target_position;
    public float speed;
    public SpriteRenderer sr;





    // Start is called before the first frame update
    void Start()
    {
        slime_class = gameObject.GetComponent<Slime>();
        cur_position = transform.position;
        int randx = Random.Range(-7, 8);
        int randy = Random.Range(-4, 4);
        target_position = new Vector3(randx, randy, 0);
    }

    // Update is called once per frame
    void Update()
    {
        cur_position = transform.position;
        flip_sprite();
        wonder();
    }

    void wonder()
    {
        if(Vector3.Distance(cur_position, target_position) < 0.001f)
        {
            int randx = Random.Range(-7, 8);
            int randy = Random.Range(-4, 4);
            target_position = new Vector3(randx, randy, 0);
        }
        else
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target_position, step);
        }

    }

    void flip_sprite()
    {
        //SpriteRenderer sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (cur_position.x > target_position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

}
