using Memo.DataAccess.Interfaces;
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

        //public async Task<List<MemoEditModel>> GetMemos()
        //{
        //    var sql = "select * from memo.Memos";

        //    var rows = await db.LoadData<MemoRow, dynamic>(sql, new { });

        //    return Rows2Models(rows).ToList();
        //}

        public async Task<MemoReadModel> GetMemoReadModel(string url)
        {
            var row = await GetMemoRow(url);

            return row is null ? null : Row2ReadModel(row);
        }

        public async Task<MemoEditModel> GetMemoEditModel(string url, string pin)
        {
            var row = await GetMemoRow(url);

            if (row is null)
                return null;

            return Row2EditModel(row, pin);
        }

        public async Task<bool> CreateMemo(MemoNewModel memo)
        {
            var encrypted = AesHelper.Encrypt(memo.Text, memo.Pin);

            var sql =
                "insert into memo.Memos(Url, Title, Created, ValidTo, IV, Data) values (@url, @title, GETDATE(), @validTo,  @iv, @data)";
            var param = new
            {
                url = memo.Url,
                title = memo.Title,
                validTo = memo.ValidTo,
                iv = encrypted.iv,
                data = encrypted.data,
            };

            await db.SaveData<dynamic>(sql, param);

            return true;
        }


        public async Task<bool> UpdateMemo(MemoEditModel memo, string pin)
        {
            var encrypted = AesHelper.Encrypt(memo.Text, pin);

            var sql = "update memo.Memos set IV = @iv, Data = @data, ValidTo = @validTo where Url = @url";
            var param = new
            {
                url = memo.Url,
                validTo = memo.ValidTo,
                iv = encrypted.iv,
                data = encrypted.data,
            };

            await db.SaveData<dynamic>(sql, param);

            return true;
        }

        public async Task<bool> DeleteMemo(string url)
        {
            var sql = "delete memo.Memos where Url = @url";
            var param = new { url = url };

            await db.SaveData<dynamic>(sql, param);

            return true;
        }

        private async Task<MemoRow> GetMemoRow(string url)
        {
            var sql =
                "select * from memo.Memos m where m.Url = @url and (m.ValidTo is null or m.ValidTo >= cast(GETDATE() as date))";
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
                    Title = row.Title,
                    EnabledValidTo = row.ValidTo.HasValue,
                    ValidTo = row.ValidTo,
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
            };
    }
}
