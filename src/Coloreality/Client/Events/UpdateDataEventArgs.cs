using System;

namespace Coloreality.Client
{
    public delegate void UpdateDataEventHandler<T>(object sender, UpdateDataEventArgs<T> e);

    public class UpdateDataEventArgs<T> : EventArgs
    {
        public T Data { get; private set; }
        public UpdateDataEventArgs(T data)
        {
            Data = data;
        }
    }
}