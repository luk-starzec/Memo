using Memo.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memo.DataAccess.Interfaces
{
    public interface IMemosRepo
    {
        Task<MemoReadModel> GetMemoReadModelAsync(string url);
        Task<MemoEditModel> GetMemoEditModelAsync(string url, string pin);
        Task<string[]> ValidateMemoAsync(MemoNewModel memo);
        Task<bool> CreateMemoAsync(MemoNewModel memo);
        Task<MemoReadModel> UpdateMemoAsync(MemoEditModel memo, string pin);
        Task<bool> DeleteMemoAsync(string url);
    }
}