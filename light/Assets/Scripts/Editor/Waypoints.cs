using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Ennemy_Flying))]
public class Waypoints : Editor
{
    Ennemy_Flying controller;

    private void OnEnable()
    {
        controller = target as Ennemy_Flying;
    }

    private void OnSceneGUI()
    {        
        Vector3[] array = controller.waypoints.ToArray();
        Handles.DrawAAPolyLine(array);
        if (!Application.isPlaying)
        {
            array[0] = controller.transform.position;
        }
        for (int i = 1; i < array.Length; i++)
        {
            array[i] = Handles.PositionHandle(array[i], Quaternion.identity);
            Handles.Label(array[i], i.ToString());
        }
        controller.waypoints = array.ToList();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add waypoint")){
            Vector3 position;
            if (controller.waypoints.Count > 0)
            {
                position = controller.waypoints.Last() + new Vector3(0, -1, 0);
            }
            else {
                position = controller.transform.position + new Vector3(0, -1, 0);
            }
            controller.waypoints.Add(position);
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Remove waypoint"))
        {
            if (controller.waypoints.Count <= 1)
                return;
            controller.waypoints.Remove(controller.waypoints.Last());
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Reset")) {
            controller.waypoints.Clear();
            controller.waypoints.Add(controller.transform.position);
            SceneView.RepaintAll();
        }
    }
}


