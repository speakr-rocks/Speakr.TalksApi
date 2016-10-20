using System.Collections.Generic;

namespace Speakr.TalksApi.DataAccess.Dapper
{
    public interface IDapper
    {
        IEnumerable<T> Query<T>(string sql);
    }
}