namespace SUT23_Project_AvanceradNET.Services
{
    public interface IAppointment<T>
    {
        Task<IEnumerable<T>> SearchAll(DateTime start, DateTime end);
        Task<IEnumerable<T>> SearchSpecific(DateTime start, DateTime end, int id);

    }
}
