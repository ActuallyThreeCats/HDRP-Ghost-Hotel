using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class FurnitureEditor : OdinMenuEditorWindow
{
    [MenuItem("Crafty/FurnitureEditor")]
    private static void OpenWindow()
    {
        GetWindow<FurnitureEditor>().Show();
    }

    private CreateNewFurniture createNewFurniture;

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if(createNewFurniture != null)
        {
            DestroyImmediate(createNewFurniture.furnitureObject);
        }
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        
        var tree = new OdinMenuTree();

        createNewFurniture = new CreateNewFurniture();
        tree.Add("Create New", createNewFurniture);
        tree.AddAllAssetsAtPath("Furniture Data", "Assets/Furniture", typeof(FurnitureScriptableObject));
        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        OdinMenuTreeSelection selected = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if(SirenixEditorGUI.ToolbarButton("Delete Current"))
            {
                FurnitureScriptableObject asset = selected.SelectedValue as FurnitureScriptableObject;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }


    public class CreateNewFurniture
    {
        public CreateNewFurniture()
        {
            furnitureObject = ScriptableObject.CreateInstance<FurnitureScriptableObject>();
            furnitureObject.name = "New Furniture Item";
        }

        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public FurnitureScriptableObject furnitureObject;

        [Button("Add New Furniture SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(furnitureObject, "Assets/Furniture/" + furnitureObject.name + ".asset");
            AssetDatabase.SaveAssets();

            furnitureObject = ScriptableObject.CreateInstance<FurnitureScriptableObject>();
            furnitureObject.name = "New Furniture Item";
        }
    }
}
