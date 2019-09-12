Thank you for downloading! :)

Translucent Subsurface Scattering support features:
=> Directional and Point light source.
=> Support maximal 4 point light sources.
=> Bump and Non-Bump mapping.
=> Full control of effect parameters.

How to use ?
1) There are 2 shaders which implement translucent subsurface scattering.
   "Translucent Subsurface Scattering/Point Light" used for point light.
   "Translucent Subsurface Scattering/Directional Light" used for directional light.
   Ready to use, you just need to apply it to your gameobjects.
2) The next step is send proper material parameters to shader.
   In demo scene, I use "MaterialControl.cs" help to do this.
3) Support thickness map based translucent.
   There is a realtime local thickness map rendering process in demo scene, fast but artifacts.
   You can use third-part tool to bake custom thickness map, set it to "CustomThicknessMap" slot it will preferred to be used.
   
When play the demo scene, you can press W,S,A,D,Q,E to move camera, hold left mouse button move to rotate camera.
Demo scene demonstrate all features, please refer it as example usage.
Notice you don't really need to put 4 sphere objects as point light source as demo scene does.
You only need set the position of point light (just a vector3) to material, as demo script "m_BeLightOfs[i].SetMaterialsVector (m_BeLightOfs[i].m_ID_TSSLitPos1, m_LightSources[0].transform.position);".

If you like it, please give us a good review on asset store. We will keep moving !
Any question, suggestion or requesting, please contact qq_d_y@163.com.
Hope we can help more and more unity3d developers.