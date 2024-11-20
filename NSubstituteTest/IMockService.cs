using System;

namespace NSubstituteTest
{
    public interface IMockService
    {
        string GetData();
    }

    public interface ILoggingService
    {
        void Log(string message);
    }

    public class MockController
    {
        private readonly IMockService _service;
        private readonly ILoggingService _loggingService;

        public MockController(IMockService service, ILoggingService loggingService)
        {
            _service = service;
            _loggingService = loggingService;
        }

        public string GetInfo()
        {
            try
            {
                var data = _service.GetData();
                _loggingService.Log("Data retrieved: " + data);
                return data;
            }
            catch (Exception ex)
            {
                _loggingService.Log("Error: " + ex.Message);
                throw;
            }
        }
    }
}

