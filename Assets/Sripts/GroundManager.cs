using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Groundtype
{
    Normal,
    Hole,
    Ice,
    Lake,
    Soaked,
    FrostedLake,
    Portal,
}
public class GroundManager
{


    public static Groundtype ApplyHeat(Groundtype groundtype)
    {
        switch (groundtype)
        {
            case Groundtype.Ice:
                return Groundtype.Soaked;
            case Groundtype.FrostedLake:
                return Groundtype.Lake;
            case Groundtype.Normal:
            case Groundtype.Hole:
            case Groundtype.Lake:
            case Groundtype.Soaked:
            case Groundtype.Portal:    
                return groundtype;
            default:
                throw new ArgumentOutOfRangeException(nameof(groundtype), groundtype, null);
        }
    }

    public static Groundtype ApplyCold(Groundtype groundtype)
    {
        switch (groundtype)
        {
            case Groundtype.Normal:
            case Groundtype.Hole:
            case Groundtype.Ice:
            case Groundtype.FrostedLake:
            case Groundtype.Portal:
                return groundtype;
            case Groundtype.Lake:
                return Groundtype.FrostedLake;
            case Groundtype.Soaked:
                return Groundtype.Ice;
            default:
                throw new ArgumentOutOfRangeException(nameof(groundtype), groundtype, null);
        }
    }

    public static bool IsReachable(Groundtype groundtype)
    {
        switch (groundtype)
        {
            case Groundtype.Normal:
            case Groundtype.Soaked:
            case Groundtype.FrostedLake:
            case Groundtype.Ice:
            case Groundtype.Portal:    
                return true;
            case Groundtype.Hole:
            case Groundtype.Lake:
                return false;

            default:
                throw new ArgumentOutOfRangeException(nameof(groundtype), groundtype, null);
        }
    }


    public static bool IsSlippery(Groundtype groundtype)
    {
        switch (groundtype)
        {
            case Groundtype.Normal:
            case Groundtype.Hole:
            case Groundtype.Lake:
            case Groundtype.Soaked:
            case Groundtype.Portal:    
                return false;
            case Groundtype.Ice:
            case Groundtype.FrostedLake:
                return true;
            default:
                throw new ArgumentOutOfRangeException(nameof(groundtype), groundtype, null);
        }
    }
}
