using System;
using ClientNetAPI.Controllers;
using Xunit;
using Moq;
using ClientNetAPI.Repositories;
using System.Collections.Generic;
using ClientNetAPI.Repositories.Net;

namespace ClientNetAPI.Tests
{
    public class TestService
    {
        private SqlServerConnectionProvider provider;
        private IClientRepository clientRepository;
        private INetRepository netRepository;
        private ClientNetService clientNetService;

        public TestService()
        {
            provider = new SqlServerConnectionProvider(new DatabaseSettings { ConnectionString = "Data Source = DESKTOP-2I3B1C7; Initial Catalog = NetDB; Integrated Security = True" });
            netRepository = new NetRepository(provider);
            clientRepository = new ClientRepository(provider);
            clientNetService = new ClientNetService(netRepository, clientRepository);
        }

        [Fact]
        public void CreateNetTest()
        {

            var netDTO = new NetDTO { Info = "someInfo", IpAddress = "46.48.11.244/30" };

            var result = clientNetService.CreateNet(netDTO, 1);
            netRepository.DeleteByClient(1);
            Assert.Equal("46.48.11.244/30", result.IpAddress);
        }

        [Fact]
        public void CreateNetIncorrectAddressTest()
        {
            var netDTO = new NetDTO { Info = "someInfo", IpAddress = "IncorrectAddress" };
            netRepository.DeleteByClient(1);
            Assert.Throws<FormatException>(() => clientNetService.CreateNet(netDTO, 1));
        }

        [Fact]
        public void CreateNetIncorrectMaskTest()
        {
            var netDTO = new NetDTO { Info = "someInfo", IpAddress = "46.48.11.244" };
            netRepository.DeleteByClient(1);
            Assert.Throws<FormatException>(() => clientNetService.CreateNet(netDTO, 1));
        }

        [Fact]
        public void CreateNetIncorrectMaskValueTest()
        {
            var netDTO = new NetDTO { Info = "someInfo", IpAddress = "46.48.11.244/40" };
            netRepository.DeleteByClient(1);
            Assert.Throws<FormatException>(() => clientNetService.CreateNet(netDTO, 1));
        }
        [Fact]
        public void CreateNetIncorrectAddressValueTest()
        {
            var netDTO = new NetDTO { Info = "someInfo", IpAddress = "11.244/40" };
            netRepository.DeleteByClient(1);
            Assert.Throws<FormatException>(() => clientNetService.CreateNet(netDTO, 1));
        }

        [Fact]
        public void GetClientAndNetTest()
        {
            var netEntity = new NetEntity { Info = "someInfo", IpAddress = "6.48.11.244/30", NetId = 1 };
            netRepository.Create(netEntity);
            var client = clientRepository.Get(1);
            client.Net = netEntity;

            var result = clientNetService.GetClientAndNet(1);
            netRepository.DeleteByClient(1);

            var flag = client.Age == result.Age &&
                client.ClientId == result.ClientId &&
                client.Gender == result.Gender &&
                client.Name == result.Name &&
                client.Net.Info == result.Net.Info &&
                client.Net.IpAddress == result.Net.IpAddress &&
                client.Net.NetId == result.Net.NetId;

            Assert.True(flag);

        }

        [Fact]
        public void GetClientAndNetIcrorrectIdTest()
        {
            Assert.Throws<Exception>(() => clientNetService.GetClientAndNet(20));
        }
    }
}