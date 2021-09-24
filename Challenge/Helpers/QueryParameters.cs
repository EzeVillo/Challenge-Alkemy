using Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenge.Helpers
{
    public class QueryParameters<TEntity>
        where TEntity : Entity
    {
        public QueryParameters(int pagina, int top)
        {
            Pagina = pagina;
            Top = top;
            Where = null;
            OrderBy = null;
            OrderByAscending = false;
            OrderByDescending = false;
        }

        public int Pagina { get; set; }
        public int Top { get; set; }
        public Expression<Func<TEntity, bool>> Where { get; set; }
        public Func<TEntity, object> OrderBy { get; set; }
        public bool OrderByAscending { get; set; }
        public bool OrderByDescending { get; set; }
    }
}
