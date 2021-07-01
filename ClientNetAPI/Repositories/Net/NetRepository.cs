using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNetAPI.Repositories.Net
{
    public interface INetRepository
    {
        NetEntity Create(NetEntity net);
        void DeleteByClient(int idClient);
        NetEntity Get(int id);
    }

    public class NetRepository : INetRepository
    {
        private readonly SqlServerConnectionProvider _provider;

        public NetRepository(SqlServerConnectionProvider provider)
        {
            _provider = provider;
        }
        public NetEntity Create(NetEntity net)
        {      
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "insert into Net (NetId, IpAddress, Info) values (@NetId, @IpAddress, @Info)";
                db.Execute(sql, net);
                return net;
            }
        }

        public void DeleteByClient(int idClient)
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "DELETE FROM Net WHERE NetId = @idClient";
                db.Execute(sql, new { idClient });
            }
        }
        public NetEntity Get(int id)
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "SELECT * FROM Net where NetId= @id";
                var result = db.Query<NetEntity>(sql, new { id }).FirstOrDefault();
                return result;
            }
        }
    }
}
