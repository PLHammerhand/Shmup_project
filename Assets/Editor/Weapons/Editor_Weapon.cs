using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(Weapon))]
[CanEditMultipleObjects]
public class Editor_Weapon : Editor
{
	protected virtual void _ShowWeaponProperties()
	{
		EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("range"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("fireParticles"));
	}
}
