using Domain.Model;
using System.Collections.Generic;

namespace Domain.Interfaces.IPSender
{
    public interface IIPService
    {
        IP GetIP();

        IP SorteioIP(int posicao);

        IEnumerable<IP> GetAll();
    }
}
