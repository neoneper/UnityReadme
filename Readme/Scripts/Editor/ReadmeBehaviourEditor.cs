using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ReadmeSystem;

namespace ReadmeSystem.Editor
{
    [CustomEditor(typeof(ReadmeBehaviour))]
    public class ReadmeBehaviourEditor : UnityEditor.Editor
    {
        public bool changeReadme = false;
        protected override void OnHeaderGUI()
        {
            var behaviour = (ReadmeBehaviour)target;

            if (behaviour.readme == null)
            {
                base.OnInspectorGUI();
                return;
            }
        }

        public override void OnInspectorGUI()
        {
            var behaviour = (ReadmeBehaviour)target;

            if (behaviour.readme == null)
            {
                base.OnInspectorGUI();
                return;
            }

            if (changeReadme)
            {
                base.OnInspectorGUI();
                changeReadme = EditorGUILayout.Toggle("Change Readme File", changeReadme);
                return;
            }

            ReadmeEditor.DrawHeaderGUI(behaviour.readme);
            ReadmeEditor.DrawInspectorGUI(behaviour.readme);

            GUILayout.BeginVertical(EditorStyles.helpBox);
            changeReadme = EditorGUILayout.Toggle("Change Readme File", changeReadme);
            behaviour.showIconInHierarchy = EditorGUILayout.Toggle("Show Icon In Hierarchy", behaviour.showIconInHierarchy);
            GUILayout.EndVertical();
        }


        public static bool TryGetReadme(int instanceID, out Readme outReadme)
        {
            GameObject instance = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            outReadme = null;
            bool result = false;
            if (instance != null)
            {
                ReadmeBehaviour rb = instance.GetComponent<ReadmeBehaviour>();

                if (rb != null)
                {
                    if (rb.readme != null)
                    {
                        result = true;
                        outReadme = rb.readme;
                    }
                }
            }

            return result;
        }
        public static bool TryGetReadmeIcon(int instanceID, out GUIContent outIcon)
        {
            GameObject instance = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            outIcon = null;
            bool result = false;
            if (instance != null)
            {
                ReadmeBehaviour rb = instance.GetComponent<ReadmeBehaviour>();

                if (rb != null)
                {
                    if (rb.readme != null && rb.showIconInHierarchy)
                    {
                        if (rb.readme.icon != null)
                        {
                            result = true;
                            outIcon = new GUIContent(rb.readme.icon);
                        }
                    }
                }
            }

            return result;
        }
        public static bool TryGetReadmeBehaviour(int instanceID, out ReadmeBehaviour outReadmeBehaviour)
        {
            GameObject instance = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            outReadmeBehaviour = null;
            bool result = false;
            if (instance != null)
            {
                ReadmeBehaviour rb = instance.GetComponent<ReadmeBehaviour>();

                if (rb != null)
                {

                    result = true;
                    outReadmeBehaviour = rb;

                }
            }

            return result;
        }


    }
}