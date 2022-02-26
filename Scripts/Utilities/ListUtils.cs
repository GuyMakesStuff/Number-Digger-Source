using System;
using System.Collections.Generic;
using UnityEngine;

namespace NumberDigger.Utils
{
    public static class ListUtils
    {
        public static void CloneList<T>(List<T> Target, List<T> WhatToClone)
        {
            if(Target != null)
            {
                Target.Clear();
            }
            else
            {
                Target = new List<T>();
            }

            for (int L = 0; L < WhatToClone.Count; L++)
            {
                Target.Add(WhatToClone[L]);
            }
        }

        public static List<T> RandomItemsFromList<T>(List<T> Items, int Amount)
        {
            List<T> Result = new List<T>();
            for (int I = 0; I < Amount; I++)
            {
                int Index = UnityEngine.Random.Range(0, Amount + 1);
                Result.Add(Items[Index]);
                Items.RemoveAt(Index);
            }

            return Result;
        }
    }
}