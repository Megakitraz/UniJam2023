using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    private GlowHighlight glowHighlight;

    private void Awake()
    {
        glowHighlight = GetComponent<GlowHighlight>();
    }

    public void Select()
    {
        glowHighlight.ToggleGlow1(true);
    }

    public void Deselect()
    {
        glowHighlight.ToggleGlow1(false);
    }
}
