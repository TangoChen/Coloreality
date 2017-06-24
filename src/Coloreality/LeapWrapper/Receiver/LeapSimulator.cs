namespace Coloreality.LeapWrapper.Receiver
{
    public class LeapSimulator : SimulatorBase<LeapData>
    {
        public LeapSimulator() : base(LeapData.DataIndex) { }
    }
}