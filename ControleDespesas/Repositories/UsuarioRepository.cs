using ControleDespesas.Database;
using ControleDespesas.Models;
using ControleDespesas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDespesas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ControleDespesasContext _banco;

        public UsuarioRepository(ControleDespesasContext banco)
        {
            _banco = banco;
        }

        public void Atualizar(Usuario usuario)
        {
            _banco.Update(usuario);
            _banco.SaveChanges();
        }

        public void Cadastrar(Usuario usuario)
        {
            _banco.Add(usuario);
            _banco.SaveChanges();
        }

        public Usuario Consultar(int id)
        {
           return _banco.Usuarios.Find(id);
        }

        public Usuario ConsultarPorEmail(string email)
        {
            return _banco.Usuarios.Where(a => a.Email == email).FirstOrDefault();
        }

        public void Excluir(int id)
        {
            Usuario usuario = Consultar(id);
            _banco.Remove(usuario);
            _banco.SaveChanges();
        }

        public Usuario Login(string email, string senha)
        {
            return _banco.Usuarios.Where(u => u.Email == email && u.Senha == senha).FirstOrDefault();
        }
    }
}
