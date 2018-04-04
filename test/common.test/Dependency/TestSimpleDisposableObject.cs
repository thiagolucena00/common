using System;

namespace common.test.Dependency
{
    public class TestSimpleDisposableObject : IDisposable
    {
        public int MyData { get; set; }
        public int DisposeCount { get; set; }

        public TestSimpleDisposableObject()
        {

        }
        public TestSimpleDisposableObject(int myData)
        {
            MyData = myData;
        }

        public int GetMyData()
        {
            return MyData;
        }
        public void Dispose()
        {
            DisposeCount++;
        }
    }
}