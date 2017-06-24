using System;

namespace Coloreality.LeapWrapper
{
    [Serializable]
    public class LeapHmdConfig
    {
        [NonSerialized]
        public const int DataIndex = 36;

        public float OffsetX = 0;
        public float OffsetY = 0;
        public float OffsetZ = 0;

        public float Scale = 1;

        public LeapHmdConfig() { }

        public LeapHmdConfig(float x, float y, float z, float scale = 1) {
            OffsetX = x;
            OffsetY = y;
            OffsetZ = z;
            Scale = scale;
        }
    }
}