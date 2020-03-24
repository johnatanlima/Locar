using Locar.AcessoDados.Interface;
using Locar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class 
    {
        private readonly LocarDbContext _context;

        public RepositorioGenerico(LocarDbContext context)
        {
            _context = context;
        }

        public async Task Atualizar(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(int id) 
        {
            try
            {
                var entity = await PegarPeloId(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task Excluir(string id)
        {
            try
            {
                var entity = await PegarPeloId(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Inserir(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> PegarPeloId(string id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id); //Pegar a instancia passada por seu id, 
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> PegarTodos()
        {
            try
            {
                return _context.Set<TEntity>();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        

    }
}
