using Memo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo.DataAccess
{
    public class MemosRepo : IMemosRepo
    {
        private readonly ISqlDataAccess db;

        public MemosRepo(ISqlDataAccess db)
        {
            this.db = db;
        }

        public async Task<List<MemoModel>> GetMemos()
        {
            var sql = "select * from dbo.Memos";

            var rows = await db.LoadData<MemoRow, dynamic>(sql, new { });

            return Rows2Models(rows).ToList();
        }
        public async Task<MemoModel> GetMemoByTitle(string title)
        {
            var sql = "select * from dbo.Memos m where m.Title = @title";

            var rows = await db.LoadData<MemoRow, dynamic>(sql, new { title = title });

            return Rows2Models(rows).FirstOrDefault();
        }

        public async Task<MemoModel> GetMemoByUrl(string url)
        {
            var sql = "select * from dbo.Memos m where m.Url = @url";

            var rows = await db.LoadData<MemoRow, dynamic>(sql, new { url = url });

            return Rows2Models(rows).FirstOrDefault();
        }

        private MemoModel Row2Model(MemoRow row)
            => new MemoModel
            {
                Title = row.Title,
                Created = row.Created,
                EnabledValidTo = row.ValidTo.HasValue,
                ValidTo = row.ValidTo,
            };

        private IEnumerable<MemoModel> Rows2Models(IEnumerable<MemoRow> rows)
            => rows.Select(r => Row2Model(r));
    }
}
