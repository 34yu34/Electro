using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(Attribute))]
public class AttributeDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var baseValue = property.FindPropertyRelative("_baseValue");

        var labelRect = new Rect(position.x, position.y, position.width / 2, position.height);
        var propertyRect = new Rect(position.x + position.width / 2, position.y, position.width /2, position.height);

        EditorGUI.LabelField(labelRect, property.displayName);
        baseValue.floatValue = EditorGUI.FloatField(propertyRect, baseValue.floatValue);

        property.FindPropertyRelative("_calculatedValue").floatValue = baseValue.floatValue;

        EditorGUI.EndProperty();
    }


}
