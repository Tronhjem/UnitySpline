using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowOnSpline))]
public class SplineFollowInspector : Editor
{
    public List<Transform> transforms = new List<Transform>();

    public FollowOnSpline splineTool;

    private void OnEnable()
    {
        splineTool = target as FollowOnSpline;

        for (int i = 0; i < splineTool.Points.Length; i++)
        {
            transforms.Add(splineTool.Points[i]);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        using (new EditorGUILayout.VerticalScope("box"))
        {
            for (int i = 0; i < transforms.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                transforms[i].gameObject.name = i.ToString();
                transforms[i] = EditorGUILayout.ObjectField("point: ", transforms[i], typeof(Transform), false) as Transform;
                if (GUILayout.Button("-"))
                {
                    RemovePoint(transforms[i]);
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("+"))
            {
                AddNewPoint();
            }
        }
        splineTool.Points = transforms.ToArray();

        if (GUILayout.Button("Clear"))
        {
            transforms.Clear();
        }
    }

    private void AddNewPoint()
    {
        string name = (transforms.Count + 1).ToString();
        GameObject newGo = new GameObject(name);
        newGo.transform.SetParent(splineTool.gameObject.transform);

        if (transforms.Count > 0)
            newGo.transform.position = transforms[transforms.Count - 1].position;

        transforms.Add(newGo.transform);
    }

    private void RemovePoint(Transform t)
    {
        for (int i = 0; i < transforms.Count; i++)
        {
            if (t == transforms[i])
            {
                
                Transform toRemove = splineTool.transform.Find(transforms[i].name);
                DestroyImmediate(toRemove.gameObject);
                
                transforms.Remove(transforms[i]);
                break;
            }

        }
    }

}
