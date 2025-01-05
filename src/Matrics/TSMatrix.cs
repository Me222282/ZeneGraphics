using System;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// A translate then scale matrix.
    /// </summary>
    public class TSMatrix : IMatrix
    {
        public TSMatrix() { }
        public TSMatrix(Vector3 translate, Vector3 scale)
        {
            Scale = scale;
            Translate = translate;
            _data[15] = 1d;
        }
        public TSMatrix(Vector2 translate, Vector2 scale)
        {
            Scale = new Vector3(scale, 1d);
            TXY = translate;
            _data[15] = 1d;
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
                _ts = _translate * value;
            }
        }
        private Vector3 _translate;
        private Vector3 _ts;
        public Vector3 Translate
        {
            get => _translate;
            set
            {
                _translate = value;
                _ts = value * _scale;
            }
        }
        public Vector2 SXY
        {
            get => (Vector2)_scale;
            set
            {
                _scale.X = value.X;
                _scale.Y = value.Y;
                _ts.X = _translate.X * value.X;
                _ts.Y = _translate.Y * value.Y;
            }
        }
        public Vector2 TXY
        {
            get => (Vector2)_translate;
            set
            {
                _translate.X = value.X;
                _translate.Y = value.Y;
                _ts.X = value.X * _scale.X;
                _ts.Y = value.Y * _scale.Y;
            }
        }
        public double SX
        {
            get => _scale.X;
            set
            {
                _scale.X = value;
                _ts.X = _translate.X * value;
            }
        }
        public double SY
        {
            get => _scale.Y;
            set
            {
                _scale.Y = value;
                _ts.Y = _translate.Y * value;
            }
        }
        public double SZ
        {
            get => _scale.Z;
            set
            {
                _scale.Z = value;
                _ts.Z = _translate.Z * value;
            }
        }
        public double TX
        {
            get => _translate.X;
            set
            {
                _translate.X = value;
                _ts.X = value * _scale.X;
            }
        }
        public double TY
        {
            get => _translate.Y;
            set
            {
                _translate.Y = value;
                _ts.Y = value * _scale.Y;
            }
        }
        public double TZ
        {
            get => _translate.Z;
            set
            {
                _translate.Z = value;
                _ts.Z = value * _scale.Z;
            }
        }
        
        private double[] _data = new double[16];

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
            ms.Data[12] = _ts.X;
            ms.Data[13] = _ts.Y;
            ms.Data[14] = _ts.Z;
            ms.Data[15] = 1d;
        }
    }
}
