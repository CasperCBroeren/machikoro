using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service
{
    public interface IBankService
    {
        Task TransferMoney(Player receiver, int amount, Player sender = null);
    }
}