using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Model;
using Service.IP;
using Moq;
using Domain.Interfaces.Repository;
using Domain.Interfaces.IPSender;
using Domain.DTO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Linq;
using System;

namespace TestNetWork.SendIP
{
    [TestClass]
    public class SendIPTest
    {
        private readonly IIPService _service;
        private readonly Mock<IIPRepository> _mock = new Mock<IIPRepository>();
        Random random = new Random();
        public SendIPTest()
        {
            _service = new IPService(_mock.Object);
        }

        public List<IP> MockListaIp()
        {
            var json = File.ReadAllText(@"C:\Users\anabr\Documents\IPV4Learning\Infra\Mock\Ips.json", Encoding.GetEncoding("iso-8859-1"));

            var ips = JsonConvert.DeserializeObject<List<IP>>(json);

            return ips;
        }
        [TestMethod]
        public void getIP()
        {
            //arranjo
            var ip = MockListaIp().FirstOrDefault();
   
            var asserto =_mock.Setup(p => p.Query()).Returns(MockListaIp());
            /*----*/
            //ação
            var acao = _service.GetIP();
            //asserto
            Assert.AreEqual(ip,acao);
        }

        [TestMethod]
        public void SorteioIP()
        {
            //range
            var tamanhoLista = MockListaIp().Count();
            int posicaoSorteada = random.Next(tamanhoLista);

            //assert
            var asserto = _mock.Setup(x=>x.Query())
            .Returns(MockListaIp());
           
            //action
            var acao = _service.SorteioIP(posicaoSorteada);

            Assert.AreEqual(acao.id,posicaoSorteada);

        }
    }
}
