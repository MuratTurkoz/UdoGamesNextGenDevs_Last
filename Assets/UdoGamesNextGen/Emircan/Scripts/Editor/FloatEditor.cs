using UnityEditor;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [CustomEditor(typeof(Float))]
    public class FloatEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Float floatVariable = (Float)target;

            if (GUILayout.Button("Update UI"))
            {
                floatVariable.Value = floatVariable.Value;
            }
        }
    }
}
