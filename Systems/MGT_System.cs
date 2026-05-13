using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MGTool.Systems
{
    internal class MGT_System
    {
        public static T FindInside<T>(object parent)
        {
            if (parent == null) return default(T);

            if (parent is T target) return target;

            var type = parent.GetType();
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var field = type.GetFields(flags)
                            .FirstOrDefault(f => typeof(T).IsAssignableFrom(f.FieldType));
            if (field != null) return (T)field.GetValue(parent);

            var prop = type.GetProperties(flags)
                           .FirstOrDefault(p => typeof(T).IsAssignableFrom(p.PropertyType));
            if (prop != null) return (T)prop.GetValue(parent);

            return default(T);
        }

        public static T EnsureInside<T>(object parent)
        {
            T result = FindInside<T>(parent);

            if (Equals(result, default(T)))
            {
                throw new InvalidOperationException(
                    $"[MGTool Error]: В классе '{parent.GetType().Name}' не найден компонент типа '{typeof(T).Name}'. " +
                    "Проверьте, объявлено ли поле этого типа в вашем классе.");
            }

            return result;
        }
    }
}
