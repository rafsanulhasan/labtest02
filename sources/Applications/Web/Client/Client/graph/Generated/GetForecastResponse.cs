using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace LabTest2.Apps.Web.Client.GraphClient
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class GetForecastResponse
        : IGetForecastResponse
    {
        public GetForecastResponse(
            string response, 
            global::System.Collections.Generic.IReadOnlyList<global::LabTest2.Apps.Web.Client.GraphClient.IUserError> errors)
        {
            Response = response;
            Errors = errors;
        }

        public string Response { get; }

        public global::System.Collections.Generic.IReadOnlyList<global::LabTest2.Apps.Web.Client.GraphClient.IUserError> Errors { get; }
    }
}
