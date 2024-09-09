using Domain.Models.Label;

namespace Domain.Interfaces
{
    public interface ILabelPrinterService
    {
        Task PrintLabelAsync(LabelModel label);
    }
}