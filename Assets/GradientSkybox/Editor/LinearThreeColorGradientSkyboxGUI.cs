using UnityEditor;
using UnityEngine;

namespace GradientSkybox.Editor
{
    public class LinearThreeColorGradientSkyboxGUI : ShaderGUI
    {
        public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
        {
            var topColor = FindProperty("_TopColor", properties);
            editor.ColorProperty(topColor, topColor.displayName);
            var middleColor = FindProperty("_MiddleColor", properties);
            editor.ColorProperty(middleColor, middleColor.displayName);
            var bottomColor = FindProperty("_BottomColor", properties);
            editor.ColorProperty(bottomColor, bottomColor.displayName);
            var up = FindProperty("_Up", properties);
            Vector3 upVector = up.vectorValue;
            upVector = EditorGUILayout.Vector3Field(up.displayName, upVector);
            up.vectorValue = upVector;
            var exp = FindProperty("_Exp", properties);
            editor.RangeProperty(exp, exp.displayName);
        }
    }
}