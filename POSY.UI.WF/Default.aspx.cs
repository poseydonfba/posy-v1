using Microsoft.AspNet.Identity.Owin;
using POSY.Domain.Entities;
using POSY.Domain.Interfaces;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Common.Enums;
using System;
using System.ComponentModel.Composition;
using System.Web;
using System.Web.UI;

namespace POSY.UI.WF
{
    public partial class _Default : Page
    {
        [Import] public IUnitOfWork _unitOfWork { get; set; }
        [Import] public IUsuarioService _usuarioService { get; set; }
        [Import] public IPerfilService _perfilService { get; set; }
        [Import] public IPrivacidadeService _privacidadeService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.UserContext.IsAdministrator)
            //{
            //    this.UserRepository.DoSomeStuff();
            //}

            if (Request.IsAuthenticated)
            {
                //var usuarioId = usuario.UsuarioId;
                var nome = "Poseydon";
                var sobrenome = "Espilacopa";
                var alias = "poseydon";
                var dataNascimento = new DateTime(1985, 12, 12);
                var sexo = Sexo.MASCULINO;
                var estadoCivil = EstadoCivil.SOLTEIRO;
                var pais = "pt-BR";

                try
                {
                    using (_unitOfWork)
                    {
                        var usuario = _usuarioService.Obter(Guid.Parse("ECFC33C9-56E2-4F51-92CA-CF9DF1F7436E"));
                        var perfil = _perfilService.Obter(usuario.UsuarioId);

                        if (perfil == null)
                        {
                            _perfilService.Inserir(new Perfil(usuario.UsuarioId, nome, sobrenome, alias, dataNascimento, sexo, estadoCivil, pais));
                        }
                        else
                        {
                            perfil.Edit(nome, sobrenome, alias, dataNascimento, sexo, estadoCivil, "Frase A", "Descrição A", pais);
                            _perfilService.Alterar(perfil);
                        }

                        //throw new Exception("Error fake");

                        _privacidadeService.Inserir(new Privacidade(usuario.UsuarioId, true, true));

                        _unitOfWork.Commit();
                    }

                    FailureText.Text = "Sucesso UnitOfWork";
                    ErrorMessage.Visible = true;
                }
                catch (Exception ex)
                {
                    FailureText.Text = ex.Message;
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}