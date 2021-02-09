using Microsoft.EntityFrameworkCore;
using PrestamosMoraDetalle.DAL;
using RegistroPrestamoBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrestamosMoraDetalle.BLL
{
    public class PersonasBLL
    {
        private Contexto Contexto { get; set; }

        public PersonasBLL(Contexto contexto)
        {
            this.Contexto = contexto;
        }

        public async Task<bool> Guardar(Personas persona)
        {
            if (!await Existe(persona.PersonaId))
                return await Insertar(persona);
            else
                return await Modificar(persona);
        }

        public async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await Contexto.Personas.AnyAsync(p => p.PersonaId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Personas persona)
        {
            bool ok = false;

            try
            {
                await Contexto.Personas.AddAsync(persona);
                ok = await Contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Personas persona)
        {
            bool ok = false;

            try
            {
                var aux = Contexto
                    .Set<Personas>()
                    .Local.FirstOrDefault(p => p.PersonaId == persona.PersonaId);

                if (aux != null)
                {
                    Contexto.Entry(aux).State = EntityState.Detached;
                }

                Contexto.Entry(persona).State = EntityState.Modified;
                ok = await Contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Personas> Buscar(int id)
        {
            Personas persona;

            try
            {
                persona = await Contexto.Personas
                   .Where(p => p.PersonaId == id)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return persona;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;

            try
            {
                var registro = await Contexto.Personas.FindAsync(id);
                if (registro != null)
                {
                    Contexto.Entry(registro).State = EntityState.Deleted;
                    ok = await Contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Personas>> GetPersonas()
        {
            List<Personas> lista = new List<Personas>();

            try
            {
                lista = await Contexto.Personas.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<Personas>> GetPersonas(Expression<Func<Personas, bool>> criterio)
        {
            List<Personas> lista = new List<Personas>();

            try
            {
                lista = await Contexto.Personas.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
