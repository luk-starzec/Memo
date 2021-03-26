using Memo.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memo.DataAccess
{
    public interface IMemosRepo
    {
        Task<MemoModel> GetMemoByTitle(string title);
        Task<MemoModel> GetMemoByUrl(string url);
        Task<List<MemoModel>> GetMemos();
    }
}