using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{

    Triangulator tr;
    Game_Manager gm;
    //Mouse_Collider mc;

    public GameObject drawPrefab;
    public List<Vector2> verts = new List<Vector2>();
    public Vector2[] line_verts_adjust;
    //private List<Vector2> m_points = new List<Vector2>();
    GameObject theTrail;
    public GameObject selction_poly;
    public bool movedfromstart;
    public bool Line_broken;
    Plane planeobj;
    public Vector2 startPos;
    public Vector2 curPos;
    public Vector2 start_diff;
    public float selection_resolution;
    public GameObject mc;
    public GameObject Line_break_ps;
    private CircleCollider2D mc_col;

    


    // Start is called before the first frame update
    void Start()
    {
        tr = FindObjectOfType<Triangulator>();
        gm = FindObjectOfType <Game_Manager>();
        mc_col = mc.GetComponent<CircleCollider2D>();
        planeobj = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.game_over != true)
        {
            //Loop Drawing
            if (Input.GetMouseButtonDown(0))
            {
                mc_col.enabled = false;


                theTrail = (GameObject)Instantiate(drawPrefab, this.transform.position, Quaternion.identity);
                theTrail.SetActive(true);

                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float _dis;
                if (planeobj.Raycast(mouseRay, out _dis))
                {

                    startPos = mouseRay.GetPoint(_dis);
                    //Debug.Log("Start point: " + startPos);
                    movedfromstart = false;
                    //verts.Add(startPos);
                }
            }
            else if (Input.GetMouseButton(0) && Line_broken == false)
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float _dis;
                Vector3 diff_pos;
                if (planeobj.Raycast(mouseRay, out _dis))
                {
                    theTrail.transform.position = mouseRay.GetPoint(_dis);

                    Vector2 trail_pos_V3toV2;
                    trail_pos_V3toV2 = new Vector2(theTrail.transform.position.x, theTrail.transform.position.y);


                    diff_pos = trail_pos_V3toV2 - curPos;


                    //Check if mouse has moved far enough to generate new point
                    if ((trail_pos_V3toV2 != curPos) && (diff_pos.magnitude > selection_resolution))
                    {

                        curPos = theTrail.transform.position;
                        //Debug.Log("Next point: " + curPos);
                        verts.Add(curPos);
                        start_diff = startPos - curPos;
                        if (diff_pos.magnitude > selection_resolution && start_diff.magnitude > (0 + selection_resolution))
                        {
                            movedfromstart = true;
                        }

                        //Line collider
                        EdgeCollider2D line_col = theTrail.GetComponent<EdgeCollider2D>();
                        line_col.enabled = true;
                        //adjust verts so they follow line for edge collider (relativity issue)
                        line_verts_adjust = new Vector2[verts.Count];
                        line_verts_adjust = verts.ToArray();
                        Vector2 adjustment_factor;
                        adjustment_factor = line_verts_adjust[line_verts_adjust.Length - 1];
                        for (int i = 0; i < line_verts_adjust.Length; i++)
                        {
                            //removes leading point fromm collider
                            if (i == line_verts_adjust.Length - 1 && i > 1)
                            {
                                //Debug.Log(i);
                                line_verts_adjust[i].x = line_verts_adjust[i - 1].x;
                                line_verts_adjust[i].y = line_verts_adjust[i - 1].y;
                            }
                            //corrects the other points
                            else
                            {
                                line_verts_adjust[i].x = line_verts_adjust[i].x - adjustment_factor.x;
                                line_verts_adjust[i].y = line_verts_adjust[i].y - adjustment_factor.y;
                            }


                        }

                        line_col.points = new Vector2[0];
                        line_col.points = line_verts_adjust;



                        //Finish loop
                        if (start_diff.magnitude < selection_resolution && movedfromstart == true)
                        {
                            //Debug.Log("Complete Loop");

                            mc_col.enabled = false;

                            // Use the triangulator to get indices for creating triangles
                            tr.ini_list(verts.ToArray());
                            int[] indices = tr.Triangulate();

                            // Create the Vector3 vertices
                            Vector3[] vertices = new Vector3[verts.ToArray().Length];
                            for (int i = 0; i < vertices.Length; i++)
                            {
                                vertices[i] = new Vector3(verts.ToArray()[i].x, verts.ToArray()[i].y, 0);
                            }

                            // Create the mesh
                            Mesh msh = new Mesh();
                            msh.vertices = vertices;
                            msh.triangles = indices;
                            msh.RecalculateNormals();
                            msh.RecalculateBounds();

                            // Set up game object with mesh and collider;
                            GameObject poly_inst = Instantiate(selction_poly);
                            //poly_inst.AddComponent(typeof(MeshRenderer));
                            MeshFilter filter = poly_inst.AddComponent(typeof(MeshFilter)) as MeshFilter;
                            filter.mesh = msh;

                            PolygonCollider2D poly_col = poly_inst.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                            poly_col.points = verts.ToArray();
                            poly_col.isTrigger = true;

                            line_col.enabled = false;
                            StartCoroutine("complete_loop");

                            gm.lines = gm.lines - 1;
                            verts.Clear();
                            movedfromstart = false;
                            Line_broken = false;

                            //mc_col.enabled = true;
                        }
                    }


                    if (verts.Count > 3)
                    {
                        mc_col.enabled = true;
                    }

                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                theTrail.SetActive(false);
                verts.Clear();
                Line_broken = false;
            }
        }
    }

    public void Line_Break()
    {
        if (theTrail != null)
        {
            theTrail.SetActive(false);
            verts.Clear();
            Line_broken = true;
            gm.lines = gm.lines - 1;
            gm.win_fail_state();
        }
    }

    IEnumerator complete_loop()
    {
        //Line collider
        EdgeCollider2D line_col = theTrail.GetComponent<EdgeCollider2D>();
        line_col.enabled = false;
        yield return new WaitForSeconds(0.25f);
        theTrail.SetActive(false);
    }


}
