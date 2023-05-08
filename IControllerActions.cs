using Microsoft.EntityFrameworkCore;

namespace Department_Student_Course.Repositry
{
    public interface IControllerActions
    {
        Task Add<T>(T obj) where T : class;
        bool Any<T>(int id) where T : class;
        Task Delete<T>(T obj) where T : class;
        Task<ICollection<T>> GetAll<T>() where T : class;
        Task<ICollection<T>> GetAll<T>(string _property) where T : class;
        Task<ICollection<T>> GetAll<T>(string equalityProperty, int equalityValue) where T : class;
        Task<ICollection<T>> GetAll<T>(string _property, string _property2) where T : class;
        Task<ICollection<T>> GetAll<T>(string _property, string _property2, string equalityProperty, int equalityValue) where T : class;
        Task<ICollection<T>> GetAll<T>(string _property, string _property2, string equalityProperty, string equalityValue) where T : class;
        Task<T> GetById<T>(int id) where T : class;
        Task<T> GetById<T>(int id, string _property) where T : class;
        Task<T> GetById<T>(int id, string _property, string _property2) where T : class;
        Task<T> GetById<T>(string equalityProperty, int id) where T : class;
        DbSet<T> Set<T>() where T : class;
        Task Update<T>(T newObject, string PK = "Id") where T : class;
    }
}