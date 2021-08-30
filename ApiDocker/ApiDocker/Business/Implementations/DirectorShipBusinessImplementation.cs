using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using ApiDocker.Model;
using ApiDocker.Repository;

namespace ApiDocker.Business.Implementations
{
    public class DirectorShipBusinessImplementation : IDirectorShipBusiness
    {
        private IDirectorShipRepository _directorShipRepository;

        public DirectorShipBusinessImplementation(IDirectorShipRepository directorShipRepository)
        {
            _directorShipRepository = directorShipRepository;
        }

        public List<DirectorShip> FindAll()
        {
            return _directorShipRepository.FindAll();
        }

        public ActionResult<DirectorShip> Create(DirectorShip director)
        {
            var mensagemErro = TratamentoErroParaDadosDiretoria(director);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            mensagemErro = TratamentoErroParaID(director.Dir_id);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            return _directorShipRepository.Create(director);
        }

        public ActionResult<int> Delete(long id)
        {
            //verificacao ID
            var mensagemErro = TratamentoErroParaID(id);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            //Verifica se foi apagado do banco
            var result = _directorShipRepository.Delete(id);
            if (result == null)
                return new BadRequestObjectResult("Erro ao tentar deletar no banco de dados");
            return Convert.ToInt32(id);
        }

        public DirectorShip FindByUserName(string username, string password)
        {
            //var chamada = _directorShipRepository.FindByUserName(username, password);
            //if (chamada == null)
            //    return new NotFoundObjectResult("usuario nao encontrado");
            return _directorShipRepository.FindByUserName(username, password);
        }

        public string TratamentoErroParaDadosDiretoria(DirectorShip director)
        {
            double numero = 0;

            return director.Dir_userName == "" ?

                 "No campo nome do usuario digite algum valor" :

            director.Dir_userName.Length < 3 || director.Dir_userName.Length > 80 ?

                 "No campo nome do usuario digite um nome com mais de 3 letras e menos de 80 letras" :

            director.Dir_pwd == "" ?

                 "No campo password digite algum valor" :

            director.Dir_pwd.Length <= 5 || director.Dir_pwd.Length >= 20 ?

                 "No campo password digite uma senha maior que 5 e menos que 20" :

            director.Dir_role == "" ?

                 "No campo ROLE digite algum valor" :

            double.TryParse(director.Dir_role, out numero) ?

                  "No campo ROLE do usuario digite letras, nao numeros" :

             null;
        }
        public string TratamentoErroParaID(long id)
        {
            int numero;

            return id <= 0 ?

                "Digite uma id acima de 0" :

                 !int.TryParse(id.ToString(), out numero) ?

                  "Digite um id numerico" :

                  null;
        }
    }
}
