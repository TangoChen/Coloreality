using System;

namespace Coloreality
{
    public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);

    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }

}
