namespace Coloreality.LeapWrapper.Sender
{
    public static class LeapUtil{
        public static Vector ToSerialiableVector(this Leap.Vector vector)
        {
            return new Vector(vector.x, vector.y, vector.z);
        }

        public static LeapQuaternion ToSerialiableQuaternion(this Leap.LeapQuaternion leapQuaternion)
        {
            return new LeapQuaternion(leapQuaternion.x, leapQuaternion.y, leapQuaternion.z, leapQuaternion.w);
        }
    }
}