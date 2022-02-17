using System;

namespace Zene.Graphics
{
    public abstract class GLObject : IGLObject
    {
        protected bool _dataCreated;
        public bool DataCreated
        {
            get
            {
                return _dataCreated;
            }
            set
            {
                if (value && (!_dataCreated))
                {
                    CreateData();
                }
                else if ((!value) && _dataCreated)
                {
                    DeleteData();
                }
            }
        }

        protected bool _bound;
        public bool Bound
        {
            get
            {
                return _bound;
            }
            set
            {
                if (value && (!_bound))
                {
                    Bind();
                }
                else if ((!value) && _bound)
                {
                    UnBind();
                }
            }
        }

        public abstract void CreateData();

        public abstract void DeleteData();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                DeleteData();
            }
        }

        public abstract void Bind();

        public abstract void UnBind();
    }
}
