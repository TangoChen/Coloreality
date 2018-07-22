using UnityEngine;
using System.Collections;
using Coloreality.LeapWrapper;

namespace Coloreality
{
    public static class LeapUnityUtil {

        public static Vector3 ToScaledVector3(this Vector vector, float scale = 0.001f){
            return new Vector3(vector.x, vector.y, vector.z) * scale;
        }

        public static Vector3 ToHMDVector3(this Vector vector, float scale = 0.001f){
            return new Vector3(-vector.x, vector.y, vector.z) * scale;
        }

        public static Vector3 ToVector3(this Vector vector){
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public static Quaternion ToQuaternion(this Vector vector){
            return Quaternion.Euler(vector.ToVector3());
        }

        public static Quaternion ToQuaternion(this LeapQuaternion leapQuaternion){
            return new Quaternion(leapQuaternion.x, leapQuaternion.y, leapQuaternion.z, leapQuaternion.w);
        }

        public static Quaternion ToHMDQuaternion(this LeapQuaternion leapQuaternion){
            Vector3 eulur = leapQuaternion.ToQuaternion().eulerAngles;
			return Quaternion.Euler(eulur.x, -eulur.y, -eulur.z);
        }

    }
}