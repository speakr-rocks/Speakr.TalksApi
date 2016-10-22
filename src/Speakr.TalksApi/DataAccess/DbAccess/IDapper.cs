using System.Collections.Generic;

namespace Speakr.TalksApi.DataAccess.DbAccess
{
    public interface IDapper
    {
        IEnumerable<T> Query<T>(string sql);
    }
}