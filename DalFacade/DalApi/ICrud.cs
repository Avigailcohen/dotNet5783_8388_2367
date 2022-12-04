﻿using DO;

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        int Add(T item);
        T GetById(int id);
        void Update(T item);
        void Delete(int id);
        public IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);

    }
}
