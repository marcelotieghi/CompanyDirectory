using System.Linq.Expressions;
using CompanyDirectory.Core.Abstract;

namespace CompanyDirectory.Core.Interface;

public interface IReadRepos<T> where T : BaseEntity
{
    IQueryable<T> GetList();
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
}