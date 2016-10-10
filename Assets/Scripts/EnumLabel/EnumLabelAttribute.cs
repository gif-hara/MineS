using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#endif

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumLabelAttribute : PropertyAttribute
{
	public string label;

	public Type enumType;

	public EnumLabelAttribute(string label)
	{
		this.label = label;
		this.enumType = null;
	}

	public EnumLabelAttribute(string label, Type enumType)
	{
		this.label = label;
		this.enumType = enumType;
	}
}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnumLabelAttribute))]
public class EnumLabelDrawer : PropertyDrawer
{
	private Dictionary<string, string> customEnumNames = new Dictionary<string, string>();

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if(enumLabelAttribute.enumType != null)
		{
			SetUpCustomEnumNames(property, Enum.GetNames(enumLabelAttribute.enumType));
			EditorGUI.BeginChangeCheck();
			string[] displayedOptions = Enum.GetNames(enumLabelAttribute.enumType)
				.Where(enumName => customEnumNames.ContainsKey(enumName))
				.Select<string, string>(enumName => customEnumNames[enumName])
				.ToArray();
			int selectedIndex = EditorGUI.Popup(position, enumLabelAttribute.label, property.enumValueIndex, displayedOptions);
			if(EditorGUI.EndChangeCheck())
			{
				property.enumValueIndex = selectedIndex;
			}
		}
		else if(property.propertyType == SerializedPropertyType.Enum)
		{
			SetUpCustomEnumNames(property, property.enumNames);
			EditorGUI.BeginChangeCheck();
			string[] displayedOptions = property.enumNames
                    .Where(enumName => customEnumNames.ContainsKey(enumName))
                    .Select<string, string>(enumName => customEnumNames[enumName])
                    .ToArray();
			int selectedIndex = EditorGUI.Popup(position, enumLabelAttribute.label, property.enumValueIndex, displayedOptions);
			if(EditorGUI.EndChangeCheck())
			{
				property.enumValueIndex = selectedIndex;
			}
		}
	}

	private EnumLabelAttribute enumLabelAttribute
	{
		get
		{
			return (EnumLabelAttribute)attribute;
		}
	}

	public void SetUpCustomEnumNames(SerializedProperty property, string[] enumNames)
	{
		Type type = property.serializedObject.targetObject.GetType();
		foreach(FieldInfo fieldInfo in type.GetFields())
		{
			object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EnumLabelAttribute), false);
			foreach(EnumLabelAttribute customAttribute in customAttributes)
			{
				Type enumType = enumLabelAttribute.enumType != null ? enumLabelAttribute.enumType : fieldInfo.FieldType;

				foreach(string enumName in enumNames)
				{
					FieldInfo field = enumType.GetField(enumName);
					if(field == null)
						continue;
					EnumLabelAttribute[] attrs = (EnumLabelAttribute[])field.GetCustomAttributes(customAttribute.GetType(), false);

					if(!customEnumNames.ContainsKey(enumName))
					{
						foreach(EnumLabelAttribute labelAttribute in attrs)
						{
							customEnumNames.Add(enumName, labelAttribute.label);
						}
					}
				}
			}
		}
	}
}

#endif