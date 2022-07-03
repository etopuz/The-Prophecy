using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpritePivotEditor : EditorWindow
{
    private bool completedPivotChange = false;
    private GameObject selectedGameObject;
    private static SpritePivotEditor window;
    private static bool autoCorrectChildrenPositions = true;

    public SpritePivotEditor()
    {
        this.position = new Rect(new Vector2(100, 100), new Vector2(0, 0));
        this.maxSize = this.minSize = new Vector2(200, 80);
        SceneView.duringSceneGui -= OnScene;
        SceneView.duringSceneGui += OnScene;
    }

    [MenuItem("Tools/Set Sprite Pivot", priority = 0)]
    private static void SetSpritePivot()
    {
        window = (SpritePivotEditor)EditorWindow.GetWindow(typeof(SpritePivotEditor), true, null, false);
        window.selectedGameObject = Selection.activeGameObject;
        //Debug.Log("Setting sprite pivot for " + window.selectedGameObject.name + ". Waiting for mouse click.");
    }

    [MenuItem("Tools/Set Sprite Pivot", true)]
    private static bool ValidateSetSpritePivot()
    {
        return Selection.activeGameObject != null &&
            Selection.activeGameObject.GetComponent<SpriteRenderer>() != null &&
            Selection.activeGameObject.GetComponent<SpriteRenderer>().sprite != null;
    }

    private void OnScene(SceneView sceneView)
    {
        if (selectedGameObject == null) return;

        Event e = Event.current;
        Vector2 mousePos = e.mousePosition;
        mousePos.y = sceneView.camera.pixelHeight - mousePos.y;
        Vector3 position = sceneView.camera.ScreenPointToRay(mousePos).origin;

        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
        {
            Close();
        }

        //sets the selection back to the correct gameobject if requried to click on a different gameobjcet when setting pivot, must move mouse after clicking to trigger
        if (e.type == EventType.MouseMove && completedPivotChange)
        {
            Selection.activeGameObject = selectedGameObject;
            completedPivotChange = false;
            Close();
            return;
        }

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            updatePivot(position);
            e.Use();
            Event.current = null;
        }

        Handles.BeginGUI();
        {
            Handles.color = new Color(1, 0.92f, 0.016f, .1f);
            Handles.DrawSolidDisc(Event.current.mousePosition, Vector3.forward, 10f);
        }
        Handles.EndGUI(); HandleUtility.Repaint();
    }

    private void updatePivot(Vector2 worldMousePos)
    {
        //create child gameobject since it is in the context that we need
        GameObject helperGO = new GameObject();
        helperGO.transform.SetParent(selectedGameObject.transform);
        //reset all transform settings
        helperGO.transform.localPosition = new Vector3(0, 0, 0);
        helperGO.transform.localScale = new Vector3(1, 1, 1);
        helperGO.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        Vector2 childMousePos = helperGO.transform.InverseTransformPoint(worldMousePos);
        DestroyImmediate(helperGO);

        //get spriteRenderer
        SpriteRenderer sr = selectedGameObject.GetComponent<SpriteRenderer>();
        string path = AssetDatabase.GetAssetPath(sr.sprite.texture);
        TextureImporter ti = (TextureImporter)AssetImporter.GetAtPath(path);
        //take tke local mouse position and divide by the number of units of width and height of the sprite (based on it's pixels per unit), then we have to add the current sprite pivot point so it's relative to that
        Vector2 newPivot = new Vector2(childMousePos.x / (sr.sprite.texture.width / sr.sprite.pixelsPerUnit), childMousePos.y / (sr.sprite.texture.height / sr.sprite.pixelsPerUnit)) + ti.spritePivot;
        ti.spritePivot = newPivot;
        TextureImporterSettings texSettings = new TextureImporterSettings();
        //set to use custom pivot point
        ti.ReadTextureSettings(texSettings);
        texSettings.spriteAlignment = (int)SpriteAlignment.Custom;
        ti.SetTextureSettings(texSettings);
        ti.SaveAndReimport();
        if (autoCorrectChildrenPositions)
        {
            Dictionary<Transform, Vector3> childrenWorldPosMap = new Dictionary<Transform, Vector3>();
            foreach (Transform child in selectedGameObject.transform)
            {
                childrenWorldPosMap.Add(child, child.transform.position);
            }
            selectedGameObject.transform.position = worldMousePos;
            foreach (Transform child in selectedGameObject.transform)
            {
                child.transform.position = childrenWorldPosMap[child];
            }
            //Debug.Log(childMousePos);
        }
        completedPivotChange = true;
        //Debug.Log("Successfully set sprite pivot for " + selectedGameObject.name + ".");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("Target: " + selectedGameObject.name);
        GUILayout.Label("Click in scene view to set pivot.");
        autoCorrectChildrenPositions = GUILayout.Toggle(autoCorrectChildrenPositions, "Auto Correct Local Positions");
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }
    }

    private void OnDestroy()
    {
        SceneView.duringSceneGui -= OnScene;
        selectedGameObject = null;
    }
}