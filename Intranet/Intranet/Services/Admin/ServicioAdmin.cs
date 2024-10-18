using Intranet.Data;
using Intranet.Interfaces.Admin;
using Intranet.Modelos.Admin;
using Microsoft.EntityFrameworkCore;
using static Intranet.Pages.Usuario;
using System.DirectoryServices;
using System.Reflection.Metadata.Ecma335;
using static MudBlazor.CategoryTypes;
using Intranet.Pages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Globalization;
using System;
using MudBlazor;
using Serilog;

namespace Intranet.Services.Admin
{
    public class ServicioAdmin : IServicioAdmin
    {
        private readonly IntranetContext intranetContext;
        private IConfiguration configuration;
        public ServicioAdmin(IntranetContext intranetContext, IConfiguration Configuration)
        {

            this.intranetContext = intranetContext;
            this.configuration = Configuration;

        }

        public string BuscarRolDeUsuario(Guid id)
        {
            string result = string.Empty;
            R1_Rol dataRol = new R1_Rol();
            var dataUsuario = intranetContext.u1_Usuario.Where(x => x.Id == id).FirstOrDefault();
            if (dataUsuario != null) 
            dataRol = intranetContext.r1_Rol.Where(x => x.Id == dataUsuario.R1_RolId).FirstOrDefault();
            if (dataRol == null)
                result = "Basico";         
            else
                result = dataRol.Nombre;

            return result;
        }
        public async Task<List<string>> ObtenerPermisosDeUsuario(Guid id)
        {
            List<string> result = new List<string>();
            List<string> ListaPermisosSubCategoria = new List<string>();
            R1_Rol data = new R1_Rol();

            var dataUsuario = intranetContext.u1_Usuario.Where(x => x.Id == id).FirstOrDefault();
            if (dataUsuario != null)
                 data = intranetContext.r1_Rol.Where(x => x.Id == dataUsuario.R1_RolId).FirstOrDefault();
            if (data == null)
                return result;          
            else
            {

                TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;
                var ListaPermisosIdBD = await intranetContext.rol_Permiso.Where(x => x.R1_RolId == data.Id).ToListAsync();
                var ListaPermisosBD = await intranetContext.p1_Permiso.Where(x => ListaPermisosIdBD.Select(y => y.P1_PermisoId).Contains(x.Id)).ToListAsync();

                var ListaPermisosSubCategoria2 = await intranetContext.permisos_SubCategorias.Where(x => ListaPermisosBD.Select(y => y.Id).Contains(x.PermisoId)).ToListAsync();

                List<Permisos_SubCategoria> listaSinDuplicados = ListaPermisosSubCategoria2
                .GroupBy(p => p.SubCategoriaId)
                .Select(g => g.First())
                .ToList();

                var subCategoriaIds = listaSinDuplicados.Select(p => p.SubCategoriaId).ToList();

                var DataCategoriaSubCategoria = await intranetContext.categoria_SubCategorias
                    .Include(u => u.Categoria)
                    .Include(u => u.SubCategoria)
                    .Where(c => subCategoriaIds.Contains(c.SubCategoriaId))
                    .ToListAsync();

                List<Categoria_SubCategoria> listaSinDuplicadosCategoria = DataCategoriaSubCategoria
               .GroupBy(p => p.CategoriaId)
               .Select(g => g.First())
               .ToList();

                // agregano Categoria

                foreach (var Consulta in listaSinDuplicadosCategoria)
                {

                    result.Add(textInfo.ToTitleCase(Consulta.Categoria.Nombre).Replace(" ", ""));
                }

                // agregano SubCategoria

                foreach (var Subconsulta in DataCategoriaSubCategoria)
                {

                    result.Add(textInfo.ToTitleCase(Subconsulta.SubCategoria.Nombre).Replace(" ", ""));
                }

                // agregano permisos

                foreach (var Permiso in ListaPermisosBD)
                {

                    result.Add(textInfo.ToTitleCase(Permiso.Nombre).Replace(" ", ""));
                }

            }
                
            return result;
        }


