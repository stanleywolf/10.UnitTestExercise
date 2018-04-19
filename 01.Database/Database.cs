using System;

namespace _01.Database
{
    public class Database
    {

        private const int DEFAULT_CAPACITY = 16;

        private int[] values;
        private int curentIndex;

        public Database()
        {
            this.values = new int[16];
            this.curentIndex = 0;
        }
        public Database(params int[] values)
            :this()
        {
            this.InitializeValues(values);
        }

        private void InitializeValues(int[] inputValues)
        {
            try
            {
                Array.Copy(inputValues, this.values, inputValues.Length);
                this.curentIndex = inputValues.Length;
            }
            catch (ArgumentException e)
            {
                throw new InvalidOperationException("Array is full!",e);
            }
            
        }

        public void Add(int element)
        {
            if (this.curentIndex >= DEFAULT_CAPACITY)
            {
                throw new InvalidOperationException("Arrays full");
            }
            this.values[curentIndex] = element;
            this.curentIndex++;
        }

        public void Remove()
        {
            if (this.curentIndex == 0)
            {
                throw new InvalidOperationException("Array is empty!");
            }
            this.curentIndex--;
            this.values[curentIndex] = default(int);
        }

        public int[] Fetch()
        {
            int[] newArray = new int[curentIndex];
            Array.Copy(this.values,newArray,curentIndex);
            return newArray;
        }
    }
}
