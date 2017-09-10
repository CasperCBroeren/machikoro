using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service
{
    public interface IDiceService
    {
        int CurrentPips { get; }
        Task<int> GeneratePips(IPlayer currentPlayer);
    }
}