using System;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

namespace CustomMath {

    public struct Vector3D {  //для проверки гита х2

        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }

        public Vector3D(float x1, float y1, float z1) {
            X = x1;
            Y = y1;
            Z = z1;
        }

        public void Set(float x1, float y1, float z1) {
            X = x1;
            Y = y1;
            Z = z1;
        }

        public static Vector3D Summation(Vector3D vectorA, Vector3D vectorB) {   //сложение векторов
            Vector3D vectorZ = new Vector3D(vectorA.X + vectorB.X, vectorA.Y + vectorB.Y, vectorA.Z + vectorB.Z);
            return vectorZ;
        }
        public static Vector3D Subtraction(Vector3D vectorA, Vector3D vectorB) {  //вычитание векторов
            Vector3D vectorZ = new Vector3D(vectorA.X - vectorB.X, vectorA.Y - vectorB.Y, vectorA.Z - vectorB.Z);
            return vectorZ;
        }

        public static Vector3D Scaling(Vector3D vectorA, float A) { //умножение вектора на скаляр  
            Vector3D vectorZ = new Vector3D(vectorA.X * A, vectorA.Y * A, vectorA.Z * A);
            return vectorZ;
        }


        public static Vector3D Normalized(Vector3D vector) { //нормализация
            float lengthVector = Length(vector);
            Vector3D vectorNormalized = new Vector3D(vector.X / lengthVector, vector.Y / lengthVector, vector.Z / lengthVector);
            return vectorNormalized;
        }

        public static Vector3D CrossProduct(Vector3D vectorA, Vector3D vectorB) { //векторное произведение - позвояет найти вектор перпендикулярный двум другим векторам(плоскости)
            Vector3D vectorZ = new Vector3D(vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y,      // (a.y*b.z - a.z*b.y)
                                            vectorA.Z * vectorB.X - vectorA.X * vectorB.Z,      // -(a.x*b.z - a.z*b.x)  (ВНАЧАЛЕ СТОИТ МИНУС - ПОЭТОМУ ЗНАЧЕНИЯ ИНВЕРТИРОВАНЫ)
                                            vectorA.X * vectorB.Y - vectorA.Y * vectorB.X);   // (a.x*b.y - a.y*b.x)
            return vectorZ;
        }

        public static Vector3D LinearTransformations(Vector3D vectorNormalI, Vector3D vectorNormalJ, Vector3D vectorNormalK, Vector3D vector) { //перевод вектора из мирового пространства в локальное        
            Vector3D vectorNewSpace = new Vector3D(vector.X * vectorNormalI.X + vector.Y * vectorNormalJ.X + vector.Z * vectorNormalK.X,
                                                   vector.X * vectorNormalI.Y + vector.Y * vectorNormalJ.Y + vector.Z * vectorNormalK.Y,
                                                   vector.X * vectorNormalI.Z + vector.Y * vectorNormalJ.Z + vector.Z * vectorNormalK.Z);
            return vectorNewSpace;
        }

        public static Vector3D RotationAroundPoint(Vector3D center, Vector3D vectorObject, float angle) { //перевод вектора из мирового пространства в локальное

            float x = vectorObject.X - center.X;
            float y = vectorObject.Y;
            float z = vectorObject.Z - center.Z;

            float xR = (float)(x * Math.Cos(angle) - z * Math.Sin(angle)) + center.X;
            Debug.Log("cos =" + Math.Cos(angle) + "  Sin = " + Math.Sin(angle));
            float yR = y;
            float zR = (float)(x * Math.Sin(angle) + z * Math.Cos(angle)) + center.Z;

            //xR = xR + center.X;

            //zR = zR + center.Z;


            Vector3D vectorNewRotation = new Vector3D(xR, yR, zR);

            return vectorNewRotation;
        }
        public enum СoordRotationAroundDirection { X, Y, Z };

