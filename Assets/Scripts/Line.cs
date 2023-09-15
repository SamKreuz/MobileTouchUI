using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Line
    {
        public GameObject GameObject { get; }
        public CircleManager PointA { get; }
        public CircleManager PointB { get; }

        private LineRenderer lineRenderer;

        public Line(GameObject gameObject, CircleManager pointA, CircleManager pointB) 
        { 
            GameObject = gameObject;
            PointA = pointA;
            PointB = pointB;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            Debug.Log($"LineRenderer for {gameObject.name} created");
        }

        public bool ContainsPoint(Guid point)
        {
            return PointA.Identifier == point || PointB.Identifier == point;
        }

        public void Remove()
        {
            UnityEngine.Object.Destroy(GameObject);
        }

        public void UpdatePosition() 
        {
            lineRenderer.SetPosition(0, PointA.GetPosition());
            lineRenderer.SetPosition(1, PointB.GetPosition());
        }
    }
}
