using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service
{
    public interface IBankService
    {
        int BankBalance { get; set; }

        Task<bool> TransferMoney(int amount, IPlayer receiver =null, IPlayer sender = null); 
    }
}