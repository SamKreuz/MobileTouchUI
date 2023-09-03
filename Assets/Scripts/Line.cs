using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Line
    {

        public GameObject GameObject { get; }
        public GameObject PointA { get; }
        public GameObject PointB { get; }

        private LineRenderer lineRenderer;

        public Line(GameObject gameObject, GameObject pointA, GameObject pointB) 
        { 
            GameObject = gameObject;
            PointA = pointA;
            PointB = pointB;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            Debug.Log($"LineRenderer for {gameObject.name} created");
        }

        public bool ContainsPoint(string point)
        {
            return PointA.name == point || PointB.name == point;
        }

        public void Remove()
        {
            UnityEngine.Object.Destroy(GameObject);
        }

        public void UpdatePosition() 
        {
            lineRenderer.SetPosition(0, PointA.transform.position);
            lineRenderer.SetPosition(1, PointB.transform.position);
        }
    }
}
