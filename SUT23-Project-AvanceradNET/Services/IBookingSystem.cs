namespace SUT23_Project_AvanceradNET.Services
{
    public interface IBookingSystem<T>
    {
        Task<T> GetSingle(int id);
        Task<T> Add(T newEntity);
        Task<T> Update(T Entity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
