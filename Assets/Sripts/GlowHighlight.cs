using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowHighlight : MonoBehaviour
{
    Dictionary<Renderer,Material[]> glowMaterial1Dictionary = new Dictionary<Renderer,Material[]>();
    Dictionary<Renderer,Material[]> glowMaterial2Dictionary = new Dictionary<Renderer,Material[]>();
    Dictionary<Renderer,Material[]> glowMaterial3Dictionary = new Dictionary<Renderer,Material[]>();
    Dictionary<Renderer,Material[]> originalMaterialDictionary = new Dictionary<Renderer,Material[]>();

    Dictionary<Color,Material> cachedGlowMaterial1 = new Dictionary<Color,Material>();
    Dictionary<Color,Material> cachedGlowMaterial2 = new Dictionary<Color,Material>();
    Dictionary<Color,Material> cachedGlowMaterial3 = new Dictionary<Color,Material>();


    public Material glowMaterial1;
    public Material glowMaterial2;
    public Material glowMaterial3;

    private bool isGlowing1 = false;
    private bool isGlowing2 = false;
    private bool isGlowing3 = false;


    private void Awake()
    {
        PrepareMaterialDictionaries();

    }

    private void PrepareMaterialDictionaries()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] originalMaterials = renderer.materials;
            originalMaterialDictionary.Add(renderer, originalMaterials);
            Material[] newMaterials1 = new Material[renderer.materials.Length];
            Material[] newMaterials2 = new Material[renderer.materials.Length];
            Material[] newMaterials3 = new Material[renderer.materials.Length];
            for (int i = 0; i <originalMaterials.Length; i++)
            {
                Material mat1 = null;
                if (cachedGlowMaterial1.TryGetValue(originalMaterials[i].color, out mat1) == false)
                {
                    mat1 = new Material(glowMaterial1);
                    mat1.color = originalMaterials[i].color;
                }
                newMaterials1[i] = mat1;

                Material mat2 = null;
                if (cachedGlowMaterial2.TryGetValue(originalMaterials[i].color, out mat2) == false)
                {
                    mat2 = new Material(glowMaterial2);
                    mat2.color = originalMaterials[i].color;
                }
                newMaterials2[i] = mat2;

                Material mat3 = null;
                if (cachedGlowMaterial3.TryGetValue(originalMaterials[i].color, out mat3) == false)
                {
                    mat3 = new Material(glowMaterial3);
                    mat3.color = originalMaterials[i].color;
                }
                newMaterials3[i] = mat3;
            }
            glowMaterial1Dictionary.Add(renderer, newMaterials1);
            glowMaterial2Dictionary.Add(renderer, newMaterials2);
            glowMaterial3Dictionary.Add(renderer, newMaterials3);
        }
    }

    public void ToggleGlow1()
    {
        if (isGlowing1 == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterial1Dictionary[renderer];
            }
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing1 = !isGlowing1;
    }

    public void ToggleGlow1(bool state)
    {
        if (isGlowing1 == state)
        {
            return;
        }
        isGlowing1 = !state;
        ToggleGlow1();
    }

    public void ToggleGlow2()
    {
        if (isGlowing2 == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterial2Dictionary[renderer];
            }
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing2 = !isGlowing2;
    }

    public void ToggleGlow2(bool state)
    {
        if (isGlowing2 == state)
        {
            return;
        }
        isGlowing2 = !state;
        ToggleGlow2();
    }

    public void ToggleGlow3()
    {
        if (isGlowing3 == false)
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = glowMaterial3Dictionary[renderer];
            }
        }
        else
        {
            foreach (Renderer renderer in originalMaterialDictionary.Keys)
            {
                renderer.materials = originalMaterialDictionary[renderer];
            }
        }
        isGlowing3 = !isGlowing3;
    }

    public void ToggleGlow3(bool state)
    {
        if (isGlowing3 == state)
        {
            return;
        }
        isGlowing3 = !state;
        ToggleGlow3();
    }
}
