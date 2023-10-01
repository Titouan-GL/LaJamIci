using UnityEngine;

public class UtilitiesStatic
{
    static public void Shake(){
        Camera.main.gameObject.GetComponent<CameraScript>().Shake();
    }

    static public LayerMask GetLevelLayerMask(){
        int layerMask = 1 << 11;
        return layerMask;
    }

    static public UtilitiesNonStatic GetUNS(){
        return GameObject.FindGameObjectWithTag("UNS").GetComponent<UtilitiesNonStatic>();
    }
}
