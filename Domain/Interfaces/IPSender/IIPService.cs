using Domain.Model;

namespace Domain.Interfaces.IPSender
{
    public interface IIPService
    {
        IP GetIP();

        IP SorteioIP(int posicao);
    }
}
