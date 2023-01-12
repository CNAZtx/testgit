using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;

public class lefthandrotation : MonoBehaviour
{
    //设定方向坐标的方程
    Vector3 TriangleNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = a - b;
        Vector3 d2 = a - c;

        Vector3 dd = Vector3.Cross(d1, d2);
        dd.Normalize();

        return dd;
    }

    Quaternion Forward(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = TriangleNormal(a, b, c);
        Quaternion d2 = localbody * Quaternion.LookRotation(-d1, a - b);

        return d2;
    }

    Quaternion BasicForward(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = TriangleNormal(a, b, c);
        Vector3 d2 = TriangleNormal(d1 + b, b, c);
        Quaternion d3 = localbody * Quaternion.LookRotation(d2, b - c);

        return d3;
    }

    List<string> lines;
    int counter = 0;

    //设定关节与初始身体朝向
    public GameObject WRIST0;
    public GameObject WRISTTHUMB_CMC1;
    public GameObject THUMB_MCP2;
    public GameObject THUMB_IP3;
    public GameObject THUMB_TIP4;
    public GameObject INDEX_FINGER_MCP5;
    public GameObject INDEX_FINGER_PIP6;
    public GameObject INDEX_FINGER_DIP7;
    public GameObject INDEX_FINGER_TIP8;
    public GameObject MIDDLE_FINGER_MCP9;
    public GameObject MIDDLE_FINGER_PIP10;
    public GameObject MIDDLE_FINGER_DIP11;
    public GameObject MIDDLE_FINGER_TIP12;
    public GameObject RING_FINGER_MCP13;
    public GameObject RING_FINGER_PIP14;
    public GameObject RING_FINGER_DIP15;
    public GameObject RING_FINGER_TIP16;
    public GameObject PINKY_MCP17;
    public GameObject PINKY_PIP18;
    public GameObject PINKY_DIP19;
    public GameObject PINKY_TIP20;
    public GameObject body;

    public Quaternion localbody;
    // Start is called before the first frame update
    void Start()
    {
        lines = System.IO.File.ReadLines("Assets/datatxt/leftdata.txt").ToList();

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
        //手腕旋转
        WRIST0.transform.rotation = Forward(Pos3D(0), Pos3D(9), -Pos3D(17));

        //大拇指设置旋转
        WRISTTHUMB_CMC1.transform.rotation = Forward(Pos3D(1), Pos3D(2), -Pos3D(5));
        THUMB_MCP2.transform.rotation = BasicForward(Pos3D(1), Pos3D(2), Pos3D(3));
        THUMB_IP3.transform.rotation = BasicForward(Pos3D(2), Pos3D(3), Pos3D(4));

        //食指旋转
        INDEX_FINGER_MCP5.transform.rotation = Forward(Pos3D(5), Pos3D(6), -Pos3D(9));
        INDEX_FINGER_PIP6.transform.rotation = BasicForward(Pos3D(5), Pos3D(6), Pos3D(7));
        INDEX_FINGER_DIP7.transform.rotation = BasicForward(Pos3D(6), Pos3D(7), Pos3D(8));


        //中指旋转
        MIDDLE_FINGER_MCP9.transform.rotation = Forward(Pos3D(9), Pos3D(10), -Pos3D(13));
        MIDDLE_FINGER_PIP10.transform.rotation = BasicForward(Pos3D(9), Pos3D(10), Pos3D(11));
        MIDDLE_FINGER_DIP11.transform.rotation = BasicForward(Pos3D(10), Pos3D(11), Pos3D(12));

        //无名指旋转
        RING_FINGER_MCP13.transform.rotation = Forward(Pos3D(13), Pos3D(14), -Pos3D(17));
        RING_FINGER_PIP14.transform.rotation = BasicForward(Pos3D(13), Pos3D(14), Pos3D(15));
        RING_FINGER_DIP15.transform.rotation = BasicForward(Pos3D(14), Pos3D(15), Pos3D(16));

        //小拇指旋转
        PINKY_MCP17.transform.rotation = Forward(Pos3D(17), Pos3D(18), Pos3D(13));
        PINKY_PIP18.transform.rotation = BasicForward(Pos3D(17), Pos3D(18), Pos3D(19));
        PINKY_DIP19.transform.rotation = BasicForward(Pos3D(18), Pos3D(19), Pos3D(20));

        counter += 1;
        if (counter == lines.Count) { counter = 0; }
    }
}
