using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;
public class headrotation : MonoBehaviour
{
    List<string> lines;
    int counter = 0;
    public GameObject HEAD0;
    public GameObject body;

    public Quaternion localbody;
    // Start is called before the first frame update
    void Start()
    {
        lines = System.IO.File.ReadLines("Assets/datatxt/headdata.txt").ToList();
        localbody = body.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        string[] points = lines[counter].Split(',');
        HEAD0.transform.rotation = localbody * Quaternion.Inverse(Quaternion.Euler(float.Parse(points[0]), float.Parse(points[1]), float.Parse(points[2])));


        counter += 1;
        if (counter == lines.Count) { counter = 0; }
    }
}
