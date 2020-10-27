using UnityEngine;
using UnityEditor;

[UnityEditor.CanEditMultipleObjects()]
[UnityEditor.CustomEditor(typeof(OnTriggerEvent))]
public class OnTriggerEventEditor : Editor
{
  #region ShowStates
  bool showDestroyOptions = false;
  bool showTriggerOptions = false;
  bool showTagOptions = false;
  bool showEvents = false;
  bool showRepeatOptions = false;

  bool showScriptName = false;
  #endregion

  #region Properties
  SerializedProperty destroyOnEnterProperty;
  SerializedProperty destroyOnExitProperty;

  SerializedProperty notTagProperty;
  SerializedProperty isEnabledProperty;

  SerializedProperty triggerOnceProperty;

  SerializedProperty hasScriptProperty;

  SerializedProperty repeatDelayProperty;

  SerializedProperty scriptNameProperty;

  SerializedProperty repeatDelayTimeProperty;

  SerializedProperty hitTagProperty;
  SerializedProperty onEnterProperty;
  SerializedProperty onStayProperty;
  SerializedProperty onExitProperty;
  SerializedProperty otherColliderProperty;

  SerializedProperty hasBeenTriggeredProperty;

  #endregion

  void OnEnable()
  {
    destroyOnEnterProperty = serializedObject.FindProperty("destroyOnEnter");
    destroyOnExitProperty = serializedObject.FindProperty("destroyOnExit");

    notTagProperty = serializedObject.FindProperty("notTag");
    isEnabledProperty = serializedObject.FindProperty("isEnabled");

    triggerOnceProperty = serializedObject.FindProperty("triggerOnce");

    hasScriptProperty = serializedObject.FindProperty("hasScript");

    repeatDelayProperty = serializedObject.FindProperty("repeatDelay");

    scriptNameProperty = serializedObject.FindProperty("scriptName");

    repeatDelayTimeProperty = serializedObject.FindProperty("repeatDelayTime");

    hitTagProperty = serializedObject.FindProperty("hitTag");
    onEnterProperty = serializedObject.FindProperty("onEnter");
    onStayProperty = serializedObject.FindProperty("onStay");
    onExitProperty = serializedObject.FindProperty("onExit");
    otherColliderProperty = serializedObject.FindProperty("otherCollider");

    hasBeenTriggeredProperty = serializedObject.FindProperty("hasBeenTriggered");
  }

  public override void OnInspectorGUI()
  {
    EditorGUILayout.PropertyField(isEnabledProperty);

    showDestroyOptions = EditorGUILayout.Foldout(showDestroyOptions, "Destroy Options");
    if (showDestroyOptions)
    {
      EditorGUILayout.BeginVertical(GUI.skin.box);
      {
        EditorGUILayout.PropertyField(destroyOnEnterProperty);
        EditorGUILayout.PropertyField(destroyOnExitProperty);
      }
      EditorGUILayout.EndVertical();
    }

    showTagOptions = EditorGUILayout.Foldout(showTagOptions, "Tag Options");
    if (showTagOptions)
    {
      EditorGUILayout.BeginVertical(GUI.skin.box);
      {
        EditorGUILayout.PropertyField(notTagProperty);
        EditorGUILayout.PropertyField(hitTagProperty);
      }
      EditorGUILayout.EndVertical();

      EditorGUILayout.BeginVertical(GUI.skin.box);
      {
        EditorGUILayout.PropertyField(hasScriptProperty);
        if (hasScriptProperty.boolValue)
          EditorGUILayout.PropertyField(scriptNameProperty);
      }
      EditorGUILayout.EndVertical();
    }

    showRepeatOptions = EditorGUILayout.Foldout(showRepeatOptions, "Repeat Options");
    if (showRepeatOptions)
    {
      EditorGUILayout.BeginVertical(GUI.skin.box);
      {
        EditorGUILayout.PropertyField(triggerOnceProperty);
        EditorGUILayout.PropertyField(repeatDelayProperty);
        if (repeatDelayProperty.boolValue)
          EditorGUILayout.PropertyField(repeatDelayTimeProperty);
      }
      EditorGUILayout.EndVertical();
    }

    showEvents = EditorGUILayout.Foldout(showEvents, "Events");
    if (showEvents)
    {
      EditorGUILayout.BeginVertical(GUI.skin.box);
      {
        EditorGUILayout.PropertyField(onEnterProperty);
        EditorGUILayout.PropertyField(onStayProperty);
        EditorGUILayout.PropertyField(onExitProperty);
      }
      EditorGUILayout.EndVertical();
    }

    EditorGUILayout.PropertyField(otherColliderProperty);

    serializedObject.ApplyModifiedProperties();
  }

  public void OnInspectorUpdate()
  {
    Repaint();
  }

}
