using UnityEditor;

[CustomEditor(typeof(Interactable), true)]

public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can only use UnityEvents.", MessageType.Info);
            if(interactable.GetComponent<INteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<INteractionEvent>();
            }
        } else
        {
        base.OnInspectorGUI();
        if(interactable.useEvents)
        {
            if (interactable.GetComponent<INteractionEvent>() == null)
            {
                interactable.gameObject.AddComponent<INteractionEvent>();
            } else
            {
                if (interactable.GetComponent<INteractionEvent>() != null)
                {
                    DestroyImmediate(interactable.GetComponent<INteractionEvent>());
                }
            }

        }
        }
        
    }
}
