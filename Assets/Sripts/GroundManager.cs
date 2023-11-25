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
                return groundtype;
                break;
            case Groundtype.Lake:
                return Groundtype.FrostedLake;
                break;
            case Groundtype.Soaked:
                return Groundtype.Ice;
                break;
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
                return true;
                break;
            case Groundtype.Hole:
            case Groundtype.Lake:
                return false;
                break;

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
                return false;
            case Groundtype.Ice:
            case Groundtype.FrostedLake:
                return true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(groundtype), groundtype, null);
        }
    }
}
