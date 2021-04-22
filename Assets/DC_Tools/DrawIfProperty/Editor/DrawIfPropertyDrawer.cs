using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DC.Tools
{
    [CustomPropertyDrawer(typeof(DrawIfAttribute))]
    public class DrawIfPropertyDrawer : PropertyDrawer
    {
        //Reference to the attribute on the property.
        DrawIfAttribute drawIf;

        //Field that is being compared.
        SerializedProperty comparedField;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!ShowMe(property) && drawIf.disablingType == DisablingType.DontDraw)
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
            else
            {
                return EditorGUI.GetPropertyHeight(property, label, true);
            }
        }

        private bool ShowMe(SerializedProperty property)
        {
            drawIf = attribute as DrawIfAttribute;
            // Replace propertyname to the value from the parameter
            string path; //= property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;

            if (property.propertyPath.Contains("."))
            {
                if (property.propertyPath.Contains("Array"))
                {
                    //path = drawIf.comparedPropertyName;
                    Debug.LogError("Arrays and Lists are unsupported at this point in time!");
                    return true;
                }
                else
                {
                    path = System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName);
                }
            }
            else
            {
                path = drawIf.comparedPropertyName;
            }

            //Debug.Log("Property Name: " + property.displayName + " Property Type: " + property.propertyType + "  Path: " + path);

            comparedField = property.serializedObject.FindProperty(path);

            if (comparedField == null)
            {
                Debug.LogError("Cannot find property with name: " + path);
                return true;
            }

            //Get the value & compare based on types
            switch (comparedField.type)
            {
                //Possible extend cases to support your own type
                case "bool":
                    return comparedField.boolValue.Equals(drawIf.comparedValue);
                case "Enum":
                    return comparedField.enumValueIndex.Equals((int)drawIf.comparedValue);
                default:
                    Debug.LogError("Error: " + comparedField.type + " is not supported of " + path);
                    return true;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //If condition is met, draw the field.
            if (ShowMe(property))
            {
                EditorGUI.PropertyField(position, property, true);
            }
            //Check if Disabling Type is 'Read Only'. If Yes, draw it Disabled
            else if (drawIf.disablingType == DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, true);
                GUI.enabled = true;
            }
        }
    }
}