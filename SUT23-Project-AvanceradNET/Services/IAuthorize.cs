namespace SUT23_Project_AvanceradNET.Services
{
    public interface IAuthorize<T>
    {
        Task<T> GetAuthorized(string name, string password);
    }
}
