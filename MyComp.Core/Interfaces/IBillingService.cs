using System.Threading.Tasks;

namespace MyComp.Core.Interfaces
{
	public interface IBillingService
	{
        Task<string> GenerateInvoicesAsync(string shipmentJson);
    }
}

