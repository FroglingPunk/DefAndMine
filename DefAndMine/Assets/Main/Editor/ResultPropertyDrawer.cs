using UnityEditor;
using UnityEngine;

// отображаем значение morphOutcome если взаимодействие приводит в преображению элементаля
[CustomPropertyDrawer(typeof(Result))]
public class ResultPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty propKind = property.FindPropertyRelative("kind");
        EditorGUILayout.PropertyField(propKind);

        EElementsInteraction interaction = (EElementsInteraction)propKind.intValue;
        switch (interaction)
        {
            case EElementsInteraction.Morph:
                SerializedProperty propMorphOutcome = property.FindPropertyRelative("morphOutcome");
                EditorGUILayout.PropertyField(propMorphOutcome);
                break;

            default:
                break;
        }

        SerializedProperty propMultiply = property.FindPropertyRelative("multiply");
        EditorGUILayout.PropertyField(propMultiply);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
}