        public static Vector3D RotationAroundPointСoordinate(Vector3D center, Vector3D vectorObject, float angle, СoordRotationAroundDirection coord) {
            Vector3D vectorNewRotation = vectorObject;
            float x;
            float y;
            float z;
            float xR;
            float yR;
            float zR;

            if (coord == СoordRotationAroundDirection.X) {
                x = vectorNewRotation.X;
                y = vectorNewRotation.Y - center.Y;
                z = vectorNewRotation.Z - center.Z;

                xR = x;
                yR = (float)(y * Math.Cos(angle) - z * Math.Sin(angle)) + center.Y;
                zR = (float)(y * Math.Sin(angle) + z * Math.Cos(angle)) + center.Z;

                vectorNewRotation = new Vector3D(xR, yR, zR);
            }

            if (coord == СoordRotationAroundDirection.Y) {
                x = vectorNewRotation.X - center.X;
                y = vectorNewRotation.Y;
                z = vectorNewRotation.Z - center.Z;

                xR = (float)(x * Math.Cos(angle) - z * Math.Sin(angle)) + center.X;
                yR = y;
                zR = (float)(x * Math.Sin(angle) + z * Math.Cos(angle)) + center.Z;

                vectorNewRotation = new Vector3D(xR, yR, zR);
            }


            if (coord == СoordRotationAroundDirection.Z) {
                x = vectorNewRotation.X - center.X;
                y = vectorNewRotation.Y - center.Y;
                z = vectorNewRotation.Z;

                xR = (float)(x * Math.Cos(angle) - y * Math.Sin(angle)) + center.X;
                yR = (float)(x * Math.Sin(angle) + y * Math.Cos(angle)) + center.Y;
                zR = z;
                vectorNewRotation = new Vector3D(xR, yR, zR);
            }

            return vectorNewRotation;
        }
        public static Vector3D RotationAroundPointСoordinate(Vector3D center, Vector3D vectorObject, float angle, bool xBool, bool yBool, bool zBool) { //TODO спросить про расширение 

            Vector3D vectorNewRotation = vectorObject;
            float x;
            float y;
            float z;
            float xR;
            float yR;
            float zR;
            if (xBool) {
                x = vectorNewRotation.X;
                y = vectorNewRotation.Y - center.Y;
                z = vectorNewRotation.Z - center.Z;

                xR = x;
                yR = (float)(y * Math.Cos(angle) - z * Math.Sin(angle)) + center.Y;
                zR = (float)(y * Math.Sin(angle) + z * Math.Cos(angle)) + center.Z;

                vectorNewRotation = new Vector3D(xR, yR, zR);
            }

            if (yBool) {
                x = vectorNewRotation.X - center.X;
                y = vectorNewRotation.Y;
                z = vectorNewRotation.Z - center.Z;

                xR = (float)(x * Math.Cos(angle) - z * Math.Sin(angle)) + center.X;
                yR = y;
                zR = (float)(x * Math.Sin(angle) + z * Math.Cos(angle)) + center.Z;

                vectorNewRotation = new Vector3D(xR, yR, zR);
            }


            if (zBool) {
                x = vectorNewRotation.X - center.X;
                y = vectorNewRotation.Y - center.Y;
                z = vectorNewRotation.Z;

                xR = (float)(x * Math.Cos(angle) - y * Math.Sin(angle)) + center.X;
                yR = (float)(x * Math.Sin(angle) + y * Math.Cos(angle)) + center.Y;
                zR = z;
                vectorNewRotation = new Vector3D(xR, yR, zR);
            }

            return vectorNewRotation;
        }

        public static Vector3D ReflectionFromThePlaneMirror(Vector3D vectorObject, Vector3D speed, Vector3D normal, float kElasticity) { //TODO НОРМАЛИЗОВАТЬЁЁ
            //  v' = v - 2 * (v ∙ n/n ∙ n) * n  - зеркало (отражение)                    
            float dot = vectorObject * normal;
            Vector3D vectorReflection = Scaling((vectorObject - Scaling(normal, 2 * dot)), kElasticity);
            return vectorReflection;
        }

        public static Vector3D ReflectionFromThePlaneGlass(Vector3D vectorObject, Vector3D speed, Vector3D normal, float kElasticity) {
            //v'=v - 2v*n   рикошет (отражение от поверхности)                     
            Vector3D multiplicationVectors = new Vector3D(vectorObject.X * normal.X,
                                                          vectorObject.Y * normal.Y,
                                                          vectorObject.Z * normal.Z);

            Vector3D vectorReflectionGlass = vectorObject - Scaling(vectorObject - multiplicationVectors, 2);
            vectorReflectionGlass = Scaling(vectorReflectionGlass, kElasticity);
            return vectorReflectionGlass;
        }



        // x(t) = Vx * t * cos(angleLaunch)
        // y(t) = Vy * t * sin(angleLaunch) - 0.5 * g * t * t
        // z(t) = Vz * t - 0.5 * g * t * t
        public static List<Vector3D> Ballistics(Vector3D vectorObject, Vector3D speed, float angleLaunchDegrees, float g, float timeSecond) {//todo
            float timeInterval = 0.1f;
            float t = 0f;
            float x;
            float y;
            float z = vectorObject.Z;
            Vector3D Ballistics = new Vector3D();
            List<Vector3D> BallisticsList = new List<Vector3D>();
            while (t < timeSecond) {
                x = (float)(vectorObject.X + speed.X * t * Math.Cos(angleLaunchDegrees));
                y = (float)(vectorObject.Y + speed.Y * t * Math.Sin(angleLaunchDegrees) - 0.5 * g * t * t);
                z = (float)(vectorObject.Z + speed.Z * t * Math.Cos(angleLaunchDegrees));
                //x = (float)(vectorObject.X * t * Math.Cos(angleLaunchDegrees));
                //y = (float)(vectorObject.Y * t * Math.Sin(angleLaunchDegrees) - 0.5 * g * t * t);

                //  z = (float)(vectorObject.Z * t - 0.5 * g * t * t);
                Ballistics.Set(x, y, z);
                BallisticsList.Add(Ballistics);
                if (y <= 0) break;
                t += timeInterval;
            }
            return BallisticsList;
        }


