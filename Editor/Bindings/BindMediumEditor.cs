using System.Reflection;
using System.Runtime.CompilerServices;
using Flexy.UI.Bindings;
using UnityEditor;

namespace Flexy.UI.Editor.Bindings;

[CustomEditor(typeof(BindMedium))]
public class BindMediumEditor: UnityEditor.Editor
{
	private		Boolean		_findEnabled;
	private		Type[]		_extractedTypes;
	
	public override void OnInspectorGUI()
	{
		serializedObject.UpdateIfRequiredOrScript();
		
		DrawExpectedType();
		DrawObjBindableFields();
			
		serializedObject.ApplyModifiedProperties();
	}
		
	private void DrawExpectedType		( )		
	{
		var propTypeName = serializedObject.FindProperty("ExpectedType"); 
			
		var typeString = propTypeName.stringValue;
		var type = Type.GetType(typeString);
			
		GUILayout.BeginHorizontal();
		{
			EditorGUILayout.PrefixLabel("Expected Type");
			GUILayout.Label(type != null ? GetClassNamesFromFullName(type.FullName) : "Undefined");
				
			if (!_findEnabled)
			{
				if (GUILayout.Button("Find", GUILayout.Width(50))) _findEnabled = true;
			}
			else
			{
				GUILayout.Label("...", GUILayout.Width(50));
			}
		}
		GUILayout.EndHorizontal();
			
		if (_findEnabled)
		{
			GUILayout.BeginVertical(new GUIContent("Finding..."), GUI.skin.window);
			{
				if (_extractedTypes == null)
				{
					UnityEngine.Object obj = null;
						
					EditorGUI.BeginChangeCheck();
					obj = EditorGUILayout.ObjectField("Specify Object", obj, typeof(UnityEngine.Object), true);
						
					if (EditorGUI.EndChangeCheck() && obj != null)
						_extractedTypes = GetAllTypesFromObject(obj).Where(t => !t.Name.Contains("<")).ToArray();
				}
				else
				{
					var popupNames = _extractedTypes.Select( t => GetClassNamesFromFullName(t.FullName) ).ToArray();
					EditorGUI.BeginChangeCheck();
						
					var newSelection = EditorGUILayout.Popup("Choose Type", 0, popupNames);
					if (EditorGUI.EndChangeCheck())
					{
						var newType		= _extractedTypes[newSelection];
						propTypeName.stringValue = $"{newType.FullName}, {newType.Assembly.FullName[..newType.Assembly.FullName.IndexOf(",", StringComparison.Ordinal)]}";
						_extractedTypes	= null;
						_findEnabled		= false;
					}
				}
			}
			GUILayout.EndVertical();
		}
	}
	private void DrawObjBindableFields	( )		
	{
		GUILayout.Space(10);
		GUILayout.Label("Bindable Fields", EditorStyles.boldLabel);
		GUILayout.Space(5);
		
		if (Application.isPlaying && target is BindMedium bindMedium && bindMedium.gameObject.scene.IsValid())
		{
			// Draw bindMedium.DataStruct fields
			var dataStruct = bindMedium.DataObj;
			if (dataStruct == null) 
				return;
			
			var dataStructType = dataStruct.GetType();
			foreach (var property in dataStructType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => !p.IsDefined(typeof(CompilerGeneratedAttribute), false)))
			{
				if (!property.CanRead)
					continue;

				var currentValue = property.GetValue(dataStruct);
				var propertyType = property.PropertyType;
				Object newValue = null;
				
				GUI.enabled = property.CanWrite; 
				EditorGUI.BeginChangeCheck();

				// Handle different property types
				if (propertyType == typeof(Int32))			newValue = EditorGUILayout.IntField			(property.Name, (Int32)		(currentValue ?? 0));
				else if (propertyType == typeof(Single))	newValue = EditorGUILayout.FloatField		(property.Name, (Single)	(currentValue ?? 0f));
				else if (propertyType == typeof(Double))	newValue = EditorGUILayout.DoubleField		(property.Name, (Double)	(currentValue ?? 0.0));
				else if (propertyType == typeof(String))	newValue = EditorGUILayout.TextField		(property.Name, (String)	(currentValue ?? ""));
				else if (propertyType == typeof(Boolean))	newValue = EditorGUILayout.Toggle			(property.Name, (Boolean)	(currentValue ?? false));
				else if (propertyType == typeof(Vector2))	newValue = EditorGUILayout.Vector2Field		(property.Name, (Vector2)	(currentValue ?? Vector2.zero));
				else if (propertyType == typeof(Vector3))	newValue = EditorGUILayout.Vector3Field		(property.Name, (Vector3)	(currentValue ?? Vector3.zero));
				else if (propertyType == typeof(Vector4))	newValue = EditorGUILayout.Vector4Field		(property.Name, (Vector4)	(currentValue ?? Vector4.zero));
				else if (propertyType == typeof(Color))		newValue = EditorGUILayout.ColorField		(property.Name, (Color)		(currentValue ?? Color.white));
				else if (propertyType == typeof(Rect))		newValue = EditorGUILayout.RectField		(property.Name, (Rect)		(currentValue ?? new Rect()));
				else if (propertyType == typeof(Bounds))	newValue = EditorGUILayout.BoundsField		(property.Name, (Bounds)	(currentValue ?? new Bounds()));
				else if (propertyType == typeof(AnimationCurve)) newValue = EditorGUILayout.CurveField	(property.Name, (AnimationCurve)currentValue ?? new AnimationCurve());
				else if (propertyType.IsEnum) newValue = EditorGUILayout.EnumPopup(property.Name, (Enum)(currentValue ?? Activator.CreateInstance(propertyType)));
				else if (typeof(UnityEngine.Object).IsAssignableFrom(propertyType)) newValue = EditorGUILayout.ObjectField(property.Name, (UnityEngine.Object)currentValue, propertyType, true);
				else
				{
					// For unsupported types, show as read-only label
					EditorGUILayout.LabelField(property.Name, currentValue?.ToString() ?? "null");
				}

				GUI.enabled = true;
				if (EditorGUI.EndChangeCheck() && property.CanWrite)
				{
					property.SetValue(dataStruct, newValue);
					EditorUtility.SetDirty(bindMedium);
					bindMedium.RebindAll();
				}
			}
		} 
		else
		{
			// Draw expected type bindable field labels
			var propTypeName = serializedObject.FindProperty("ExpectedType"); 
			var dataStructType = Type.GetType(propTypeName.stringValue);
			if (dataStructType == null) 
				return;
			
			foreach (var field in dataStructType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => !p.IsDefined(typeof(CompilerGeneratedAttribute), false)))
			{
				EditorGUILayout.LabelField(field.Name);
			}
		}
	}
		
	public static Type[] GetAllTypesFromObject(UnityEngine.Object unityObject)
	{
		if (unityObject == null)
			return Array.Empty<Type>();

		var types = new HashSet<Type>();

		// If it's a GameObject, get all components
		if (unityObject is GameObject gameObject)
		{
			var components = gameObject.GetComponents<Component>();
			foreach (var component in components)
			{
				if (component == null || component is not MonoBehaviour)
					continue;

				AddTypeHierarchy(component.GetType(), types);
			}
		}
		// If it's a Component, get it and its GameObject's other components
		else if (unityObject is Component component)
		{
			if (component != null && component.gameObject != null)
			{
				var components = component.gameObject.GetComponents<Component>();
				foreach (var comp in components)
				{
					if (comp == null || component is not MonoBehaviour)
						continue;

					AddTypeHierarchy(comp.GetType(), types);
				}
			}
		}
		// For any other Unity Object (ScriptableObject, Asset, etc.), just add its type hierarchy
		else
		{
			AddTypeHierarchy(unityObject.GetType(), types);
		}

		return types.ToArray();
	}

	private static void AddTypeHierarchy(Type type, HashSet<Type> types)
	{
		// Add the type itself
		types.Add(type);

		// Get nested types (types declared inside this type)
		var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
	
		foreach (var nestedType in nestedTypes)
		{
			types.Add(nestedType);
			// Recursively add nested types of nested types
			AddTypeHierarchy(nestedType, types);
		}
	}

	private static String GetClassNamesFromFullName(String fullName)
	{
		if (String.IsNullOrEmpty(fullName))
			return String.Empty;

		var assemblyIndex = fullName.IndexOf(',');
		var typeFullName = assemblyIndex > 0 ? fullName[..assemblyIndex] : fullName;
		typeFullName = typeFullName.Trim();

		// Remove namespace, keep only class names (including nested classes)
		var lastDotIndex = typeFullName.LastIndexOf('.');
		var result = lastDotIndex >= 0 ? typeFullName.Substring(lastDotIndex + 1) : typeFullName;
		return result.Replace("+", ".");
	}

		
}