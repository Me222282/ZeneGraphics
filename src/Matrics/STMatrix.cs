using System;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// A scale then translate matrix.
    /// </summary>
    public class STMatrix : IMatrix
    {
        public STMatrix() { }
        public STMatrix(Vector3 scale, Vector3 translate)
        {
            Scale = scale;
            Translate = translate;
        }
        public STMatrix(Vector2 scale, Vector2 translate)
        {
            Scale = new Vector3(scale, 1d);
            TXY = translate;
        }

        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => false;
        
        private Vector3 _scale;
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
            }
        }
        private Vector3 _translate;
        public Vector3 Translate
        {
            get => _translate;
            set
            {
                _translate = value;
            }
        }
        public Vector2 SXY
        {
            get => (Vector2)_scale;
            set
            {
                _scale.X = value.X;
                _scale.Y = value.Y;
            }
        }
        public Vector2 TXY
        {
            get => (Vector2)_translate;
            set
            {
                _translate.X = value.X;
                _translate.Y = value.Y;
            }
        }
        public double SX
        {
            get => _scale.X;
            set
            {
                _scale.X = value;
            }
        }
        public double SY
        {
            get => _scale.Y;
            set
            {
                _scale.Y = value;
            }
        }
        public double SZ
        {
            get => _scale.Z;
            set
            {
                _scale.Z = value;
            }
        }
        public double TX
        {
            get => _translate.X;
            set
            {
                _translate.X = value;
            }
        }
        public double TY
        {
            get => _translate.Y;
            set
            {
                _translate.Y = value;
            }
        }
        public double TZ
        {
            get => _translate.Z;
            set
            {
                _translate.Z = value;
            }
        }

        public void MatrixData(MatrixSpan ms)
        {
            // 4x4 only
            if (ms.Rows != 4 || ms.Columns != 4)
            {
                ms.Padding(0, 0);
                return;
            }
            
            ms.Data[0] = _scale.X;
            ms.Data[5] = _scale.Y;
            ms.Data[10] = _scale.Z;
            ms.Data[12] = _translate.X;
            ms.Data[13] = _translate.Y;
            ms.Data[14] = _translate.Z;
            ms.Data[15] = 1d;
        }
    }
}
