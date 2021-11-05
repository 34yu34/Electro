using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(Side))]
public class SidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var mask = property.FindPropertyRelative("_mask");

        mask.intValue = EditorGUI.MaskField(position, label, mask.intValue, mask.enumDisplayNames);

        EditorGUI.EndProperty();
    }

}