using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(GunType))]
[CanEditMultipleObjects]
public class Editor_GunType : Editor_RangedType
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

		EditorGUILayout.PropertyField(serializedObject.FindProperty("firerate"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("muzzleVelocity"));
	}
}
