using Memo.DataAccess.Interfaces;
using Memo.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<MemoReadModel> GetMemoReadModelAsync(string url)
        {
            var row = await GetMemoRow(url);

            return row is null ? null : Row2ReadModel(row);
        }

        public async Task<MemoEditModel> GetMemoEditModelAsync(string url, string pin)
        {
            var row = await GetMemoRow(url);

            return row is null ? null : Row2EditModel(row, pin);
        }

        public async Task<string[]> ValidateMemoAsync(MemoNewModel memo)
        {
            var errors = new List<string>();

            var sameUrl = await GetMemoRow(memo.Url, includeExpired: true);
            if (sameUrl != null)
                errors.Add("Memo with same url already exists. Choose different memo title");

            return errors.ToArray();
        }

        public async Task<bool> CreateMemoAsync(MemoNewModel memo)
        {
            var encrypted = AesHelper.Encrypt(memo.Text, memo.Pin);

            var sql =
                "insert into memo.Memos(Url, Title, Created, Expires, IV, Data) values (@url, @title, GETDATE(), @expires,  @iv, @data)";
            var param = new
            {
                url = memo.Url,
                title = memo.Title,
                expires = memo.EnabledExpires ? memo.Expires : null,
                iv = encrypted.iv,
                data = encrypted.data,
            };

            await db.SaveData<dynamic>(sql, param);

            return true;
        }

        public async Task<MemoReadModel> UpdateMemoAsync(MemoEditModel memo, string pin)
        {
            var encrypted = AesHelper.Encrypt(memo.Text, pin);

            var sql = "update memo.Memos set IV = @iv, Data = @data, Expires = @expires where Url = @url";
            var param = new
            {
                url = memo.Url,
                expires = memo.EnabledExpires ? memo.Expires : null,
                iv = encrypted.iv,
                data = encrypted.data,
            };

            await db.SaveData<dynamic>(sql, param);

            return await GetMemoReadModelAsync(memo.Url);
        }

        public async Task<bool> DeleteMemoAsync(string url)
        {
            var sql = "delete memo.Memos where Url = @url";
            var param = new { url = url };

            await db.SaveData<dynamic>(sql, param);

            return true;
        }

        private async Task<MemoRow> GetMemoRow(string url, bool includeExpired = false)
        {
            var sql = "select * from memo.Memos m where m.Url = @url";
            if (!includeExpired)
                sql += " and (m.Expires is null or m.Expires >= cast(GETDATE() as date))";
            var param = new { url = url };

            return (await db.LoadData<MemoRow, dynamic>(sql, param)).FirstOrDefault();
        }

        private MemoEditModel Row2EditModel(MemoRow row, string pin)
        {
            var text = AesHelper.Decrypt(row.Data, row.IV, pin);
            return text is null
                ? null
                : new MemoEditModel
                {
                    Url = row.Url,
                    EnabledExpires = row.Expires.HasValue,
                    Expires = row.Expires,
                    Text = text,
                };
        }
        private MemoReadModel Row2ReadModel(MemoRow row)
            => new MemoReadModel
            {
                Url = row.Url,
                Title = row.Title,
                EncryptedData = row.Data,
                IV = row.IV,
                Created = row.Created,
                Expires = row.Expires,
            };
    }
}
