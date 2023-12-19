using System.IO;
using UnityEditor;
using UnityEngine;

namespace GradientSkybox.Editor
{
    public class CircularMultipleColorGradientSkyboxGUI : ShaderGUI
    {
        private GradientObject gradientObject = null;
        private bool isGradientSaved = false;

        public override void OnGUI(MaterialEditor editor, MaterialProperty[] properties)
        {
            var norm = FindProperty("_Norm", properties);
            editor.ShaderProperty(norm, norm.displayName);

            var material = editor.target as Material;
            var materialRelativePath = AssetDatabase.GetAssetPath(material);

            if (gradientObject == null)
            {
                var objectRelativePath = materialRelativePath + ".asset";
                gradientObject = AssetDatabase.LoadAssetAtPath<GradientObject>(objectRelativePath);
                if (gradientObject == null)
                {
                    gradientObject = ScriptableObject.CreateInstance<GradientObject>();
                    AssetDatabase.CreateAsset(gradientObject, objectRelativePath);
                    AssetDatabase.Refresh();
                }
            }

            var data = new SerializedObject(gradientObject);
            data.Update();
            var gradientProperty = data.FindProperty("gradient");
            Texture2D texture = null;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(gradientProperty);
            if (EditorGUI.EndChangeCheck())
            {
                data.ApplyModifiedProperties();
                texture = CreateRampTexture();
                texture.wrapMode = TextureWrapMode.Clamp;
                material.SetTexture("_RampTex", texture);
                isGradientSaved = false;
            }

            if (GUILayout.Button("Save Gradient"))
            {
                if (texture == null)
                {
                    texture = CreateRampTexture();
                }

                var png = texture.EncodeToPNG();
                var textureRelativePath = materialRelativePath + ".png";
                var textureAbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), textureRelativePath);
                File.WriteAllBytes(textureAbsolutePath, png);

                var textureImporter = AssetImporter.GetAtPath(textureRelativePath) as TextureImporter;
                textureImporter.wrapMode = TextureWrapMode.Clamp;
                AssetDatabase.ImportAsset(textureRelativePath);

                var savedTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(textureRelativePath);
                material.SetTexture("_RampTex", savedTexture);

                isGradientSaved = true;
            }

            if (!isGradientSaved)
            {
                EditorGUILayout.HelpBox("Changes to gradient has not saved yet.", MessageType.Warning);
            }
        }


        private Texture2D CreateRampTexture()
        {
            var gradient = gradientObject.gradient;
            var texture = new Texture2D(128, 2);
            for (var h = 0; h < texture.height; h++)
            {
                for (var w = 0; w < texture.width; w++)
                {
                    texture.SetPixel(w, h, gradient.Evaluate((float)w / texture.width));
                }
            }
            texture.Apply();
            return texture;
        }
    }
}
