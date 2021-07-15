using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class ObjectPool<T> where T : IPoolable, new()
	{
		private Stack stack;
		private int capacity;

		
		public ObjectPool(int capacity)
		{
			stack = new Stack(capacity);

			for (int i = 0; i < capacity; i++)
			{
				AddNewObject();
			}
		}

		private void AddNewObject()
		{
			T obj = new T();
			obj.PoolIsValid = true;
			stack.Push(obj);
			capacity++;
		}


		public void Release(T obj)
		{
			if (obj.PoolIsFree)
			{
				throw new Exception("POOL (" + this + "): Object already released " + obj);
			}
			else if (!obj.PoolIsValid)
			{
				throw new Exception("POOL (" + this + ") Object not valid " + obj);
			}
			obj.Release();
			obj.PoolIsFree = true;
			stack.Push(obj);
		}


		public T Get()
		{
			if (stack.Count == 0)
			{
				AddNewObject();
			}
			T obj = (T)stack.Pop();
			obj.Initialize();
			obj.PoolIsFree = false;
			return obj;
		}
	}
}
