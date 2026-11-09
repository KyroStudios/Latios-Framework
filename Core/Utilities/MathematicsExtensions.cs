
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Latios
{
    /// <summary>
    /// Additional mathematics operations used by Latios Framework.
    /// </summary>
    public static class MathematicsExtensions
    {
        /// <summary>
        /// Returns the vector transformed by the inverse of a normalized rotation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 InverseRotateFast(quaternion normalizedRotation, float3 vector)
        {
            return math.rotate(math.conjugate(normalizedRotation), vector);
        }

        /// <summary>
        /// Returns the second quaternion rotated by the inverse of the first normalized quaternion.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion InverseRotateFast(quaternion normalizedRotation, quaternion rotationToBeRotated)
        {
            return math.mul(math.conjugate(normalizedRotation), rotationToBeRotated);
        }

        /// <summary>
        /// Returns the point transformed by the inverse of a RigidTransform.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 InverseTransformFast(in RigidTransform rigidTransform, float3 point)
        {
            return InverseRotateFast(rigidTransform.rot, point - rigidTransform.pos);
        }

        /// <summary>
        /// Returns the second RigidTransform transformed by the inverse of the first.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RigidTransform InverseTransformFast(in RigidTransform a, in RigidTransform b)
        {
            var inverseRot = math.conjugate(a.rot);

            return new RigidTransform(
                math.mul(inverseRot, b.rot),
                math.rotate(inverseRot, b.pos - a.pos)
            );
        }
    }

    /// <summary>
    /// Additional float3x4 matrix construction operations.
    /// </summary>
    public static class Float3x4Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 TRS(float3 translation, quaternion rotation, float3 scale)
        {
            float3x3 r = new float3x3(rotation);

            return new float3x4(
                r.c0 * scale.x,
                r.c1 * scale.y,
                r.c2 * scale.z,
                translation
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3x4 Scale(float3 scale)
        {
            return new float3x4(
                new float3(scale.x, 0f, 0f),
                new float3(0f, scale.y, 0f),
                new float3(0f, 0f, scale.z),
                float3.zero
            );
        }

        public static readonly float3x4 identity = new float3x4(
            new float3(1f, 0f, 0f),
            new float3(0f, 1f, 0f),
            new float3(0f, 0f, 1f),
            float3.zero
        );
    }
}
