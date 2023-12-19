using System.Linq;
using Game.Core.Enums;
using Game.Core.LevelBase;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelEditor : UnityEditor.Editor
    {
        private bool _isStarted = false;
        private Vector2Int _targetGridDimensions;

        private LevelName _levelToEdit;
        private LevelData _levelDataToEdit;

        private GridObjectEditorData _objectEditorData;
        private InteractableObjectType _selectedInteractableObjectType;

        private void SetupEditorForNewLevel(LevelManager levelManager, LevelName levelName)
        {
            _levelDataToEdit = Resources.Load<LevelData>($"LevelData/{levelName}");
            _targetGridDimensions = _levelDataToEdit.GridDimensions;
            _levelToEdit = levelName;
            levelManager.StartLevel = levelName;
        }

        private void DrawLineWithMargin(int margin = 5)
        {
            GUILayout.Space(margin);
            GUILayout.Box("", GUILayout.Width(1000f), GUILayout.Height(1));
            GUILayout.Space(margin);
        }

        private void DrawGridEditor()
        {
            var levelManagerTarget = (LevelManager)target;

            if (!_isStarted)
            {
                _objectEditorData = (GridObjectEditorData)EditorGUIUtility.Load("GridObjectData.asset");
                SetupEditorForNewLevel(levelManagerTarget, levelManagerTarget.StartLevel);
                _isStarted = true;
            }

            var labelWidthCache = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100f;
            var newLevelName =
                (LevelName)EditorGUILayout.EnumPopup("Level To Edit: ", _levelToEdit, GUILayout.MaxWidth(200f));

            if (newLevelName != _levelToEdit)
            {
                SetupEditorForNewLevel(levelManagerTarget, newLevelName);
            }

            DrawLineWithMargin();

            _targetGridDimensions =
                EditorGUILayout.Vector2IntField("Grid Dimension: ", _targetGridDimensions, GUILayout.Width(300f));

            _targetGridDimensions = new Vector2Int(Mathf.Max(_targetGridDimensions.x, 8), Mathf.Max(_targetGridDimensions.y, 8));

            if (GUILayout.Button("Generate Grid", GUILayout.Width(300f)))
            {
                _levelDataToEdit.GridDimensions = _targetGridDimensions;
                _levelDataToEdit.GridItemSerializedData =
                    new InteractableObjectType[_levelDataToEdit.GridDimensions.x * _levelDataToEdit.GridDimensions.y];
                EditorUtility.SetDirty(levelManagerTarget);
                EditorUtility.SetDirty(_levelDataToEdit);
            }

            DrawLineWithMargin();

            GUILayout.BeginHorizontal();
            for (var i = 0; i < _levelDataToEdit.Goals.Length; i++)
            {
                GUILayout.BeginVertical(GUILayout.MaxWidth(100f));

                var goalType = (InteractableObjectType)EditorGUILayout.EnumPopup("GoalType", _levelDataToEdit.Goals[i].GoalType);
                if (goalType != _levelDataToEdit.Goals[i].GoalType)
                {
                    _levelDataToEdit.Goals[i].GoalType = goalType;
                    EditorUtility.SetDirty(levelManagerTarget);
                    EditorUtility.SetDirty(_levelDataToEdit);
                }

                var goalCount = EditorGUILayout.IntField("GoalCount: ", _levelDataToEdit.Goals[i].TargetGoalCount);
                if (goalCount != _levelDataToEdit.Goals[i].TargetGoalCount)
                {
                    _levelDataToEdit.Goals[i].TargetGoalCount = goalCount;
                    EditorUtility.SetDirty(levelManagerTarget);
                    EditorUtility.SetDirty(_levelDataToEdit);
                }

                if (GUILayout.Button("RemoveGoal"))
                {
                    var goalArray = _levelDataToEdit.Goals.ToList();
                    goalArray.RemoveAt(i);
                    _levelDataToEdit.Goals = goalArray.ToArray();
                    EditorUtility.SetDirty(levelManagerTarget);
                    EditorUtility.SetDirty(_levelDataToEdit);

                    break;
                }

                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();

            if (GUILayout.Button("AddGoal", GUILayout.Width(300f)))
            {
                var goalArray = new Goal[_levelDataToEdit.Goals.Length + 1];
                _levelDataToEdit.Goals.CopyTo(goalArray, 0);
                _levelDataToEdit.Goals = goalArray;
                EditorUtility.SetDirty(levelManagerTarget);
                EditorUtility.SetDirty(_levelDataToEdit);
            }

            DrawLineWithMargin();

            if (_levelDataToEdit.GridItemSerializedData == null)
            {
                return;
            }

            GUILayout.BeginHorizontal();
            for (var i = 0; i < _objectEditorData.GridObjectData.Length; i++)
            {
                var colorCache = GUI.color;
                if (_objectEditorData.GridObjectData[i].ObjectType == _selectedInteractableObjectType)
                {
                    GUI.color = Color.green;
                }

                if (GUILayout.Button(_objectEditorData.GridObjectData[i].ObjectEditorTexture, GUILayout.Width(75f),
                                     GUILayout.Height(75f)))
                {
                    _selectedInteractableObjectType = _objectEditorData.GridObjectData[i].ObjectType;
                }

                GUI.color = colorCache;
            }

            GUILayout.EndHorizontal();
            DrawLineWithMargin();

            const float imageSize = 50f;
            GUILayout.BeginVertical(GUILayout.MaxHeight(imageSize));
            {
                var gridDim = _levelDataToEdit.GridDimensions;
                for (var y = gridDim.y - 1; y >= 0; y--)
                {
                    GUILayout.BeginHorizontal(GUILayout.Width(imageSize), GUILayout.Height(imageSize));
                    for (var x = 0; x < gridDim.x; x++)
                    {
                        if (GUILayout.Button(_objectEditorData.GetTextureForType(_levelDataToEdit.GridItemSerializedData[gridDim.x * y + x]),
                                             GUILayout.Width(imageSize),
                                             GUILayout.Height(imageSize)))
                        {
                            _levelDataToEdit.GridItemSerializedData[gridDim.x * y + x] = _selectedInteractableObjectType;
                            EditorUtility.SetDirty(levelManagerTarget);
                            EditorUtility.SetDirty(_levelDataToEdit);
                        }
                    }

                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
            EditorGUIUtility.labelWidth = labelWidthCache;
            DrawLineWithMargin();
        }

        public override void OnInspectorGUI()
        {
            DrawGridEditor();
            base.OnInspectorGUI();
        }
    }
}