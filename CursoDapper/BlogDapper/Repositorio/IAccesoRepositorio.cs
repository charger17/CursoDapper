﻿using BlogDapper.Models;
using System.Security.Claims;

namespace BlogDapper.Repositorio
{
    public interface IAccesoRepositorio
    {
        public List<Usuario> UserLogin(Usuario user);
    }
}
