using SmartPlug.Common;

namespace Smartplug.Models
{
    public interface IPlug
    {
        int Id { get; set; }
        bool State { get; set; }
        double CurrentAc { get; }
        IWeakEvent<EventArgs<double>> CurrentAcChanged { get; }
    }
}