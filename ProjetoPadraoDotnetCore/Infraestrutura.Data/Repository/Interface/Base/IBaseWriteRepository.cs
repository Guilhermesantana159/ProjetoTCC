namespace Infraestrutura.Repository.Interface.Base;

public interface IBaseWriteRepository <T> : IDisposable where T : class
{
    void Add(T entidade);
    void AddRange(List<T> lEntidade);
    void Update(T entidade);
    void UpdateRange(List<T> lEntidade);
    void DeleteById(int id);
    void DeleteRange(List<T> lEntidade);
    public T AddWithReturn(T entidade);
    List<T> AddRangeWithRange(List<T> lEntidade);
}