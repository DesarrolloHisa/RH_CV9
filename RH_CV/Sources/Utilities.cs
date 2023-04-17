using Microsoft.AspNetCore.Mvc.Rendering;
using RH_CV.Data;
using RH_CV.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RH_CV.Sources
{
    public static class Utilities
    {
        //Obtener Rol
        public static string GetRol(HttpContext httpContext, ApplicationDbContext _contexto)
        {
            ClaimsPrincipal claimsRol = httpContext.User;
            string userId = "";
            userId = claimsRol.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();

            // Obtener el rol del usuario
            Rol rol = _contexto.Usuario.Where(u => u.User == userId).Select(u => u.Rol).FirstOrDefault();

            // Verificar si el usuario es un administrador
            string userRol = "";
            if (rol != null && rol.Id == 1)
            {
                userRol = "Admin";
            }
            else if (rol != null && rol.Id == 2)
            {
                userRol = "Observador";
            }
            else { userRol = "Empleado"; }

            return userRol;
        }

        public static string GetName(HttpContext httpContext, ApplicationDbContext _contexto)
        {
            ClaimsPrincipal claimsRol = httpContext.User;
            string userId = "";
            userId = claimsRol.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();

            // Obtener el rol del usuario
            Rol rol = _contexto.Usuario.Where(u => u.User == userId).Select(u => u.Rol).FirstOrDefault();

            Usuario user = _contexto.Usuario.Find(userId);

            // Verificar si el usuario es un administrador
            string name = "";

            name = user.PrimerNombre + " " + user.PrimerApellido;

            return name;
        }

        //EncriptarPassword
        public static string EncryptPassword(string password)
        {
            StringBuilder stringBuilder = new();
            Encoding encoding = Encoding.UTF8;

            byte[] result = SHA256.HashData(encoding.GetBytes(password));

            foreach (byte b in result)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        //ListasEnHv
        public static object[] DropDownList(ApplicationDbContext _contexto)
        {

            object[] drop = new object[9];

            drop[0] = _contexto.TipoVinculo.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[1] = _contexto.TipoContrato.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[2] = _contexto.TipoDocumento.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[3] = _contexto.EPS.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[4] = _contexto.FondoPensiones.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[5] = _contexto.FondoCesantias.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[6] = _contexto.Rol.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[7] = _contexto.TipoCargo.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });
            drop[8] = _contexto.TipoVinculacion.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Tipo,
            });


            return drop;
        }


        //private static readonly byte[] Salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

        //public static string EncryptPassword(string password)
        //{
        //    //StringBuilder stringBuilder = new();
        //    //Encoding encoding = Encoding.UTF8;

        //    //byte[] result = SHA256.HashData(encoding.GetBytes(password));

        //    //foreach (byte b in result)
        //    //{
        //    //    stringBuilder.Append(b.ToString("x2"));
        //    //}
        //    //return stringBuilder.ToString();

        //    //if (string.IsNullOrEmpty(password))
        //    //{
        //    //    throw new ArgumentNullException(nameof(password));
        //    //}

        //    using Aes aes = Aes.Create();
        //    Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Salt, 10000);
        //    aes.Key = pbkdf2.GetBytes(32);
        //    aes.IV = pbkdf2.GetBytes(16);

        //    using MemoryStream memoryStream = new();
        //    using CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        //    byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
        //    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //    cryptoStream.FlushFinalBlock();

        //    byte[] cipherTextBytes = memoryStream.ToArray();
        //    return Convert.ToBase64String(cipherTextBytes);
        //}

        //public static string DecryptPassword(string cipherText)
        //{
        //    //if (string.IsNullOrEmpty(cipherText))
        //    //{
        //    //    throw new ArgumentNullException(nameof(cipherText));
        //    //}

        //    try
        //    {
        //        using Aes aes = Aes.Create();
        //        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(cipherText, Salt, 10000);
        //        aes.Key = pbkdf2.GetBytes(32);
        //        aes.IV = pbkdf2.GetBytes(16);

        //        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        //        using MemoryStream memoryStream = new(cipherTextBytes);
        //        using CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        //        using BinaryReader reader = new(cryptoStream);
        //        byte[] plainTextBytes = reader.ReadBytes(cipherTextBytes.Length);
        //        return Encoding.UTF8.GetString(plainTextBytes);
        //    }
        //    catch (CryptographicException)
        //    {
        //        return null;
        //    }
        //}
    }
}
