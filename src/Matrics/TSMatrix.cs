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
        }
        public TSMatrix(Vector2 translate, Vector2 scale)
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
                _data[0] = value.X;
                _data[5] = value.Y;
                _data[10] = value.Z;
            }
        }
        private Vector3 _translate;
        public Vector3 Translate
        {
            get => _translate;
            set
            {
                _translate = value;
                _data[12] = value.X * _scale.X;
                _data[13] = value.Y * _scale.Y;
                _data[14] = value.Z * _scale.Z;
            }
        }
        public Vector2 SXY
        {
            get => (Vector2)_scale;
            set
            {
                _scale.X = value.X;
                _scale.Y = value.Y;
                _data[0] = value.X;
                _data[5] = value.Y;
            }
        }
        public Vector2 TXY
        {
            get => (Vector2)_translate;
            set
            {
                _translate.X = value.X;
                _translate.Y = value.Y;
                _data[12] = value.X * _scale.X;
                _data[13] = value.Y * _scale.Y;
            }
        }
        public double SX
        {
            get => _scale.X;
            set
            {
                _scale.X = value;
                _data[0] = value;
            }
        }
        public double SY
        {
            get => _scale.Y;
            set
            {
                _scale.Y = value;
                _data[5] = value;
            }
        }
        public double SZ
        {
            get => _scale.Z;
            set
            {
                _scale.Z = value;
                _data[10] = value;
            }
        }
        public double TX
        {
            get => _translate.X;
            set
            {
                _translate.X = value;
                _data[12] = value * _scale.X;
            }
        }
        public double TY
        {
            get => _translate.Y;
            set
            {
                _translate.Y = value;
                _data[13] = value * _scale.Y;
            }
        }
        public double TZ
        {
            get => _translate.Z;
            set
            {
                _translate.Z = value;
                _data[14] = value * _scale.Z;
            }
        }
        
        private double[] _data = new double[16];

        public MatrixSpan MatrixData() => new MatrixSpan(4, 4, _data);
    }
}
