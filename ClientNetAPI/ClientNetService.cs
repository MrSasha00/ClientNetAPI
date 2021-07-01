using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientNetAPI.Repositories;
using ClientNetAPI.Repositories.Net;
using System.Net;
using ClientNetAPI.Controllers;

namespace ClientNetAPI
{
    public class NetModel
    {
        public IPAddress IpAddress { get; set; }
        public int Mask { get; set; }
        public string Info { get; set; }
        public int ClientId { get; set; }
    }

    public interface IClientNetServise 
    {
        NetEntity CreateNet(NetDTO net, int clientId);
        void DeleteNetByClient(int idCLient);
        ClientEntity GetClientAndNet(int idClient);
    }

    public class ClientNetService : IClientNetServise
    {
        private INetRepository _netRepository;
        private IClientRepository _clientRepository;

        public ClientNetService(INetRepository netRepository, IClientRepository clientRepository) 
        {
            _netRepository = netRepository;
            _clientRepository = clientRepository;
        }

        public NetEntity CreateNet(NetDTO netDto, int clientId)
        {
            var netModel = NetConverter.DtoToModel(netDto);
            netModel.ClientId = clientId;

            var client = _clientRepository.Get(clientId);
            if (client == null)
                throw new Exception("Клиент не найден");

            var net = _netRepository.Get(clientId);
            if (net != null)
                throw new Exception("За клиентом уже закреплена сеть");

            var netEntity = NetConverter.ModelToEntity(netModel);
            netEntity.NetId = clientId;
            return _netRepository.Create(netEntity);
        }

        public void DeleteNetByClient(int idCLient)
        {
            _netRepository.DeleteByClient(idCLient);
        }

        public ClientEntity GetClientAndNet(int idClient)
        {
            var result = _clientRepository.GetClientAndNet(idClient);
            if (result == null)
                throw new Exception("Клиент не найден");
            return result;
        }
    }
}