        public bool ActualizarUsuarioBD(List<U1_Usuario> listaDiretorioActivo) {

            bool result = false;
            try {
                 var ListaBD = intranetContext.u1_Usuario.ToList();
                var IdRolBasico = intranetContext.r1_Rol.Where(x => x.Nombre == "Basico").FirstOrDefault();
                U1_Usuario usuarioNuevo = new U1_Usuario();

                foreach (var directorioActivo in listaDiretorioActivo)
                {
                    if (!ListaBD.Any(x => x.Id == directorioActivo.Id))
                    {
                        usuarioNuevo = new U1_Usuario
                        {
                            Id = directorioActivo.Id,
                            Username = directorioActivo.Username,
                            Email = directorioActivo.Email,                           
                            FirstName = string.IsNullOrEmpty(directorioActivo.FirstName)? directorioActivo.FirstName: directorioActivo.FirstName.ToUpper(),
                            LastName = string.IsNullOrEmpty(directorioActivo.LastName) ? directorioActivo.LastName : directorioActivo.LastName.ToUpper(),
                            Active = directorioActivo.Active,
                            CreatedAt = directorioActivo.CreatedAt
                        };

                        if (IdRolBasico != null)
                            usuarioNuevo.R1_RolId = IdRolBasico.Id;

                        intranetContext.u1_Usuario.Add(usuarioNuevo);
                    }
                    else
                    {
                        var dataExistente = ListaBD.Where(x => x.Id == directorioActivo.Id).FirstOrDefault();
                        if (dataExistente != null) {

                            intranetContext.Attach(dataExistente);
                            dataExistente.Email = directorioActivo.Email;
                            dataExistente.Active = true;
                            dataExistente.CreatedAt = directorioActivo.CreatedAt;
                            dataExistente.UpdatedAt = DateTime.Now;
                            dataExistente.FirstName = string.IsNullOrEmpty(directorioActivo.FirstName) ? directorioActivo.FirstName : directorioActivo.FirstName.ToUpper();
                            dataExistente.LastName = string.IsNullOrEmpty(directorioActivo.LastName) ? directorioActivo.LastName : directorioActivo.LastName.ToUpper();
                            dataExistente.Username = directorioActivo.Username;
                            if (dataExistente.R1_RolId == Guid.Empty || dataExistente.R1_RolId == null)
                                dataExistente.R1_RolId = Guid.Empty;

                        }
                          
                    }
                }

                intranetContext.SaveChanges();
                result = true;
            }
            catch(Exception ex) {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
                result = false;
            }
           
            return result;
        }
        public async Task<List<CrudUsuario>> ObtenerListaUsuario() {

            var listaRol = intranetContext.r1_Rol.Include(u => u.Usuarios).ToList();
            List<CrudUsuario> ListaUsuarioDataGrid = new List<CrudUsuario>();
            foreach (var rol in listaRol) 
            {
                foreach (var usuario in rol.Usuarios)
                {
                    ListaUsuarioDataGrid.Add(new CrudUsuario { Id = usuario.Id, FirstName = usuario.FirstName, Email = usuario.Email, 
                    LastName = usuario.LastName, R1_RolId = usuario.R1_RolId, Username = usuario.Username , NombreRolId = rol.Nombre});
                }
                
            }

            return ListaUsuarioDataGrid;
        }
        public async Task<List<C1_Categoria>> ObtenerListaCategoria()
        {
            var result = intranetContext.c1_Categorias.ToList();

            return result;
        }

        public async Task<List<SubCategoriaDataGrid>> ObtenerListaSubCategoria(string categoriaNombre)
        {
            List<SubCategoriaDataGrid> result =  new List<SubCategoriaDataGrid>();
            if (string.IsNullOrEmpty(categoriaNombre))
            {
                result = await (from subcategoria in intranetContext.s1_SubCategorias
                                join categoriaSubCategoria in intranetContext.categoria_SubCategorias on subcategoria.Id equals categoriaSubCategoria.SubCategoriaId
                                join categoria in intranetContext.c1_Categorias on categoriaSubCategoria.CategoriaId equals categoria.Id
                                select new SubCategoriaDataGrid { Id = subcategoria.Id, Nombre = subcategoria.Nombre, Categoria = categoria }).ToListAsync();
            }
            else 
            {
                result = await (from subcategoria in intranetContext.s1_SubCategorias
                                join categoriaSubCategoria in intranetContext.categoria_SubCategorias on subcategoria.Id equals categoriaSubCategoria.SubCategoriaId
                                join categoria in intranetContext.c1_Categorias on categoriaSubCategoria.CategoriaId equals categoria.Id
                                where categoria.Nombre == categoriaNombre
                                select new SubCategoriaDataGrid { Id = subcategoria.Id, Nombre = subcategoria.Nombre, Categoria = categoria }).ToListAsync();
            }
            

            return result;
        }

