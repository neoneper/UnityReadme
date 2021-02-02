using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ReadmeSystem.Editor
{


    [CustomEditor(typeof(Readme))]
    [InitializeOnLoad]
    public class ReadmeEditor : UnityEditor.Editor
    {

        Readme readme { get { return (Readme)target; } }
        string nextReadmeTitle
        {
            get
            {
                if (readme.nextReadme == null)
                    return "Next";


                return readme.nextReadme.title == "" ? "Next" : readme.nextReadme.title;
            }
        }

        string prevReadmeTitle
        {
            get
            {
                if (readme.prevReadme == null)
                    return "Prev";


                return readme.prevReadme.title == "" ? "Prev" : readme.prevReadme.title;
            }
        }



        static bool showInEditMode = false;

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
                if (readme)
                {
                    SessionState.SetBool(kShowedReadmeSessionStateName, true);
                }
            }
        }


        [MenuItem("Tutorial/Show Tutorial Instructions")]
        static Readme SelectReadme()
        {
            showInEditMode = false;

            Readme result = GetReadmeRoot();


            if (result != null)
            {
                Selection.objects = new UnityEngine.Object[] { result };

            }
            else
            {
                Debug.LogWarning("Couldn't find a readme");
            }

            return result;

        }


        protected override void OnHeaderGUI()
        {
            var readme = (Readme)target;

            if (showInEditMode)
            {
                base.OnHeaderGUI();
                return;
            }

            DrawHeaderGUI(readme);
        }

        public override void OnInspectorGUI()
        {
            var readme = (Readme)target;

            if (showInEditMode)
            {
                base.OnInspectorGUI();

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                showInEditMode = EditorGUILayout.Toggle("Show in Edit Mode", showInEditMode);

                EditorGUI.BeginChangeCheck();
                readme.isRoot = EditorGUILayout.Toggle("Set as Root", readme.isRoot);
                if (EditorGUI.EndChangeCheck())
                {
                    if (readme.isRoot)
                    {
                        Debug.Log("ResetAll");
                        //Ensures that there is only one readme as root.
                        ResetAllRootReadme();
                        readme.isRoot = true;
                    }
                }

                if (GUILayout.Button("Update Sections Label"))
                {
                    ResetSectionsLabel(readme);
                }

                EditorGUILayout.EndVertical();

                //ImportOptions
                return;
            }


            DrawInspectorGUI(readme);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            showInEditMode = EditorGUILayout.Toggle("Show in Edit Mode", showInEditMode);
            EditorGUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(EditorStyles.toolbar);


            GUI.enabled = readme.prevReadme != null;
            if (GUILayout.Button(prevReadmeTitle, EditorStyles.toolbarButton))
            {
                Selection.objects = new UnityEngine.Object[] { readme.prevReadme };
            }

            GUI.enabled = readme.nextReadme != null;
            if (GUILayout.Button(nextReadmeTitle, EditorStyles.toolbarButton))
            {
                Selection.objects = new UnityEngine.Object[] { readme.nextReadme };

            }

            GUI.enabled = true;

            GUILayout.EndHorizontal();
        }

        public static void DrawHeaderGUI(Readme readme)
        {
            if (readme == null)
                return;

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
            if (readme == null)
                return;
            if (readme.sections == null)
                return;

            foreach (var section in readme.sections)
            {
                if (!string.IsNullOrEmpty(section.heading))
                {
                    section.name = "Header -" + section.heading;

                    GUILayout.Label(section.heading, ReadmeEditorStyles.HeadingStyle);

                    //Add Horizontal Bar
                    if (section.heading != "") { EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); }
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
        public static void DrawImport(Readme readme)
        {

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

        static List<Readme> GetAllRootReadme()
        {
            var ids = AssetDatabase.FindAssets("Readme t:Readme");
            List<Readme> results = new List<Readme>();

            foreach (string guid in ids)
            {
                var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid));

                Readme readme = (Readme)readmeObject;
                if (readme.isRoot)
                {
                    results.Add(readme);

                }
            }



            return results; ;
        }
        static Readme GetReadmeRoot()
        {
            Readme result = GetAllRootReadme().FirstOrDefault();
            //Ensures that there is only one readme as root.
            if (result != null)
            {
                ResetAllRootReadme();
                result.isRoot = true;
            }
            return result;

        }
        static void ResetAllRootReadme()
        {
            foreach (Readme readme in GetAllRootReadme())
            {
                readme.isRoot = false;
            }
        }
    }





}