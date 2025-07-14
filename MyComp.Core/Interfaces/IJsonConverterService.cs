using System;
using System.Threading.Tasks;

namespace MyComp.Core.Interfaces
{
    public interface IJsonConverterService
    {
        Task<T> DeserializeAsync<T>(string json);
        Task<string> SerializeAsync<T>(T data);
    }
}

