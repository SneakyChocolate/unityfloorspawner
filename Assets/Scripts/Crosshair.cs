using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Camera _headCamera;
    public float lineLength = 10f;          
    public float lineWidth = 2f;            
    public float gapSize = 5f;             
    public Color crosshairColor = Color.red;

    private Material lineMaterial;

    private void Awake()
    {
        _headCamera = gameObject.GetComponent<Camera>();
        CreateLineMaterial();
    }

    private void CreateLineMaterial()
    {
        lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        lineMaterial.SetInt("_ZWrite", 0);
    }

    private void OnPostRender()
    {
        if (_headCamera == null || lineMaterial == null) return;
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.Color(crosshairColor);
        
        Vector2 center = new Vector2(0.5f, 0.5f);
        DrawThickLine(center + new Vector2(-gapSize / Screen.width, 0), 
                      center + new Vector2(-gapSize / Screen.width - lineLength / Screen.width, 0), 
                      lineWidth / Screen.height);
        
        DrawThickLine(center + new Vector2(gapSize / Screen.width, 0), 
                      center + new Vector2(gapSize / Screen.width + lineLength / Screen.width, 0), 
                      lineWidth / Screen.height);
        DrawThickLine(center + new Vector2(0, gapSize / Screen.height), 
                      center + new Vector2(0, gapSize / Screen.height + lineLength / Screen.height), 
                      lineWidth / Screen.width);
        DrawThickLine(center + new Vector2(0, -gapSize / Screen.height), 
                      center + new Vector2(0, -gapSize / Screen.height - lineLength / Screen.height), 
                      lineWidth / Screen.width);

        GL.End();
        GL.PopMatrix();
    }
    private void DrawThickLine(Vector2 start, Vector2 end, float thickness)
    {
        Vector2 direction = (end - start).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x) * thickness / 2;

        GL.Vertex(new Vector3(start.x - perpendicular.x, start.y - perpendicular.y, 0));
        GL.Vertex(new Vector3(start.x + perpendicular.x, start.y + perpendicular.y, 0));
        GL.Vertex(new Vector3(end.x + perpendicular.x, end.y + perpendicular.y, 0));
        GL.Vertex(new Vector3(end.x - perpendicular.x, end.y - perpendicular.y, 0));
    }
}
