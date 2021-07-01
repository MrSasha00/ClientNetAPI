using System.ComponentModel.DataAnnotations;

namespace ClientNetAPI.Repositories.Net
{
    public class NetEntity
    {
        public int NetId { get; set; }
        public string IpAddress { get; set; }
        public string Info { get; set; }
    }
}