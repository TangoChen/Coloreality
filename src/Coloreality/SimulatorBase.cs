using Coloreality.Client;

namespace Coloreality
{
    public class SimulatorBase<T>
    {
        public event UpdateDataEventHandler<T> OnUpdatedData;

        public readonly int DataIndex;
        public T Data { get; private set; }

        public SimulatorBase(int dataIndex)
        {
            DataIndex = dataIndex;
        }

        public void AddSource(SocketClient source)
        {
            if (source.OnReceiveDataCollection.ContainsKey(DataIndex))
            {
                source.OnReceiveDataCollection[DataIndex] += UpdateData;
            }
            else
            {
                source.OnReceiveDataCollection.Add(DataIndex, UpdateData);
            }
        }

        public void RemoveSource(SocketClient source)
        {
            if (source.OnReceiveDataCollection.ContainsKey(DataIndex))
            {
                source.OnReceiveDataCollection[DataIndex] -= UpdateData;

                if (source.OnReceiveDataCollection[DataIndex] == null)
                {
                    source.OnReceiveDataCollection.Remove(DataIndex);
                }
            }
        }

        public void UpdateData(object sender, ReceiveEventArgs e)
        {
            bool gotException = false;
            try
            {
                Data = (T)e.SerializedObject;
            }
            catch
            {
                gotException = true;
            }
            finally
            {
                if (OnUpdatedData != null && !gotException) OnUpdatedData.Invoke(this, new UpdateDataEventArgs<T>(Data));
            }
        }
    }

}