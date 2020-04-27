using Domain.Interfaces.Repository;
using Domain.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infra.Repository
{
    public class IPRepository : IIPRepository
    {
        public List<IP> Query()
        {
            var json = File.ReadAllText(@"..\..\IPV4Learning\Infra\Mock\Ips.json", Encoding.GetEncoding("iso-8859-1"));

            var ips = JsonConvert.DeserializeObject<List<IP>>(json);

            return ips;
        }
    }
}
