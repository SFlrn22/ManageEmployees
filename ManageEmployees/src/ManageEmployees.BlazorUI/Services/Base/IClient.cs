﻿namespace ManageEmployees.BlazorUI.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
