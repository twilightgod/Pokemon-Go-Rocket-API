using POGOProtos.Networking.Envelopes;
using PokemonGo.RocketAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Console
{
    internal class APIFailure : IApiFailureStrategy
    {
        public async Task<ApiOperation> HandleApiFailure(RequestEnvelope request, ResponseEnvelope response)
        {
            await Task.Delay(500);
            return ApiOperation.Retry;
        }

        public void HandleApiSuccess(RequestEnvelope request, ResponseEnvelope response)
        {

        }
    }
}
