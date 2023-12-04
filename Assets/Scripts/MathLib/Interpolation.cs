using CustomMath;
using System.Collections.Generic;
using UnityEngine;

public class Interpolation {

    public static float Lerp3D(float a, float b, float t) {
        return a + (b - a) * t;
    }
    public static float LerpInverse3D(float a, float b, float c) {
        if (b - a == 0) return c;
        return (c - a) / (b - a);
    }

    public static float Remap3D(float a1, float a2, float b1, float b2, float x) {
        return Lerp3D(b1, b2, LerpInverse3D(a1, a2, x));
    }

    public static float SLerp3D(float a, float b, float t, float angle) {
        float leftPart = (Mathf.Sin((1 - t) * angle) / Mathf.Sin(angle)) * a;
        float rightPart = (Mathf.Sin(t * angle) / Mathf.Sin(angle)) * b;
        return leftPart + rightPart;
    }

    public static Vector3D Lerp3D(Vector3D vectorA, Vector3D vectorB, float t) {
        return new Vector3D(Lerp3D(vectorA.X, vectorB.X, t),
                           Lerp3D(vectorA.Y, vectorB.Y, t),
                           Lerp3D(vectorA.Z, vectorB.Z, t));
    }
    public static float LerpInverse3D(Vector3D vectorA, Vector3D vectorB, Vector3D vectorC) {
        return LerpInverse3D(vectorA.X, vectorB.X, vectorC.X);
    }

    public static Vector3D Remap3D(Vector3D vectorA1, Vector3D vectorA2, Vector3D vectorB1, Vector3D vectorB2, Vector3D XA) {
        float x = Remap3D(vectorA1.X, vectorA2.X, vectorB1.X, vectorB2.X, XA.X);
        float y = Remap3D(vectorA1.Y, vectorA2.Y, vectorB1.Y, vectorB2.Y, XA.Y);
        float z = Remap3D(vectorA1.Z, vectorA2.Z, vectorB1.Z, vectorB2.Z, XA.Z);
        return new Vector3D(x, y, z);
    }


    public static Vector3D SLerp3D(Vector3D vectorA, Vector3D vectorB, float t) {
        float angle = Vector3D.AngleBetweenVectorsDegrees(vectorA, vectorB);
        return new Vector3D(SLerp3D(vectorA.X, vectorB.X, t, angle),
                           SLerp3D(vectorA.Y, vectorB.Y, t, angle),
                           SLerp3D(vectorA.Z, vectorB.Z, t, angle));
    }


    public static List<Vector3D> SLerpList3D(Vector3D vectorA, Vector3D vectorB, float t) {
        float angle = Vector3D.AngleBetweenVectorsDegrees(vectorA, vectorB);
        float i = 0f;
        float timeSecond = 1;
        List<Vector3D> sLerp3DList = new List<Vector3D>();
        while (i < timeSecond) {
            t = i;
            sLerp3DList.Add(new Vector3D(SLerp3D(vectorA.X, vectorB.X, t, angle),
                           SLerp3D(vectorA.Y, vectorB.Y, t, angle),
                           SLerp3D(vectorA.Z, vectorB.Z, t, angle)));
            i += 0.03f;
        }

        return sLerp3DList;
    }

    public static Quaternion SLerpQuaternion3D(Quaternion quaternionA, Quaternion quaternionB, float t) {
        return Quaternion.Slerp(quaternionA, quaternionB, t);
    }



}
