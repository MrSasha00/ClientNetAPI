using ClientNetAPI.Repositories.Net;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClientNetAPI.Repositories
{
    public interface IClientRepository
    {
        void Create(ClientEntity client);
        ClientEntity Get(int id);
        void Update(ClientEntity client);
        void Delete(int id);
        List<ClientEntity> GetClients();
        public ClientEntity GetClientAndNet(int idClient);
    }
    
    public class ClientRepository : IClientRepository
    {
        private readonly SqlServerConnectionProvider _provider;

        public ClientRepository(SqlServerConnectionProvider provider)
        {
            _provider = provider;
        }
        
        public void Create(ClientEntity client)
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "insert into Client (Name, Gender, Age) values (@Name, @Gender, @Age)";
                db.Execute(sql, client);
            }
        }

        public ClientEntity Get(int id)
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "SELECT * FROM Client where ClientId = @id";
                var result = db.Query<ClientEntity>(sql, new { id }).FirstOrDefault();
                return result;
            }
        }

        public void Update(ClientEntity client)
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "UPDATE Client SET Name = @Name, Gender = @Gender, Age = @Age where ClientId = @ClientId";
                db.Execute(sql, client);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "DELETE FROM Client WHERE ClientId = @ClientId";
                db.Execute(sql, new { id });
            }
        }

        public List<ClientEntity> GetClients()
        {
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "SELECT * FROM Client";
                var result = db.Query<ClientEntity>(sql).ToList();
                return result;
            }
        }

        public ClientEntity GetClientAndNet(int idClient)
        {
            var p = new { IdClient = idClient };
            using (IDbConnection db = _provider.GetDbConnection())
            {
                var sql = "select Client.*, Net.* from Client inner join Net on Client.ClientId = Net.NetId where Client.ClientId = @IdClient;";
                var result = db.Query<ClientEntity, NetEntity, ClientEntity>(sql,
                    (client, net) => { client.Net = net; return client; }, p, splitOn: "NetId");
                return result.FirstOrDefault();
            }

        }
    }
}