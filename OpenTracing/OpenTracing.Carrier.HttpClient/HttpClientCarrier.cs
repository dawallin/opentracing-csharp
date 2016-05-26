﻿using OpenTracing.OpenTracing.Propagation;
using OpenTracing.OpenTracing.Context;

namespace OpenTracing.Carrier.HttpClient
{
    public class HttpClientCarrier<T> : IInjectCarrier<T> where T : ISpanContext
    {
        private IContextMapper<T, TextMapFormat> _contextMapper;
        System.Net.Http.HttpClient _httpClient;

        public HttpClientCarrier(IContextMapper<T, TextMapFormat> contextMapper, System.Net.Http.HttpClient httpClient)
        {
            _contextMapper = contextMapper;
            _httpClient = httpClient;
        }

        public void MapFrom(T spanContext)
        {
            var textMap = _contextMapper.MapFrom(spanContext);

            var headers = _httpClient.DefaultRequestHeaders;

            foreach(var property in textMap)
            {
                headers.Add(property.Key, property.Value);
            }
        }
    }
}