using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundAsset), true)]
public class SoundAssetEditor : Editor
{
    AudioSource virtualSource;

    private void OnEnable()
    {
        virtualSource = EditorUtility.CreateGameObjectWithHideFlags("Virtual Audio Source", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        DestroyImmediate(virtualSource.gameObject);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Listen"))
        {
            ((SoundAsset)target).Play(virtualSource);
        }
        EditorGUI.EndDisabledGroup();
    }

}
