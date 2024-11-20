using System;
namespace NSubstituteTest
{
    public interface IMockService
    {
        string GetData();
    }

    public class MockController
    {
        private readonly IMockService _service;

        public MockController(IMockService service)
        {
            _service = service;
        }

        public string GetInfo()
        {
            return _service.GetData();
        }
    }
}

