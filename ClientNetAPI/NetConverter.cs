using ClientNetAPI.Controllers;
using ClientNetAPI.Repositories.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClientNetAPI
{
    public static class NetConverter
    {
        public static NetModel EntityToModel(NetEntity netEntity)
        {
            var slashIndex = netEntity.IpAddress.IndexOf("/");
            var netModel = new NetModel();
            netModel.Mask = GetMask(netEntity.IpAddress, slashIndex);

            var ip = netEntity.IpAddress.Substring(0, slashIndex);

            netModel.IpAddress = GetIpAddress(ip);
            netModel.Info = netEntity.Info;

            return netModel;
        }

        public static NetModel DtoToModel(NetDTO netDTO)
        {
            var slashIndex = netDTO.IpAddress.IndexOf("/");
            var netModel = new NetModel();
            netModel.Mask = GetMask(netDTO.IpAddress, slashIndex);

            var ip = netDTO.IpAddress.Substring(0, slashIndex);

            netModel.IpAddress = GetIpAddress(ip);
            netModel.Info = netDTO.Info;

            return netModel;
        }

        public static NetEntity ModelToEntity(NetModel netModel)
        {
            var netEntity = new NetEntity();
            netEntity.NetId = netModel.ClientId;
            netEntity.IpAddress = netModel.IpAddress.ToString() + "/" + netModel.Mask.ToString();
            netEntity.Info = netModel.Info;

            return netEntity;
        }

        private static int GetMask(string address, int slashIndex)
        {
            if (slashIndex == -1)
                throw new FormatException();
            var mask = address.Substring(slashIndex + 1);
            if (mask.Length == 0 || int.Parse(mask) > 32)
                throw new FormatException();
            return int.Parse(mask);
        }

        private static IPAddress GetIpAddress(string ip)
        {
            IPAddress parseIP;
            bool isCorrect = IPAddress.TryParse(ip, out parseIP);
            if (isCorrect)
                return parseIP;
            else
                throw new FormatException();
        }
    }
}