        public async Task<List<PermisosDataGrid>> ObtenerListaPermisos()
        {
            var result = await (from permiso in intranetContext.p1_Permiso
                                join permisos_SubCategorias in intranetContext.permisos_SubCategorias on permiso.Id equals permisos_SubCategorias.PermisoId
                                join subCategoria in intranetContext.s1_SubCategorias on permisos_SubCategorias.SubCategoriaId equals subCategoria.Id
                                join categoriaSubCategoria in intranetContext.categoria_SubCategorias on permisos_SubCategorias.SubCategoriaId equals categoriaSubCategoria.SubCategoriaId
                                join categoria in intranetContext.c1_Categorias on categoriaSubCategoria.CategoriaId equals categoria.Id
                                select new PermisosDataGrid { Id = permiso.Id, Nombre = permiso.Nombre, Categoria = categoria, SubCategoria = subCategoria }).ToListAsync();

            return result;
        }
        public async Task<List<R1_Rol>> ObtenerRolesAsync()
        {
            return intranetContext.r1_Rol.ToList();
        }
        public async Task<bool> GuardarUsuario(U1_Usuario EditarUsuario) {
            
            bool result = false;
            try {


                 var usuario = intranetContext.u1_Usuario.Where(u => u.Id == EditarUsuario.Id).FirstOrDefault();
                if (usuario != null) 
                {
                    usuario.R1_RolId = EditarUsuario.R1_RolId;
                    intranetContext.Entry(usuario).State = EntityState.Modified;
                    intranetContext.SaveChanges();
                }


                result = true;


            }
            catch(Exception ex) 
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }
                       
            return result;
        
        }

