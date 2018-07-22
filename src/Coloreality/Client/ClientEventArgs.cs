using System;

namespace Coloreality.Client
{
    public class UpdateDataEventArgs<T> : EventArgs
    {
        public T Data { get; private set; }
        public UpdateDataEventArgs(T data)
        {
            Data = data;
        }
    }
}