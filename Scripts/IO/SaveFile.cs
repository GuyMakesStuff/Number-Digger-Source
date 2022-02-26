using System;
using System.Collections.Generic;
using UnityEngine;

namespace NumberDigger.IO
{
    [Serializable]
    public class SaveFile
    {
        public string FileName;
        public void Save()
        {
            Saver.Save(this);
        }
    }
}