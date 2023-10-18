using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsTexture : MonoBehaviour
{
    public Object[] tools;
    public MeshRenderer[] toolsRenderer;
    public Material[] materialForTool1;
    public Material[] materialForTool2;
    public Material[] materialForTool3;

    // Update is called once per frame
    void Update()
    {
        toolsRenderer[0].material = materialForTool1[tools[0].level];
        toolsRenderer[1].material = materialForTool2[tools[1].level];
        toolsRenderer[2].material = materialForTool3[tools[2].level];
    }
}
