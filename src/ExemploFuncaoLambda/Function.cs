using Amazon.Lambda.Core;
using ExemploFuncaoLambda.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ExemploFuncaoLambda
{
    public class Function
    {
        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            if (input != null)
            {
                var streamTask = await client.GetStreamAsync($"https://viacep.com.br/ws/{input}/json/");
                var endereco = await JsonSerializer.DeserializeAsync<Endereco>(streamTask);
                return endereco.logradouro;
            }
            return "CEP não informado";
            return input?.ToUpper();
        }
    }
}
