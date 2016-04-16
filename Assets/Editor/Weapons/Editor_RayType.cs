using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(RayType))]
[CanEditMultipleObjects]
public class Editor_RayType : Editor_RangedType
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		_ShowWeaponProperties();

		serializedObject.ApplyModifiedProperties();
	}

	protected override void _ShowWeaponProperties()
	{
		base._ShowWeaponProperties();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("hasCooldown"));

        if((bool)serializedObject.FindProperty("hasCooldown").boolValue)
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxFireTime"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldownDelay"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldownTime"));
		}
	}
}
