using ClientNetAPI.Repositories.Net;
using System.ComponentModel.DataAnnotations;

namespace ClientNetAPI.Repositories
{
    public class ClientEntity
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public NetEntity Net { get; set; }
    }
}