using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memo.BlazorApp.Data
{
    public class TokenManager : ITokenManager
    {
        private Dictionary<Guid, string> store = new Dictionary<Guid, string>();

        public Guid CreateToken(string value)
        {
            var token = Guid.NewGuid();
            store.Add(token, value);
            return token;
        }
        public string GetValue(Guid token)
        {
            if (store.TryGetValue(token, out string value))
                store.Remove(token);
            return value;
        }
    }
}
