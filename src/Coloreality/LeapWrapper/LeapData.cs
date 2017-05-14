using System;
using System.Collections.Generic;

namespace Coloreality.LeapWrapper
{

    [Serializable]
    public class LeapData
    {
        [NonSerialized]
        public const int DataIndex = 35;

        public LeapFrame frame = null;

        public LeapData() { }
        
    }

    [Serializable]
    public class LeapFrame
    {
        public long Id = -1;
        public List<LeapHand> Hands = new List<LeapHand>();

        public LeapFrame() { }
    }

    [Serializable]
    public class LeapHand
    {
        public int Id;
        public bool IsLeft;
        public bool IsRight { get { return !IsLeft; } }
        public float Confidence;
        public float TimeVisible;
        public float GrabStrength;
        public float GrabAngle;
        public float PinchStrength;
        public float PinchDistance;
        public float PalmWidth;
        public Vector PalmPosition;
        public Vector PalmVelocity;
        public Vector PalmNormal;
        public Vector Direction;
        public Vector WristPosition;
        //public Vector WristPosition
        //{
        //    get { return Arm == null ? Vector.Zero : Arm.Wrist; }
        //}

        //public Vector StabilizedPalmPosition;

        public LeapQuaternion Rotation;
        public LeapArm Arm = null;
        public List<LeapFinger> Fingers = new List<LeapFinger>();

        public LeapHand() { }
    }

    [Serializable]
    public class LeapArm
    {
        public float Length;
        public float Width;
        public Vector Elbow;
        public Vector Wrist;
        public Vector Center;
        public Vector Direction;
        public LeapQuaternion Rotation;

        public LeapArm() { }
    }

    [Serializable]
    public class LeapFinger
    {
        public int Id;
        public bool IsExtended;
        public float TimeVisible;
        public float Width;
        public float Length;
        public Vector TipPosition;
        public Vector Direction;
        public Vector TipVelocity;
        //public Vector StabilizedTipVelocity;

        public LeapBone[] bones = null;
        public LeapFinger() { }
    }

    [Serializable]
    public class LeapBone
    {
        public float Length;
        public float Width;
        public Vector Center;
        public Vector Direction;
        public LeapQuaternion Rotation;

        public LeapBone() { }
    }


    [Serializable]
    public struct Vector
    {
        public float x, y, z;
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [NonSerialized]
        public static Vector Zero = new Vector(0, 0, 0);
    }

    [Serializable]
    public struct LeapQuaternion
    {
        public float x, y, z, w;
        public LeapQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }
}
