using Microsoft.EntityFrameworkCore;
using PrestamosMoraDetalle.DAL;
using PrestamosMoraDetalle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrestamosMoraDetalle.BLL
{
    public class MorasBLL
    {
        private Contexto contexto { get; set; }
        private PrestamosBLL prestamosBLL { get; set; }
        
        public MorasBLL(Contexto contexto)
        {
            this.contexto = contexto;
            this.prestamosBLL = new PrestamosBLL(this.contexto);
        }
        public async Task<bool> Guardar(Moras moras)
        {
            if (!await Existe(moras.MoraId))
                return await Insertar(moras);
            else
                return await Modificar(moras);
        }

        public async Task<bool> Existe(int id)
        {
            bool ok = false;
            try
            {
                ok = await contexto.Moras.AnyAsync(m => m.MoraId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Moras moras)
        {
            bool ok = false;

            try
            {
                await contexto.Moras.AddAsync(moras);
                ok = await contexto.SaveChangesAsync() > 0;
                SumarMoras(moras);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Moras moras)
        {
            bool ok = false;
            try
            {
                Detached(moras.MoraId);

                contexto.Database.ExecuteSqlRaw($"Delete FROM MorasDetalle Where MoraId={moras.MoraId}");
                foreach (var item in moras.MorasDetalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(moras).State = EntityState.Modified;
                SumarMoras(moras);

                ok = await contexto.SaveChangesAsync() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Moras> Buscar(int id)
        {
            Moras moras;

            try
            {
                moras = await contexto.Moras.Where(m => m.MoraId == id)
                    .Include(d => d.MorasDetalles)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return moras;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                Detached(id);
                var registro = await Buscar(id);
                RestarMoras(registro);
                if (registro != null)
                {
                    contexto.Moras.Remove(registro);
                    ok = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Moras>> GetMoras()
        {
            List<Moras> lista = new List<Moras>();

            try
            {
                lista = await contexto.Moras.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<Moras>> GetMoras(Expression<Func<Moras, bool>> criterio)
        {
            List<Moras> lista = new List<Moras>();

            try
            {
                lista = await contexto.Moras.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        private void Detached(int id)
        {
            var aux = contexto.Set<Moras>().Local.FirstOrDefault(m => m.MoraId == id);
            if (aux != null)
            {
                contexto.Entry(aux).State = EntityState.Detached;
            }
        }

        private async void SumarMoras(Moras moras)
        {
            foreach (var item in moras.MorasDetalles)
            {
                var prestamo =contexto.Prestamos.Find(item.PrestamoId);

                prestamo.Balance += item.Valor;

               await prestamosBLL.Guardar(prestamo);
            }
        }

        private async void RestarMoras(Moras moras)
        {
            foreach (var item in moras.MorasDetalles)
            {
                var prestamo = contexto.Prestamos.Find(item.PrestamoId);

                prestamo.Balance -= item.Valor;

                await prestamosBLL.Guardar(prestamo);
            }
        }
    }


}
