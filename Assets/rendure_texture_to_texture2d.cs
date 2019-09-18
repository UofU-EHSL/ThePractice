using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rendure_texture_to_texture2d : MonoBehaviour {

    public RenderTexture tex;
    public bool take_picture;
    public Material[] material;
    public int counter = 0;

    public void take()
    {
        if (counter >= material.Length)
        {
            counter = 0;
            take_picture = false;
        }
        else
        {
            counter++;
            take_picture = false;
        }
    }
    public void Update()
    {
        if (take_picture == true)
        {
            take();
            material[counter].SetTexture("_MainTex", toTexture2D(tex));
        }
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}