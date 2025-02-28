using System;
using Unity.Mathematics;
using UnityEngine;

namespace Ceramic3dTest
{
    [Serializable]
    public class DataConverter
    {
        public MatrixData[] Datas;

        public Matrix4x4[] GetMatrix4X4s()
        {
            Matrix4x4[] matrix4X4s = new Matrix4x4[Datas.Length];

            for (int i = 0; i < Datas.Length; i++)
            {
                matrix4X4s[i] = Datas[i].Matrix4x4;
            }

            return matrix4X4s;
        }

        public void SetMatrix4X4s(Matrix4x4[] matrix4X4s)
        {
            Datas = new MatrixData[matrix4X4s.Length];

            for (int i = 0; i < matrix4X4s.Length; i++)
            {
                Datas[i].Matrix4x4 = matrix4X4s[i];
            }
        }



        /// <summary>
        /// For some reason it is not possible to serialize directly into a Matrix4x4
        /// </summary>
        [Serializable]
        public struct MatrixData
        {
            public float m00, m10, m20, m30, m01, m11, m21, m31, m02,
                         m12, m22, m32, m03, m13, m23, m33;


            public Matrix4x4 Matrix4x4
            {
                get
                {
                    Matrix4x4 matrix4X4 = new Matrix4x4();

                    matrix4X4.m00 = m00;
                    matrix4X4.m10 = m10;
                    matrix4X4.m20 = m20;
                    matrix4X4.m30 = m30;

                    matrix4X4.m01 = m01;
                    matrix4X4.m11 = m11;
                    matrix4X4.m21 = m21;
                    matrix4X4.m31 = m31;

                    matrix4X4.m02 = m02;
                    matrix4X4.m12 = m12;
                    matrix4X4.m22 = m22;
                    matrix4X4.m32 = m32;

                    matrix4X4.m03 = m03;
                    matrix4X4.m13 = m13;
                    matrix4X4.m23 = m23;
                    matrix4X4.m33 = m33;

                    return matrix4X4;
                }

                set
                {
                    m00 = value.m00;
                    m10 = value.m10;
                    m20 = value.m20;
                    m30 = value.m30;

                    m01 = value.m01;
                    m11 = value.m11;
                    m21 = value.m21;
                    m31 = value.m31;

                    m02 = value.m02;
                    m12 = value.m12;
                    m22 = value.m22;
                    m32 = value.m32;

                    m03 = value.m03;
                    m13 = value.m13;
                    m23 = value.m23;
                    m33 = value.m33;
                }
            }

            public Matrix4x4 SeMatrix4x4
            {
                get
                {
                    Matrix4x4 matrix4X4 = new Matrix4x4();

                    matrix4X4.m00 = m00;
                    matrix4X4.m10 = m10;
                    matrix4X4.m20 = m20;
                    matrix4X4.m30 = m30;

                    matrix4X4.m01 = m01;
                    matrix4X4.m11 = m11;
                    matrix4X4.m21 = m21;
                    matrix4X4.m31 = m31;

                    matrix4X4.m02 = m02;
                    matrix4X4.m12 = m12;
                    matrix4X4.m22 = m22;
                    matrix4X4.m32 = m32;

                    matrix4X4.m03 = m03;
                    matrix4X4.m13 = m13;
                    matrix4X4.m23 = m23;
                    matrix4X4.m33 = m33;

                    return matrix4X4;
                }
            }
        }
    }
}