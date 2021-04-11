using UnityEngine;

namespace Util
{
    public static class Vector3Extension
    {
        public static Vector3 MultiplyComponents(this Vector3 lhs, Vector3 rhs) =>
            new Vector3(
                lhs.x * rhs.x,
                lhs.y * rhs.y,
                lhs.z * rhs.z
            );
    }
}