using UnityEngine;
using UnityEditor;
using JGM.Game;

namespace JGM.GameEditor
{
    [CustomEditor(typeof(GameSettings))]
    public class GameSettingsDrawer : Editor
    {
        private SerializedProperty m_rows;
        private SerializedProperty m_columns;
        private SerializedProperty m_minCellSize;
        private SerializedProperty m_cellSprites;

        private const int m_minColorsSize = 3;
        private const int m_maxColorsSize = 6;

        private void OnEnable()
        {
            m_rows = serializedObject.FindProperty("m_rows");
            m_columns = serializedObject.FindProperty("m_columns");
            m_minCellSize = serializedObject.FindProperty("m_minCellSize");
            m_cellSprites = serializedObject.FindProperty("m_cellSprites");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_rows, true);
            EditorGUILayout.PropertyField(m_columns, true);
            EditorGUILayout.PropertyField(m_minCellSize, true);
            EditorGUILayout.PropertyField(m_cellSprites, true);
            int arraySize = m_cellSprites.arraySize;
            arraySize = Mathf.Clamp(arraySize, m_minColorsSize, m_maxColorsSize);
            m_cellSprites.arraySize = arraySize;
            serializedObject.ApplyModifiedProperties();
        }
    }
}