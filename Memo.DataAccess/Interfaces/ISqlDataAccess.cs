using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memo.DataAccess.Interfaces
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, P>(string sql, P parameters);
        Task SaveData<T>(string sql, T parameters);
    }
}