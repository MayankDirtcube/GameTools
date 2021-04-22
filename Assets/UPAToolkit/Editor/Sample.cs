using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Sample : EditorWindow
{
    [MenuItem("Window/Sample")]
    public static void show()
    {
        Sample window = new Sample();
        window.Show();
    }

    private void OnGUI()
    {
        for(int i=0; i < 5; i++)
        {
            Rect position = new Rect(0, i*50+10, 50, 50);
            EditorGUI.DrawRect(position, Color.red);
            if (GUI.Button(position, i.ToString(), new GUIStyle()))
            {
                Debug.Log("Red Color Selected");
            }
        }
    }
}
