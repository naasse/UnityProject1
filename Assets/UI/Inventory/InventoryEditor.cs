using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/inventory
[CustomEditor (typeof(Inventory))]
public class InventoryEditor : Editor {

    private SerializedProperty ItemImagesProperty;
    private SerializedProperty ItemsProperty;
    private bool[] showItemSlots = new bool[Inventory.numItemSlots];

    private const string inventoryPropItemImagesName = "itemImages";
    private const string inventoryPropItemsName = "items";

    private void OnEnable()
    {
        ItemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
        ItemsProperty = serializedObject.FindProperty(inventoryPropItemsName);
    }
    //overrides how edtior views this
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for(int i =0; i<Inventory.numItemSlots; i++)
        {
            itemSlotsGui(i);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void itemSlotsGui(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
        //foldout takes in bool, verifies if its open or closed, and if its true it returns the changed value
        showItemSlots[index] =EditorGUILayout.Foldout(showItemSlots[index], "Item slot " + index);

        if (showItemSlots[index])
        {
            EditorGUILayout.PropertyField(ItemImagesProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(ItemsProperty.GetArrayElementAtIndex(index));
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
