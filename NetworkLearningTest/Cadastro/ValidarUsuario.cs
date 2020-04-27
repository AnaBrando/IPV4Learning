using Domain;
using Domain.Interfaces.Application;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Usuario;
using System.Collections.Generic;
using System.Linq;

namespace TestNetWork.Usuario
{
    [TestClass]
    public class ValidarUsuario
    {
        private IUsuarioService _service;
        private Mock<IUsuarioRepository> _Mock = new Mock<IUsuarioRepository>();

        #region Mock
        public List<Domain.Model.Usuario> RetornaUser()
        {
            var users = new List<Domain.Model.Usuario>();

            users.Add(new Domain.Model.Usuario
            {
                UserName = "anabrando",
                Email = "ana@gmail.com"
            });
            return users;
        }
        #endregion

        public ValidarUsuario()
        {
            _service = new UsuarioService(_Mock.Object);
        }
        [TestMethod]
        public void ValidaEmail()
        {
            var usuario = RetornaUser().FirstOrDefault();

            var result = _service.EmailIsValid(usuario.Email);

            Assert.IsTrue(result);
        }

     
    }
}

