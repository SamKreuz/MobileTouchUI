using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircleUIDataManager : MonoBehaviour
{
    [SerializeField] private Vector2 position;
    [SerializeField] private int textPositionX;
    [SerializeField] private int textPositionY;

    public Vector2 Position { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjectX = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        GameObject gameObjectY = gameObject.transform.GetChild(0).GetChild(1).gameObject;

        var tmp = gameObjectX.GetComponent<TextMeshPro>();
        tmp.text = "1234";
    }

    public void SetTextCoordinates(string x, string y)
    {
        
    }
}
