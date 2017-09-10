using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service
{
    public interface IDiceService
    {
        int CurrentPips { get; }
        int PipsDice1 { get; }
        int? PipsDice2 { get; }
        Task<int> GeneratePips(IPlayer currentPlayer);
    }
}