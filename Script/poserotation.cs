using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;

public class poserotation : MonoBehaviour
{
    Vector3 TriangleNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = a - b;
        Vector3 d2 = a - c;

        Vector3 dd = Vector3.Cross(d1, d2);
        dd.Normalize();

        return dd;
    }

    Quaternion Left(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = TriangleNormal(a, b, c);
        Quaternion d2 = localbody * Quaternion.LookRotation(-d1, a - b);

        return d2;
    }

    Quaternion Right(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = TriangleNormal(a, b, c);
        Quaternion d2 = localbody * Quaternion.LookRotation(d1, a - b);

        return d2;
    }
    List<string> lines;
    int counter = 0;
    public GameObject LEFT_SHOULDER11;
    public GameObject RIGHT_SHOULDER12;
    public GameObject LEFT_ELBOW13;
    public GameObject RIGHT_ELBOW14;
    public GameObject body;

    public Quaternion localbody;

    // Start is called before the first frame update
    void Start()
    {
        lines = System.IO.File.ReadLines("Assets/datatxt/posedata.txt").ToList();
        localbody = body.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        string[] points = lines[counter].Split(',');
        Vector3 Pos3D(int i)
        {
            Vector3 d3d = new Vector3(float.Parse(points[0 + 3 * i]) / 100, float.Parse(points[3 * i + 1]) / 100, float.Parse(points[3 * i + 2]) / 100);
            return d3d;
        }


        //左肩膀/大臂/小臂旋转设定
        LEFT_SHOULDER11.transform.rotation = Left(Pos3D(11), Pos3D(13), Pos3D(15)) ;
        LEFT_ELBOW13.transform.rotation = Left(Pos3D(13), Pos3D(15), Pos3D(11));

        //右肩膀/大臂/小臂旋转设定
        RIGHT_SHOULDER12.transform.rotation = Right(Pos3D(12), Pos3D(14), Pos3D(16));
        RIGHT_ELBOW14.transform.rotation = Right(Pos3D(14), Pos3D(16), Pos3D(12));

        counter += 1;
        if (counter == lines.Count) { counter = 0; }
    }
}
