using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LinesManager", menuName = "Scriptable Objects/Lines Manager SO")]

public class LinesManagerSO : ScriptableObject
{
    [SerializeField] private List<Line> lines = new List<Line>();
    [SerializeField] private Material lineMaterial;
    [SerializeField] private float lineWidth = 0.25f;
    public void CreateLines(CircleManager originCircle, Dictionary<int, CircleManager> circles)
    {
        Vector3 pos1 = originCircle.GetPosition();

        foreach (var currentCircle in circles.Values)
        {
            Vector3 pos2 = currentCircle.GetPosition();

            GameObject lineObject = new GameObject(Guid.NewGuid().ToString());

            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, pos1);
                lineRenderer.SetPosition(1, pos2);
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
                lineRenderer.material = lineMaterial;
            }
            else
            {
                Debug.LogWarning("LineRenderer could not be created.");
            }

            lines.Add(new Line(lineObject, originCircle, currentCircle));
        }
    }

    public void UpdateLines(Guid identifier)
    {
        lines.Where(x => x.ContainsPoint(identifier)).ToList().ForEach(x => x.UpdatePosition());
    }

    public void RemoveLines(Guid identifier)
    {
        var linesToRemove = lines.Where(x => x.ContainsPoint(identifier)).ToList();
        linesToRemove.ForEach(x => x.Remove());
        lines.RemoveAll(x => x.ContainsPoint(identifier));
    }
}
