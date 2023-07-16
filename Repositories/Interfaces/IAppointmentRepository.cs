namespace DocDocGo.Repositories.Interfaces
{
    public interface IAppointmentRepository<T> :IRepository<T>
    {
        Task<T> DeleteAsync(T entity);

    }
}
