using Memo.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memo.DataAccess.Interfaces
{
    public interface IMemosRepo
    {
        Task<MemoReadModel> GetMemoReadModel(string url);
        Task<MemoEditModel> GetMemoEditModel(string url, string pin);
        Task<bool> CreateMemo(MemoNewModel memo);
        Task<bool> UpdateMemo(MemoEditModel memo, string pin);
        Task<bool> DeleteMemo(string url);
    }
}