using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wallet", menuName = "Wallet")]
public class Wallet : ScriptableObject
{
    public int nuts;
    public int bolts;
    public int cogs;


    public static bool operator >(Wallet a, Wallet b)
    {
        if (a.nuts>b.nuts&&a.bolts>b.bolts&&a.cogs>b.cogs)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool operator >=(Wallet a, Wallet b)
    {
        if (a.nuts >= b.nuts && a.bolts >= b.bolts && a.cogs >= b.cogs)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool operator <(Wallet a, Wallet b)
    {
        if (a.nuts < b.nuts && a.bolts < b.bolts && a.cogs < b.cogs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator <=(Wallet a, Wallet b)
    {
        if (a.nuts <= b.nuts && a.bolts <= b.bolts && a.cogs <= b.cogs)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Wallet operator -(Wallet a, Wallet b)
    {
        Wallet ret = new Wallet();
        ret.bolts =a.bolts - b.bolts;
        ret.cogs = a.cogs - b.cogs;
        ret.nuts = a.nuts - b.nuts;

        return ret;
    }
}
