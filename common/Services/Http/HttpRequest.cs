﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Mmm.Platform.IoT.Common.Services.Http
{
    public class HttpRequest : IHttpRequest, IDisposable
    {
        private bool disposedValue = false;
        private readonly MediaTypeHeaderValue defaultMediaType = new MediaTypeHeaderValue("application/json");
        private readonly Encoding defaultEncoding = new UTF8Encoding();

        // Http***Headers classes don't have a public ctor, so we use this class
        // to hold the headers, this is also used for PUT/POST requests body
        private readonly HttpRequestMessage requestContent = new HttpRequestMessage();

        public HttpRequest()
        {
        }

        public HttpRequest(Uri uri)
        {
            this.Uri = uri;
        }

        public HttpRequest(string uri)
        {
            this.SetUriFromString(uri);
        }

        public Uri Uri { get; set; }

        public HttpHeaders Headers => this.requestContent.Headers;

        public MediaTypeHeaderValue ContentType { get; private set; }

        public HttpRequestOptions Options { get; } = new HttpRequestOptions();

        public HttpContent Content => this.requestContent.Content;

        public void AddHeader(string name, string value)
        {
            if (!this.Headers.TryAddWithoutValidation(name, value))
            {
                if (name.ToLowerInvariant() != "content-type")
                {
                    throw new ArgumentOutOfRangeException(name, "Invalid header name");
                }

                this.ContentType = new MediaTypeHeaderValue(value);
            }
        }

        public void SetUriFromString(string uri)
        {
            this.Uri = new Uri(uri);
        }

        public void SetContent(string content)
        {
            this.SetContent(content, this.defaultEncoding, this.defaultMediaType);
        }

        public void SetContent(string content, Encoding encoding)
        {
            this.SetContent(content, encoding, this.defaultMediaType);
        }

        public void SetContent(string content, Encoding encoding, string mediaType)
        {
            this.SetContent(content, encoding, new MediaTypeHeaderValue(mediaType));
        }

        public void SetContent(string content, Encoding encoding, MediaTypeHeaderValue mediaType)
        {
            this.requestContent.Content = new StringContent(content, encoding, mediaType.MediaType);
            this.ContentType = mediaType;
        }

        public void SetContent(StringContent stringContent)
        {
            this.requestContent.Content = stringContent;
            this.ContentType = stringContent.Headers.ContentType;
        }

        public void SetContent<T>(T sourceObject)
        {
            this.SetContent(sourceObject, this.defaultEncoding, this.defaultMediaType);
        }

        public void SetContent<T>(T sourceObject, Encoding encoding)
        {
            this.SetContent(sourceObject, encoding, this.defaultMediaType);
        }

        public void SetContent<T>(T sourceObject, Encoding encoding, string mediaType)
        {
            this.SetContent(sourceObject, encoding, new MediaTypeHeaderValue(mediaType));
        }

        public void SetContent<T>(T sourceObject, Encoding encoding, MediaTypeHeaderValue mediaType)
        {
            var content = JsonConvert.SerializeObject(sourceObject, Formatting.None);
            this.requestContent.Content = new StringContent(content, encoding, mediaType.MediaType);
            this.ContentType = mediaType;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    requestContent.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
