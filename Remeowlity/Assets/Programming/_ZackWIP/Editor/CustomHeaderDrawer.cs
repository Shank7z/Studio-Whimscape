using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CustomHeader))]
public class CustomHeaderDrawer : DecoratorDrawer
{
    public override float GetHeight()
    {
        CustomHeader header =
            (CustomHeader)attribute;

        return header.fontSize + 10f;
    }

    public override void OnGUI(Rect position)
    {
        CustomHeader header =
            (CustomHeader)attribute;

        GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
        style.fontSize = header.fontSize;

        position.y += 5f;

        EditorGUI.LabelField(
            position,
            header.header,
            style
        );
    }
}