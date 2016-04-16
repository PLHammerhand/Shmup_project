using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(SpawnType))]
[CanEditMultipleObjects]
public class Editor_SpawnType : Editor_Weapon
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		_ShowWeaponProperties();

		serializedObject.ApplyModifiedProperties();
	}

	protected override void _ShowWeaponProperties()
	{
		EditorGUILayout.PropertyField(serializedObject.FindProperty("fireParticles"));
		//	TODO	debug - editor error
		EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnList"), true, null);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnPosition"));

		if(serializedObject.FindProperty("spawnList").arraySize > 1)
			EditorGUILayout.PropertyField(serializedObject.FindProperty("randomSpawnFromList"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("randomSpawnPosition"));

		if(serializedObject.FindProperty("randomSpawnPosition").boolValue)
			EditorGUILayout.PropertyField(serializedObject.FindProperty("range"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnDelay"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("randomSpawnTime"));

		if(serializedObject.FindProperty("randomSpawnTime").boolValue)
			EditorGUILayout.PropertyField(serializedObject.FindProperty("spawnTimeInterval"));
	}
}
