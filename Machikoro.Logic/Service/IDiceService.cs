using System.Threading.Tasks;
using Machikoro.Logic.GameItems;

namespace Machikoro.Logic.Service
{
    public interface IDiceService
    {
        Task<int> GeneratePips(Player currentPlayer);
    }
}