        public static float ScalingVector(Vector3D vectorA, Vector3D vectorB) { //скалярное умножение двух векторов     //ax × bx + ay * by + az * bz /// нужно для определения параллельности или перпендиккулярности 
            float scalingVecotor = vectorA.X * vectorB.X + vectorA.Y * vectorB.Y + vectorA.Z * vectorB.Z;
            return scalingVecotor;
        }
        /*
        public static float ScalingVector3Dcos(Vector3D vectorA, Vector3D vectorB) { //скалярное умножение через косинус - не нужно   //|a| × |b| × cos(θ)    
            float scalingVecotor = Length(vectorA) * Length(vectorB) * CosVector3D(vectorA, vectorB);
            return scalingVecotor;
        }
        */

        public static float CosVector3D(Vector3D vectorA, Vector3D vectorB) { //косинус между векторами      // cos(θ) = (ax × bx + ay * by + az * bz) / |a| × |b|
            float cosin = ScalingVector(vectorA, vectorB) / (Length(vectorA) * Length(vectorB));
            return cosin;
        }

        public static float AngleBetweenVectorsDegrees(Vector3D vectorA, Vector3D vectorB) {
            return Mathf.Acos(CosVector3D(vectorA, vectorB)) * Mathf.Rad2Deg;
        }

        public static float AngleBetweenVectorsRadians(Vector3D vectorA, Vector3D vectorB) {
            return Mathf.Acos(CosVector3D(vectorA, vectorB));
        }

        public static float Length(Vector3D vector) { // длина  вектора     
            float lengthVector = Mathf.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            return lengthVector;
        }

        public override string ToString() {
            return $"{X} {Y} {Z}\n";
        }

        public static Vector3 ConversionVector3DInVector3(Vector3D vector3D) {
            Vector3 Vector3new = new Vector3(vector3D.X, vector3D.Y, vector3D.Z);
            return Vector3new;
        }

        public static Vector3D ConversionVector3InVector3D(Vector3 vector3) {
            Vector3D Vector3Dnew = new Vector3D(vector3.x, vector3.y, vector3.z);
            return Vector3Dnew;
        }


        public static Vector3D operator +(Vector3D v1, Vector3D v2) {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector3D operator -(Vector3D v1, Vector3D v2) {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        //public static Vector3D operator *(Vector3D vectorA, Vector3D vectorB)
        //{
        //    Vector3D vectorZ = new Vector3D(vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y,      // (a.y*b.z - a.z*b.y)
        //                                    vectorA.Z * vectorB.X - vectorA.X * vectorB.Z,      // -(a.x*b.z - a.z*b.x)  (ВНАЧАЛЕ СТОИТ МИНУС - ПОЭТОМУ ЗНАЧЕНИЯ ИНВЕРТИРОВАНЫ)
        //                                    vectorA.X * vectorB.Y - vectorA.Y * vectorB.X);   // (a.x*b.y - a.y*b.x)
        //    return vectorZ;
        //}

        public static float operator *(Vector3D vectorA, Vector3D vectorB) {
            float scalingVecotor = vectorA.X * vectorB.X + vectorA.Y * vectorB.Y + vectorA.Z * vectorB.Z;
            return scalingVecotor;
        }


        public static bool operator ==(Vector3D vectorA, Vector3D vectorB) { //скалярное умножение двух векторов     //ax × bx + ay * by + az * bz /// нужно для определения параллельности или перпендиккулярности 
            if (vectorA.X == vectorB.X && vectorA.Y == vectorB.Y && vectorA.Z == vectorB.Z) return true;
            return false;
        }
        public static bool operator !=(Vector3D vectorA, Vector3D vectorB) { //скалярное умножение двух векторов     //ax × bx + ay * by + az * bz /// нужно для определения параллельности или перпендиккулярности 
            if (vectorA.X != vectorB.X || vectorA.Y != vectorB.Y || vectorA.Z != vectorB.Z) return true;
            return false;
        }

    }




}
