using UnityEditor;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    [CustomEditor(typeof(Int))]
    public class IntEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Int intVariable = (Int)target;

            if (GUILayout.Button("Update UI"))
            {
                intVariable.Value = intVariable.Value;
            }
        }
    }
}
