using Domain.Model;
using System.Collections.Generic;

namespace Domain.Interfaces.Repository
{
    public interface IIPRepository
    {
        List <IP> Query();
    }
}
