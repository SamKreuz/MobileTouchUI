using System;
using TMPro;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    public Guid Identifier;

    private TextMeshProUGUI textMeshProX;
    private TextMeshProUGUI textMeshProY;

    public void Setup()
    {
        Identifier = Guid.NewGuid();
        name = Identifier.ToString();

        var animator = GetComponent<Animator>();
        animator.Play("CircleScaleUp");

        GameObject gameObjectX = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        GameObject gameObjectY = gameObject.transform.GetChild(0).GetChild(1).gameObject;

        textMeshProX = gameObjectX.GetComponent<TextMeshProUGUI>();
        textMeshProY = gameObjectY.GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string textX, string textY)
    {
        if(textMeshProX != null && textMeshProY != null)
        {
            textMeshProX.text = textX;
            textMeshProY.text = textY;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void UpdatePosition(Vector2 position)
    {
        transform.position = position;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);

        string x = ((int)screenPosition.x).ToString();
        string y = ((int)screenPosition.y).ToString();

        SetText(x, y);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
