using UnityEngine;
using UnityEditor;
using JGM.Game;

namespace JGM.GameEditor
{
    [CustomEditor(typeof(Cells))]
    public class CellsDrawer : Editor
    {
        private SerializedProperty colors;

        private void OnEnable()
        {
            colors = serializedObject.FindProperty("m_colors");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(colors, true);

            int arraySize = colors.arraySize;
            arraySize = Mathf.Clamp(arraySize, 3, 6);
            colors.arraySize = arraySize;

            serializedObject.ApplyModifiedProperties();
        }
    }
}