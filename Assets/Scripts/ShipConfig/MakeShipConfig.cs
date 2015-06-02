using System;
using UnityEditor;
using UnityEngine;

public class MakeShipConfig
{
  [MenuItem("Assets/Create/Ship Config")]
  public static void CreateShipConfig()
  {
    ShipConfigObject sco = ScriptableObject.CreateInstance<ShipConfigObject>();

    string path = EditorUtility.SaveFilePanelInProject("Create Ship Config", "ShipConfig", "asset", "Enter a file name to save the ship config to.");
    if (String.IsNullOrEmpty(path)) return;

    AssetDatabase.CreateAsset(sco, path);
    AssetDatabase.SaveAssets();

    EditorUtility.FocusProjectWindow();

    Selection.activeObject = sco;
  }
}