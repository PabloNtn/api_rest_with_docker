using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using ApiDocker.Model;
using ApiDocker.Repository;

namespace ApiDocker.Business.Implementations
{
    public class StudentBusinessImplementation : IStudentBusiness
    {
        private IStudentRepository _StudentRepository;


        public StudentBusinessImplementation(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        public ActionResult<List<Student>> FindAll()
        {
            return _StudentRepository.FindAll();
        }

        public ActionResult<Student> FindById(long id)
        {
            var mensagemErro = TratamentoErroParaID(id);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            var chamada = _StudentRepository.FindById(id);
            if (chamada == null)
                return new NotFoundObjectResult("usuario nao encontrado");

            return chamada;
        }

        public ActionResult<Student> Create(Student student)
        {
            var mensagemErro = TratamentoErroParaDadosEstudante(student);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            mensagemErro = TratamentoErroParaID(student.Alu_id);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            return _StudentRepository.Create(student);
        }

        public ActionResult<int> Delete(long id)
        {
            //verificacao ID
            var mensagemErro = TratamentoErroParaID(id);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            //Verifica se foi apagado do banco
            var result = _StudentRepository.Delete(id);
            if (result == null)
                return new BadRequestObjectResult("Erro ao tentar deletar no banco de dados");
            return Convert.ToInt32(id);
        }

        public ActionResult<Student> Update(Student student)
        {
            var mensagemErro = TratamentoErroParaDadosEstudante(student);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            mensagemErro = TratamentoErroParaID(student.Alu_id);
            if (mensagemErro != null)
                return new BadRequestObjectResult(mensagemErro);

            return _StudentRepository.Update(student);
        }

        public string TratamentoErroParaDadosEstudante(Student student)
        {
            double numero = 0;
            DateTime valorParaConversao;
            string DataString = student.Alu_dt_nascimento.ToString();
            var testeConcatenacaoData = DateTime.TryParseExact(DataString,
                    "dd/MM/yyyyTHH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out valorParaConversao);

            return student.Alu_nm == "" ?

                 "No campo nome digite algum valor" :

            double.TryParse(student.Alu_nm, out numero) ?

                 "No campo nome digite letras, nao numeros" :

            student.Alu_nm.Length < 3 || student.Alu_nm.Length > 80 ?

                 "No campo nome digite um nome com mais de 3 letras e menos de 80 letras" :

            student.Alu_nr_tel == "" ?

                 "No campo telefone digite algum valor" :

            !double.TryParse(student.Alu_nr_tel, out numero) ?

                 "No campo telefone digite numeros, nao letras" :

            student.Alu_nr_tel.Length != 11 ?

                 "O telefone precisa de 11 digitos" :

            DataString == "" ?

                 "No campo data digite algum valor" :

           

             null;
        }
        public string TratamentoErroParaID(long id)
        {
            int numero;

            return id < 0 ?

                "Digite uma id acima de 0" :

                 !int.TryParse(id.ToString(), out numero) ?

                  "Digite um id numerico" :

                  null;
        }
    }
}
