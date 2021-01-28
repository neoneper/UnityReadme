using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ReadmeSystem.Editor
{


    [CustomEditor(typeof(Readme))]
    [InitializeOnLoad]
    public class ReadmeEditor : UnityEditor.Editor
    {

        static string kShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";

        static float kSpace = 16f;

        static ReadmeEditor()
        {
            EditorApplication.delayCall += SelectReadmeAutomatically;
        }

        static void SelectReadmeAutomatically()
        {
            if (!SessionState.GetBool(kShowedReadmeSessionStateName, false))
            {
                var readme = SelectReadme();
                SessionState.SetBool(kShowedReadmeSessionStateName, true);

                if (readme && !readme.loadedLayout)
                {
                    LoadLayout();
                    readme.loadedLayout = true;
                }
            }
        }

        static void LoadLayout()
        {
            var assembly = typeof(EditorApplication).Assembly;
            var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
            var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);
            method.Invoke(null, new object[] { Path.Combine(Application.dataPath, "Readme/Layout.wlt"), false });
        }

        [MenuItem("Tutorial/Show Tutorial Instructions")]
        static Readme SelectReadme()
        {
            var ids = AssetDatabase.FindAssets("Readme t:Readme");
            Readme result = null;

            foreach (string guid in ids)
            {
                var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid));

                Readme readme = (Readme)readmeObject;
                if (readme.isRoot)
                {
                    Selection.objects = new UnityEngine.Object[] { readmeObject };
                    result = (Readme)readmeObject;
                    break;
                }
            }

            if (result == null)
            {
                Debug.Log("Couldn't find a readme");
            }


            return result;

        }

        protected override void OnHeaderGUI()
        {
            var readme = (Readme)target;

            if (readme.showInEditMode)
            {
                base.OnHeaderGUI();
                return;
            }

            DrawHeaderGUI(readme);
        }


        public override void OnInspectorGUI()
        {
            var readme = (Readme)target;

            if (readme.showInEditMode)
            {
                base.OnInspectorGUI();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                readme.showInEditMode = EditorGUILayout.Toggle("Show in Edit Mode", readme.showInEditMode);
                readme.isRoot = EditorGUILayout.Toggle("Set as Root", readme.isRoot);
                if (GUILayout.Button("Update Sections Label"))
                {
                    ResetSectionsLabel(readme);
                }
                EditorGUILayout.EndHorizontal();

                return;
            }
            else
            {

                DrawInspectorGUI(readme);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                readme.showInEditMode = EditorGUILayout.Toggle("Show in Edit Mode", readme.showInEditMode);
                EditorGUILayout.EndHorizontal();

            }
        }

        public static void DrawHeaderGUI(Readme readme)
        {
            var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);

            GUILayout.BeginHorizontal("In BigTitle");
            {

                GUILayout.Label(readme.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
                GUILayout.Label(readme.title, ReadmeEditorStyles.TitleStyle, GUILayout.ExpandHeight(true));

            }
            GUILayout.EndHorizontal();
        }

        public static void DrawInspectorGUI(Readme readme)
        {

            foreach (var section in readme.sections)
            {
                if (!string.IsNullOrEmpty(section.heading))
                {
                    section.name = "Header -" + section.heading;

                    GUILayout.Label(section.heading, ReadmeEditorStyles.HeadingStyle);

                    if (section.heading != "")
                    {
                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    }
                }
                if (!string.IsNullOrEmpty(section.text))
                {
                    if (string.IsNullOrEmpty(section.name))
                        section.name = "Text: " + section.text;

                    GUILayout.Label(section.text, ReadmeEditorStyles.BodyStyle);
                }
                if (!string.IsNullOrEmpty(section.linkText))
                {

                    if (string.IsNullOrEmpty(section.name))
                        section.name = "Link: " + section.text;

                    if (ReadmeEditorStyles.LinkLabel(new GUIContent(section.linkText)))
                    {
                        Application.OpenURL(section.url);
                    }
                }
                GUILayout.Space(kSpace);

                if (section.name.Length > 20)
                {
                    section.name = section.name.Remove(17);
                    section.name += "...";
                }

            }
        }

        private void ResetSectionsLabel(Readme readme)
        {

            foreach (var section in readme.sections)
            {
                section.name = "";

                if (!string.IsNullOrEmpty(section.heading))
                {
                    section.name = "Header -" + section.heading;

                }
                if (!string.IsNullOrEmpty(section.text))
                {
                    if (string.IsNullOrEmpty(section.name))
                        section.name = "Text: " + section.text;

                }
                if (!string.IsNullOrEmpty(section.linkText))
                {

                    if (string.IsNullOrEmpty(section.name))
                        section.name = "Link: " + section.text;


                }

                if (section.name.Length > 20)
                {
                    section.name = section.name.Remove(17);
                    section.name += "...";
                }

            }

        }


    }

}