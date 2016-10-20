using Speakr.TalksApi.DataAccess.Dapper;
using System.Linq;

namespace Speakr.TalksApi.DataAccess
{
    public class Repository<T> : IRepository<T>
    {
        private IDapper _dapper;

        public Repository(IDapper dapper)
        {
            _dapper = dapper;
        }

        public string VerifyConnection()
        {
            return _dapper.Query<string>("SELECT '1'").FirstOrDefault();
        }
    }
}
