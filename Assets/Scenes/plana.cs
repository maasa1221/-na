using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class plana : MonoBehaviour {
    AudioSource m_MyAudioSource;

    GameObject[] newLine1 = new GameObject[100];
    LineRenderer[] lRend = new LineRenderer[100];
    GameObject[] newLine1_d = new GameObject[100];
    LineRenderer[] lRend_d = new LineRenderer[100];
    float m_MySliderValue;
    int counter;
    int count = 0;
    int ls = 0;
    int flag = 0;
    private Quaternion gyro;
    private Vector3 acceleration;
    Color c1 = Color.red;
    Color c2 = Color.blue;
    List<Vector2> my2DPoint = new List<Vector2> ();
    List<Vector2> my2DPoint_d = new List<Vector2> ();

    void DrawLine1 (List<Vector2> my2DVec, int startPos) {
        count = startPos;

        List<Vector3> myPoint = new List<Vector3> ();
        for (int idx = 0; idx < 2; idx++) {
            myPoint.Add (new Vector3 (my2DVec[startPos + idx].x, my2DVec[startPos + idx].y, 0.0f));
        }

        newLine1[startPos] = new GameObject ("Line" + startPos);
        lRend[count] = newLine1[startPos].AddComponent<LineRenderer> ();

        lRend[count].SetVertexCount (2);
        lRend[count].SetWidth (0.1f, 0.1f);
        Vector3 startVec = myPoint[0];
        Vector3 endVec = myPoint[1];
        lRend[count].SetPosition (0, startVec);
        lRend[count].SetPosition (1, endVec);
        lRend[count].material = new Material (Shader.Find ("Sprites/Default"));
        lRend[count].SetColors (c1, c1);
    }
    void DrawLine (List<Vector2> my2DVec, int startPos) {
        count = startPos;

        List<Vector3> myPoint = new List<Vector3> ();
        for (int idx = 0; idx < 2; idx++) {
            myPoint.Add (new Vector3 (my2DVec[startPos + idx].x, my2DVec[startPos + idx].y + 4, 0.0f));
        }
        lRend[count] = newLine1[startPos].GetComponent<LineRenderer> ();
        lRend[count].SetVertexCount (2);
        lRend[count].SetWidth (0.1f, 0.1f);
        Vector3 startVec = myPoint[0];
        Vector3 endVec = myPoint[1];
        lRend[count].SetPosition (0, startVec);
        lRend[count].SetPosition (1, endVec);
        lRend[count].material = new Material (Shader.Find ("Sprites/Default"));
        lRend[count].SetColors (c1, c1);
    }
    void DrawLine1_d (List<Vector2> my2DVec, int startPos) {
        count = startPos + 100;

        List<Vector3> myPoint = new List<Vector3> ();
        for (int idx = 0; idx < 2; idx++) {
            myPoint.Add (new Vector3 (my2DVec[startPos + idx].x, my2DVec[startPos + idx].y, 0.0f));
        }

        newLine1_d[startPos] = new GameObject ("Line" + count);
        lRend_d[startPos] = newLine1_d[startPos].AddComponent<LineRenderer> ();

        lRend_d[startPos].SetVertexCount (2);
        lRend_d[startPos].SetWidth (0.1f, 0.1f);
        Vector3 startVec = myPoint[0];
        Vector3 endVec = myPoint[1];
        lRend_d[startPos].SetPosition (0, startVec);
        lRend_d[startPos].SetPosition (1, endVec);
        lRend_d[startPos].material = new Material (Shader.Find ("Sprites/Default"));
        lRend_d[startPos].SetColors (c2, c2);
    }
    void DrawLine_d (List<Vector2> my2DVec, int startPos) {
        count = startPos + 100;
        ls += 1;
        List<Vector3> myPoint = new List<Vector3> ();
        for (int idx = 0; idx < 2; idx++) {
            myPoint.Add (new Vector3 (my2DVec[startPos + idx].x, my2DVec[startPos + idx].y, 0.0f));
        }
        lRend_d[startPos] = newLine1_d[startPos].GetComponent<LineRenderer> ();
        lRend_d[startPos].SetVertexCount (2);
        lRend_d[startPos].SetWidth (0.1f, 0.1f);
        Vector3 startVec = myPoint[0];
        Vector3 endVec = myPoint[1];
        lRend_d[startPos].SetPosition (0, startVec);
        lRend_d[startPos].SetPosition (1, endVec);
        lRend_d[startPos].material = new Material (Shader.Find ("Sprites/Default"));
        lRend_d[startPos].SetColors (c2, c2);
    }
    void Start () {

        //Initiate the Slider value to half way
        m_MySliderValue = 0.5f;
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource> ();
        //Play the AudioClip attached to the AudioSource on startup
        m_MyAudioSource.Play ();
        Input.gyro.enabled = true;
        for (int idx = 0; idx < 100; idx++) {
            my2DPoint.Add (new Vector2 (-10 + 0.2f * idx, 0));
        }
        for (int idx = 0; idx < my2DPoint.Count - 1; idx++) {
            DrawLine1 (my2DPoint, /* startPos=*/ idx);
        }
        for (int idx = 0; idx < 100; idx++) {
            my2DPoint_d.Add (new Vector2 (-10 + 0.2f * idx, -1));
        }
        for (int idx = 0; idx < my2DPoint_d.Count - 1; idx++) {
            DrawLine1_d (my2DPoint_d, /* startPos=*/ idx);
        }
        counter = 0;
    }

    // Update is called once per frame
    void Update () {

        counter += 1;

        for (int idx = 0; idx < 100; idx++) {
            if (idx == 99) {
                my2DPoint[idx] = new Vector2 (-10 + 0.2f * idx, Input.gyro.attitude.x);
            } else {
                my2DPoint[idx] = new Vector2 (-10 + 0.2f * idx, my2DPoint[idx + 1].y);

            }

        }
        for (int idx = 0; idx < my2DPoint.Count - 1; idx++) {
            DrawLine (my2DPoint, /* startPos=*/ idx);
        }

        for (int idx = 0; idx < 100; idx++) {
            if (idx == 99) {
                my2DPoint_d[idx] = new Vector2 (-10 + 0.2f * idx, Input.acceleration.x - 1);
            } else {
                my2DPoint_d[idx] = new Vector2 (-10 + 0.2f * idx, my2DPoint_d[idx + 1].y);

            }

        }
        for (int idx = 0; idx < my2DPoint_d.Count - 1; idx++) {
            DrawLine_d (my2DPoint_d, /* startPos=*/ idx);
        }
        Debug.Log (Input.acceleration.x);
        if (Input.acceleration.x > 0.8) {
            flag += 1;
        }
        if (counter % 10000 == 0 && counter >= 10000) {
            if (flag == (counter / 10000)) {
                m_MyAudioSource.volume = 0;
            } else { m_MyAudioSource.volume = Input.gyro.attitude.x * Input.gyro.attitude.x * Input.gyro.attitude.x; }
        } else { m_MyAudioSource.volume = Input.gyro.attitude.x * Input.gyro.attitude.x * Input.gyro.attitude.x; }

    }
}