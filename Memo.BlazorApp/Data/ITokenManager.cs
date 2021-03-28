using System;

namespace Memo.BlazorApp.Data
{
    public interface ITokenManager
    {
        Guid CreateToken(string value);
        string GetValue(Guid token);
    }
}