using Intranet.Data;
using Intranet.Modelos.Admin;
using Intranet.Pages;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace Intranet.Services
{
    public class CreacionSuperAdmin
    {
        String Username;

        public CreacionSuperAdmin(String Username)
        {
            this.Username = Username;
        
            ExisteUsuario();
        }
        public void ExisteUsuario()
        {
            using (var db = new IntranetContext())
            {
                //R1_Rol RolAdmin = new R1_Rol();
                //R1_Rol Rolbasico = new R1_Rol();
                //if (db.r1_Rol.Count() == 0)
                //{
                //   RolAdmin = new R1_Rol
                //    {
                //        Id = Guid.NewGuid(),
                //        Nombre = "SuperAdmin"
                //    };

                //    db.r1_Rol.Add(RolAdmin);
                //    db.SaveChanges();

                //    Rolbasico = new R1_Rol
                //    {
                //        Id = Guid.NewGuid(),
                //        Nombre = "Basico"
                //    };
                //    db.r1_Rol.Add(Rolbasico);
                //    db.SaveChanges();
                //}
                R1_Rol RolAdmin = new R1_Rol();
                if (db.r1_Rol.Count() == 0)
                {
                    RolAdmin = new R1_Rol
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "SuperAdmin"
                    };

                    R1_Rol Rolbasico = new R1_Rol
                    {
                        Id = Guid.NewGuid(),
                        Nombre = "Basico"
                    };

                    db.r1_Rol.AddRange(new List<R1_Rol> { RolAdmin, Rolbasico });
                    db.SaveChanges();
                }


                if (db.u1_Usuario.Count() == 0)
                {
                    U1_Usuario UsuarioAdmin = new U1_Usuario
                    {
                        Id = Guid.NewGuid(),
                        Username = Username,
                        Email = null,
                        R1_RolId = db.r1_Rol.Count() == 0? RolAdmin.Id: db.r1_Rol.Where( x=> x.Nombre == "superAdmin").Select( u=> u.Id).FirstOrDefault(),
                        FirstName = "Super",
                        LastName = "Administracion",
                        Active = true,
                        CreatedAt = DateTime.Now,
                    };

                    db.u1_Usuario.Add(UsuarioAdmin);
                    db.SaveChanges();

                }

            }

        }
    }
}
