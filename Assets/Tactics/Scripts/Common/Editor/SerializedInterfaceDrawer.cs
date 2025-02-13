using System;
using System.Linq;
using Tactics.Common.Implementation.Services;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tactics.Common.Editor
{
    [CustomPropertyDrawer(typeof(SerializedInterface<>))]
    public class SerializedInterfaceDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("assignedObject"), label);
        }

        private void ValidateAssignment(SerializedProperty property)
        {
            var assignedObjectProperty = property.FindPropertyRelative("assignedObject");
            var assignedObject = assignedObjectProperty.objectReferenceValue;

            if (assignedObject != null)
            {
                var interfaceType = GetInterfaceType();

                if (assignedObject is MonoBehaviour monoBehaviour && !IsInterfaceImplemented(monoBehaviour, interfaceType))
                {
                    ResetAssignment(property, assignedObject, interfaceType, assignedObjectProperty);
                }
                else if (assignedObject is GameObject gameObject)
                {
                    if (!TryGetComponent(gameObject, interfaceType, out var result))
                    {
                        ResetAssignment(property, assignedObject, interfaceType, assignedObjectProperty);
                    }

                    assignedObjectProperty.objectReferenceValue = result;
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        private void ResetAssignment(SerializedProperty property,
            Object assignedObject,
            Type interfaceType,
            SerializedProperty assignedObjectProperty)
        {
            Debug.LogWarning($"Assigned object '{assignedObject.name}' does not implement the required interface '{interfaceType.Name}'.");
            assignedObjectProperty.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
        }

        private Type GetInterfaceType()
        {
            var genericArguments = fieldInfo.FieldType.GetGenericArguments();
            return genericArguments[0];
        }

        private bool IsInterfaceImplemented(MonoBehaviour monoBehaviour, Type interfaceType)
        {
            return monoBehaviour.GetType().GetInterfaces().Contains(interfaceType);
        }

        private bool TryGetComponent(GameObject gameObject, Type interfaceType, out Object result)
        {
            result = gameObject.GetComponent(interfaceType);
            return result != null;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.BeginChangeCheck();

            var assignedObjectProperty = property.FindPropertyRelative("assignedObject");

            if (EditorGUIUtility.wideMode)
            {
                position = EditorGUI.PrefixLabel(position, label);

                assignedObjectProperty.objectReferenceValue = EditorGUI.ObjectField(position, GUIContent.none,
                    assignedObjectProperty.objectReferenceValue, typeof(Object), true);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(label);

                assignedObjectProperty.objectReferenceValue = EditorGUILayout.ObjectField(assignedObjectProperty.objectReferenceValue,
                    typeof(Object), true);

                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                ValidateAssignment(property);
            }

            EditorGUI.EndProperty();
        }
    }
}
