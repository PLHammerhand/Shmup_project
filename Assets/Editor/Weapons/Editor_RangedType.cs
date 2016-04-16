using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(RangedType))]
[CanEditMultipleObjects]
public class Editor_RangedType : Editor_Weapon
{
	protected override void _ShowWeaponProperties()
	{
		base._ShowWeaponProperties();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletPrefab"));
	}
}
