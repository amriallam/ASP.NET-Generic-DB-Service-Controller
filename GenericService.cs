using Department_Student_Course.Migrations;
using Department_Student_Course.Models;
using Department_Student_Course.Repositry;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Department.Student.Course.Controllers
{
    public class GenericService : IControllerActions
    {
        protected Context context;
        public GenericService(Context _context) { context = _context; }
        public virtual async Task<ICollection<T>> GetAll<T>() where T : class =>
             await context.Set<T>().ToListAsync();
        public virtual async Task<ICollection<T>> GetAll<T>(string _property) where T : class =>
            await context.Set<T>().Include(_property).ToListAsync();
        public virtual async Task<ICollection<T>> GetAll<T>(string equalityProperty, int equalityValue) where T : class =>
            await context.Set<T>().Where(d => EF.Property<int>(d, equalityProperty) == equalityValue).ToListAsync();
        public virtual async Task<ICollection<T>> GetAll<T>(string _property, string _property2) where T : class =>
            await context.Set<T>().Include(_property).Include(_property2).ToListAsync();
        public virtual async Task<ICollection<T>> GetAll<T>(string _property, string _property2, string equalityProperty, int equalityValue) where T : class =>
            await context.Set<T>().Include(_property).Include(_property2).Where(d => EF.Property<int>(d, equalityProperty) == equalityValue).ToListAsync();
        public virtual async Task<ICollection<T>> GetAll<T>(string _property, string _property2, string equalityProperty, string equalityValue) where T : class =>
            await context.Set<T>().Include(_property).Include(_property2).Where(d => EF.Property<string>(d, equalityProperty) == equalityValue).ToListAsync();
        public virtual async Task<T> GetById<T>(int id) where T : class =>
            await context.Set<T>().FirstOrDefaultAsync(d => EF.Property<int>(d, "Id") == id);
        public virtual async Task<T> GetById<T>(string equalityProperty, int id) where T : class =>
            await context.Set<T>().FirstOrDefaultAsync(d => EF.Property<int>(d, equalityProperty) == id);
        public virtual async Task<T> GetById<T>(int id, string _property) where T : class =>
            await context.Set<T>().Include(_property).FirstOrDefaultAsync(d => EF.Property<int>(d, "Id") == id);
        public virtual async Task<T> GetById<T>(int id, string _property, string _property2) where T : class =>
            await context.Set<T>().Include(_property).Include(_property2).FirstOrDefaultAsync(d => EF.Property<int>(d, "Id") == id);
        public virtual async Task Delete<T>(T obj) where T : class
        {
            context.Set<T>().Remove(obj);
            await context.SaveChangesAsync();
        }
        public virtual async Task Update<T>(T newObject,string PK="Id") where T : class
        {
            T oldObject = await context.Set<T>().FirstOrDefaultAsync(d=> EF.Property<int>(d, PK) == (int)GetPropertyValue(newObject, PK));
            Type objectType = typeof(T);
            foreach (PropertyInfo property in objectType.GetProperties())
            {
                if (property.Name == PK) continue;
                property.SetValue(oldObject, newObject.GetType().GetProperty(property.Name).GetValue(newObject));
            }
            context.Set<T>().Update(oldObject);
            await context.SaveChangesAsync();
        }
        public virtual async Task Add<T>(T obj) where T : class
        {  
            context.Set<T>().Add(obj);
            await context.SaveChangesAsync();
        }
        public DbSet<T> Set<T>() where T : class =>
            context.Set<T>();
        public virtual bool Any<T>(int id) where T : class =>
            context.Set<T>().Any(d => EF.Property<int>(d, "Id") == id);
        private object GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on object of type '{obj.GetType().Name}'");
            }
            return propertyInfo.GetValue(obj);
        }
    }
}
