using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Context;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Repositories
{
    public class CaoRepository : Repository<Cao, Guid>, ICaoRepository
    {
        public CaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override List<Cao> GetAll()
        {
            return _entity.Include(x => x.Fotos).ToList();
        }

        public override Cao GetById(Guid id)
        {
            return _entity.Include(x => x.Fotos).Include(x => x.HistoricosDeSaude).FirstOrDefault(x => x.CaoId == id);
        }
    }
}