        public async Task<bool> GuardarCategoria(C1_Categoria nuevaCategoria)
        {

            bool result = false;
            try
            {
                if (!intranetContext.c1_Categorias.Any(x => x.Nombre == nuevaCategoria.Nombre))
                {

                    intranetContext.c1_Categorias.Add(nuevaCategoria);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ActualizarCategoria(C1_Categoria nuevaCategoria)
        {

            bool result = false;
            try
            {
                if (!await ConsultarCategoria(nuevaCategoria))
                {
                    var categoriaBD = intranetContext.c1_Categorias.Where(x => x.Id == nuevaCategoria.Id).FirstOrDefault();
                    categoriaBD.Nombre = nuevaCategoria.Nombre;
                    intranetContext.Entry(categoriaBD).State = EntityState.Modified;
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ActualizarRol(R1_Rol nuevaRol)
        {

            bool result = false;
            try
            {
                if (!await ConsultarRol(nuevaRol))
                {
                    var rolBD = intranetContext.r1_Rol.Where(x => x.Id == nuevaRol.Id).FirstOrDefault();
                    rolBD.Nombre = nuevaRol.Nombre;
                    intranetContext.Entry(rolBD).State = EntityState.Modified;
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ConsultarCategoria(C1_Categoria nuevaCategoria)
        {

            bool result = false;
            try
            {             
                var categoriaData = intranetContext.c1_Categorias.Where(x => x.Id == nuevaCategoria.Id).FirstOrDefault();
                if(categoriaData != null)
                result = String.Equals(categoriaData.Nombre, nuevaCategoria.Nombre, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ConsultarRol(R1_Rol nuevaRol)
        {

            bool result = false;
            try
            {
                var categoriaData = intranetContext.r1_Rol.Where(x => x.Id == nuevaRol.Id).FirstOrDefault();
                if (categoriaData != null)
                    result = String.Equals(categoriaData.Nombre, nuevaRol.Nombre, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ConsultarRolEnUsuario(R1_Rol nuevaRol)
        {

            bool result = false;
            try
            {
                result = !await intranetContext.u1_Usuario.AnyAsync(x => x.R1_RolId == nuevaRol.Id);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;
        }
        public async Task<bool> ConsultarNombreRol(R1_Rol rolData)
        {

            bool result = false;
            try
            {
                result = intranetContext.r1_Rol.Any(x => x.Nombre == rolData.Nombre);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ConsultarNombreCategoria(C1_Categoria CategoriaData)
        {

            bool result = false;
            try
            {
                result = intranetContext.c1_Categorias.Any(x => x.Nombre == CategoriaData.Nombre);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> EliminarCategoria(C1_Categoria CategoriaData)
        {

            bool result = false;
            try
            {
                if (await ConsultarCategoria(CategoriaData))
                {
                   
                    var CategoriaELiminar = intranetContext.c1_Categorias.Find(CategoriaData.Id);

                    intranetContext.c1_Categorias.Remove(CategoriaELiminar);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> GuardarRol(R1_Rol CrearRol)
        {

            bool result = false;
            try
            {
                if (!intranetContext.r1_Rol.Any(x => x.Nombre == CrearRol.Nombre)) 
                {

                    intranetContext.r1_Rol.Add(CrearRol);
                    intranetContext.SaveChanges();

                    result = true;
                }
          
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> EliminarRol(R1_Rol RolData)
        {

            bool result = false;
            try
            {
                if (await ConsultarRol(RolData))
                    if(await ConsultarRolEnUsuario(RolData))
                    {

                        var RolELiminar = intranetContext.r1_Rol.Find(RolData.Id);

                        intranetContext.r1_Rol.Remove(RolELiminar);

                        var rol_PermisosEliminar = await intranetContext.rol_Permiso
                        .Where(x => x.R1_RolId == RolData.Id).ToListAsync();

                        if(rol_PermisosEliminar.Count() > 0)
                        intranetContext.rol_Permiso.RemoveRange(rol_PermisosEliminar);

                        await intranetContext.SaveChangesAsync();

                        result = true;
                    }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ActualizarUsuario() 
        {

            //Indicamos el dominio en el que vamos a buscar al usuario
            // string path = "LDAP://fenixsalud.local";
            string path = configuration["ConexionLDAP"];
            bool resultado = false;


            try
            {
                using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path, configuration["usuarioSincronizarUsuario"], configuration["PasswordSincronizarUsuario"]))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {


                        //Buscamos el usuario con la cuenta user                      
                        searcher.Filter = "(objectclass=" + "user" + ")";
                        var result = searcher.FindAll();
                        var canti = result.Count;

                        if (result != null)
                        {
                            string role = "";
                            string nombreUsuario = "";

                            List<U1_Usuario> listaDiretorioActivo = new List<U1_Usuario>();
                            List<listaPropiedad> propiedades = new List<listaPropiedad>();
 

                            foreach (SearchResult resulta in result)
                            {
                                U1_Usuario u1_Usuario = new U1_Usuario();

                                u1_Usuario.Id = (new Guid((byte[])resulta.Properties["objectguid"][0]));
                                u1_Usuario.FirstName = resulta.Properties["name"][0].ToString();
                                u1_Usuario.Username = resulta.Properties["samaccountname"][0].ToString();
                                u1_Usuario.Active = true;
                                if (resulta.Properties.Contains("mail") && resulta.Properties["mail"].Count > 0)
                                    u1_Usuario.Email = resulta.Properties["mail"][0].ToString();
                                u1_Usuario.CreatedAt = (DateTime)resulta.Properties["whencreated"][0];
                                u1_Usuario.R1_RolId = Guid.Empty;                               

                                listaDiretorioActivo.Add(u1_Usuario);
                            }
                             resultado = ActualizarUsuarioBD(listaDiretorioActivo);                      

                        }
                        else
                        {
                            resultado = false;

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                resultado =  false;
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return resultado;
        }
        public async Task<bool> GuardarSubCategoria(CrearSubCategoria nuevaSubCategoria)
        {
            bool result = false;
            Categoria_SubCategoria NuevaCategoriaSubCategoria = new Categoria_SubCategoria();
            S1_SubCategoria subCategoria = new S1_SubCategoria();
            try
            {
                if (!intranetContext.s1_SubCategorias.Any(x => x.Nombre == nuevaSubCategoria.Nombre))
                {
                    var categoria = intranetContext.c1_Categorias.Where(x => x.Nombre == nuevaSubCategoria.NombreCategoria).FirstOrDefault();

                    subCategoria.Nombre = nuevaSubCategoria.Nombre;
                    subCategoria.Id = Guid.NewGuid();

                    NuevaCategoriaSubCategoria.CategoriaId = categoria.Id;
                    NuevaCategoriaSubCategoria.SubCategoriaId = subCategoria.Id;
                   
                    intranetContext.s1_SubCategorias.Add(subCategoria);
                    intranetContext.categoria_SubCategorias.Add(NuevaCategoriaSubCategoria);
                    intranetContext.SaveChanges();


                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> GuardarPermiso(CrudPermiso permisoData)
        {
            bool result = false;
            Permisos_SubCategoria permisos_SubCategoria = new Permisos_SubCategoria();
            P1_Permiso permiso = new P1_Permiso();
            try
            {
                if (!intranetContext.p1_Permiso.Any(x => x.Nombre == permisoData.Nombre))
                {
                    var Subcategoria = intranetContext.s1_SubCategorias.Where(x => x.Nombre == permisoData.NombreSubCategoria).FirstOrDefault();

                    TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;

                    string NombreTodoEnMiniscula = textInfo.ToTitleCase(permisoData.Nombre.ToLower());
                    string NombrePrimeraLetraMayuscula = textInfo.ToTitleCase(NombreTodoEnMiniscula);

                    permiso.Nombre = NombrePrimeraLetraMayuscula;
                    permiso.Id = Guid.NewGuid();

                    permisos_SubCategoria.PermisoId = permiso.Id;
                    permisos_SubCategoria.SubCategoriaId = Subcategoria.Id;

                    intranetContext.p1_Permiso.Add(permiso);
                    intranetContext.permisos_SubCategorias.Add(permisos_SubCategoria);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ActualizarSubCategoria(CrearSubCategoria SubCategoria)
        {

            bool result = false;
            try
            {
      
                if (await ConsultarSubCategoria(SubCategoria))
                {
                    return false;
                }

                var SubcategoriaBD = await intranetContext.s1_SubCategorias.FirstOrDefaultAsync(x => x.Id == SubCategoria.Id);
                var CategoriaSubcategoriaBD = await intranetContext.categoria_SubCategorias.FirstOrDefaultAsync(x => x.SubCategoriaId == SubCategoria.Id);
                var categoria = await intranetContext.c1_Categorias.FirstOrDefaultAsync(x => x.Nombre == SubCategoria.NombreCategoria);

                if (SubcategoriaBD == null || CategoriaSubcategoriaBD == null || categoria == null)
                {
                    return false;
                }

                SubcategoriaBD.Nombre = SubCategoria.Nombre;
                CategoriaSubcategoriaBD.CategoriaId = categoria.Id;

                intranetContext.Entry(SubcategoriaBD).State = EntityState.Modified;
                intranetContext.Entry(CategoriaSubcategoriaBD).State = EntityState.Modified;
                await intranetContext.SaveChangesAsync();

                result = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> ActualizarPermiso(CrudPermiso PermisoData)
        {

            bool result = false;
            try
            {

                if (await ConsultarPermiso(PermisoData))
                {
                    return false;
                }

                var SubCategoria = await intranetContext.s1_SubCategorias.Where(x => x.Nombre == PermisoData.NombreSubCategoria).FirstOrDefaultAsync();
                var PermisoSubCategoria = await intranetContext.permisos_SubCategorias.FirstOrDefaultAsync(x => x.PermisoId == PermisoData.Id);
                var Permiso = await intranetContext.p1_Permiso.Where( x => x.Id == PermisoData.Id).FirstOrDefaultAsync();

                if (SubCategoria == null || PermisoSubCategoria == null || Permiso == null)
                {
                    return false;
                }

                TextInfo textInfo = new CultureInfo("es-ES", false).TextInfo;

                string NombreTodoEnMiniscula = textInfo.ToTitleCase(PermisoData.Nombre.ToLower());
                string NombrePrimeraLetraMayuscula = textInfo.ToTitleCase(NombreTodoEnMiniscula);

                Permiso.Nombre = NombrePrimeraLetraMayuscula;
                PermisoSubCategoria.SubCategoriaId = SubCategoria.Id;
      
                intranetContext.Entry(Permiso).State = EntityState.Modified;
                intranetContext.Entry(PermisoSubCategoria).State = EntityState.Modified;

                await intranetContext.SaveChangesAsync();

                result = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ConsultarSubCategoria(CrearSubCategoria subCategoria)
        {

            bool result = false;
            try
            {
                 result = intranetContext.s1_SubCategorias.Any(x => x.Id != subCategoria.Id && x.Nombre == subCategoria.Nombre);
              
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ConsultarPermiso(CrudPermiso permiso)
        {

            bool result = false;
            try
            {
                result = intranetContext.p1_Permiso.Any(x => x.Id != permiso.Id && x.Nombre == permiso.Nombre);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<bool> ExistenciaSubCategoria(CrearSubCategoria subCategoria)
        {

            bool result = false;
            try
            {
                result = intranetContext.s1_SubCategorias.Any(x => x.Id == subCategoria.Id);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;
        }

        public async Task<bool> ExistenciaPermiso(CrudPermiso PermisoData)
        {

            bool result = false;
            try
            {
                result = intranetContext.p1_Permiso.Any(x => x.Id == PermisoData.Id);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> EliminarSubCategoria(CrearSubCategoria SubCategoriaData)
        {

            bool result = false;
            try
            {
                if (await ExistenciaSubCategoria(SubCategoriaData))
                {

                    var SubCategoriaELiminar = intranetContext.s1_SubCategorias.Find(SubCategoriaData.Id);
                    var CategoriaSubCategoriaELiminarId = await intranetContext.categoria_SubCategorias.Where( x => x.SubCategoriaId == SubCategoriaData.Id).FirstOrDefaultAsync();
                    var CategoriaSubCategoriaELiminar = intranetContext.categoria_SubCategorias.Find(CategoriaSubCategoriaELiminarId.Id);

                    if (SubCategoriaELiminar == null || SubCategoriaELiminar == null || CategoriaSubCategoriaELiminar == null)                   
                        return false;
                    

                    intranetContext.s1_SubCategorias.Remove(SubCategoriaELiminar);
                    intranetContext.categoria_SubCategorias.Remove(CategoriaSubCategoriaELiminar);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }

        public async Task<bool> EliminarPermiso(CrudPermiso permisdoData)
        {

            bool result = false;
            try
            {
                if (await ExistenciaPermiso(permisdoData))
                {

                    var Permiso = intranetContext.p1_Permiso.Find(permisdoData.Id);
                    var permisos_SubCategoriasId = await intranetContext.permisos_SubCategorias.Where(x => x.PermisoId == permisdoData.Id).FirstOrDefaultAsync();
                    var permisos_SubCategorias = intranetContext.permisos_SubCategorias.Find(permisos_SubCategoriasId.Id);

                    if (Permiso == null || permisos_SubCategoriasId == null || permisos_SubCategorias == null)
                        return false;

                    intranetContext.p1_Permiso.Remove(Permiso);
                    intranetContext.permisos_SubCategorias.Remove(permisos_SubCategorias);
                    intranetContext.SaveChanges();

                    result = true;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
        public async Task<TipoDeRol> ObtenerTiposDeRoles() 
        {
            TipoDeRol tipoDeRol = new TipoDeRol();

            tipoDeRol.Categorias = await intranetContext.c1_Categorias.ToListAsync();
            tipoDeRol.Categorias = tipoDeRol.Categorias.OrderBy(a => a.Nombre).ToList();
            tipoDeRol.SubCategorias = await intranetContext.s1_SubCategorias.ToListAsync();
            tipoDeRol.Permisos = await intranetContext.p1_Permiso.ToListAsync();
            tipoDeRol.Permisos_SubCategorias = await intranetContext.permisos_SubCategorias.ToListAsync();
            tipoDeRol.Categoria_SubCategoria = await intranetContext.categoria_SubCategorias.ToListAsync(); 

            return tipoDeRol;
        }

        public async Task<List<P1_Permiso>> ObtenerListaPermisosDeRol(Guid Id) 
        {
            List<P1_Permiso> result = new List<P1_Permiso>();

            var rol_permiso = await intranetContext.rol_Permiso.Where(x => x.R1_RolId == Id).ToListAsync();

            result = await intranetContext.p1_Permiso.Where(x => rol_permiso.Select(y => y.P1_PermisoId).Contains(x.Id)).ToListAsync();

            return result;  
        }

        public async Task<bool> ActualizarTipoRol(R1_Rol RolAEditar, List<P1_Permiso> ListaPermisosgregar, List<P1_Permiso> listaPermisosEliminar) 
        {
            bool result = false;
            try
            {

                var permisosEliminarIds = listaPermisosEliminar.Select(p => p.Id).ToList();

                var rol_PermisosEliminar = await intranetContext.rol_Permiso
                    .Where(x => permisosEliminarIds.Contains(x.P1_PermisoId))
                    .ToListAsync();

                intranetContext.rol_Permiso.RemoveRange(rol_PermisosEliminar);

                var nuevosRol_Permisos = ListaPermisosgregar.Select(permiso => new Rol_Permiso
                {
                    P1_PermisoId = permiso.Id,
                    R1_RolId = RolAEditar.Id
                }).ToList();

                await intranetContext.rol_Permiso.AddRangeAsync(nuevosRol_Permisos);

                await intranetContext.SaveChangesAsync();

                result = true;
                

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace + ex.InnerException);
            }

            return result;

        }
    }
}
