
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Interfaces.IPSender;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Service.IP
{

    public class IPService : IIPService
    {
        private readonly IIPRepository _repo;

        public IPService(IIPRepository repo)
        {
            _repo = repo;
        }  

        Domain.Model.IP IIPService.GetIP()
        {
            var ip = _repo.Query().FirstOrDefault();
            return ip;
        }
     
        public Domain.Model.IP SorteioIP(int posicao)
        {
            var ipSorteio = _repo.Query().Where(x => x.id.Equals(posicao)).FirstOrDefault();
            if(ipSorteio != null)
            {
                return ipSorteio;
            }
            return null;
        }

        public IEnumerable<Domain.Model.IP> GetAll()
        {
            return _repo.Query().ToList();
        }
    }
}

