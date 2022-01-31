using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealtimeRendering.Examples
{
    public class SavingTest : MonoBehaviour
    {
        void Start()
        {
            var data = new Data()
            {
                name = "Bob",
                value = 1
            };

            Save.SaveToJson(data, "Bob", Save.SavePath.StreamingAssets, true);
            Save.SaveToJson(data, "Bob", Save.SavePath.AssetsFolder, true);

            Save.SaveToBytes(data, "Byte Bob", Save.SavePath.StreamingAssets, true);
            Save.SaveToBytes(data, "Byte Bob", Save.SavePath.AssetsFolder, true);
        }
    }

    [System.Serializable]
    public class Data
    {
        public string name;
        public int value;
    }
}
