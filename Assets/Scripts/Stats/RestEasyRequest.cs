using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

/// <summary>
/// Use this class to send REST requests to a server.  One important thing to note, is that you must wrap the
/// requests with a StartCoroutine in order for the request to actually be sent.  If you don't the request will
/// just sit there on your machine and will never hit the server.
/// </summary>
public class RESTEasyRequest {
    public string url = "http://google.com";
    public string body = "";
    public int timeout = -1;
    public List<RESTEasyHeader> headers = new List<RESTEasyHeader>();
    private Action successEmpty;
    private Action<string> successString;
    private Action<string, int> successStringStatus;
    private Action<byte[]> successBytes;
    private Action<RESTEasyResponse> successResponse;
    private Action failureEmpty;
    private Action<string> failureString;
    private Action<string, int> failureStringStatus;
    private Action<byte[]> failureBytes;
    private Action<RESTEasyResponse> failureResponse;

    /// <summary>
    /// Set the URL of this request
    /// </summary>
    /// <param name="url">ex: "http://www.google.com"</param>
    /// <returns>this request</returns>
    public RESTEasyRequest Url(string url) {
        this.url = url;
        return this;
    }

    /// <summary>
    /// Set the boy of this request
    /// </summary>
    /// <param name="body"></param>
    /// <returns>this request</returns>
    public RESTEasyRequest Body(string body) {
        this.body = body;
        return this;
    }

    /// <summary>
    /// Set the timeout in seconds for this request
    /// </summary>
    /// <param name="timeout">defaults to not set</param>
    /// <returns>this request</returns>
    public RESTEasyRequest Timeout(int timeout) {
        this.timeout = timeout;
        return this;
    }

    /// <summary>
    /// Set a header on this request
    /// </summary>
    /// <param name="key">the header name</param>
    /// <param name="value">the value of the header</param>
    /// <returns>this request</returns>
    public RESTEasyRequest Header(string key, string value) {
        headers.Add(new RESTEasyHeader(key, value));
        return this;
    }

    /// <summary>
    /// Set a header on this request
    /// </summary>
    /// <param name="header">the RESTEasyHeader object</param>
    /// <returns>this request</returns>
    public RESTEasyRequest Header(RESTEasyHeader header) {
        headers.Add(header);
        return this;
    }

    /// <summary>
    /// Invokes a given action when this request is successful
    /// </summary>
    /// <param name="success">parameterless action to invoke on success</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnSuccess(Action success) {
        ClearOnSuccess();
        successEmpty = success;
        return this;
    }

    /// <summary>
    /// Invokes a given action when this request is successful
    /// </summary>
    /// <param name="success">invoked with the body of the response as a string</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnSuccess(Action<string> success) {
        ClearOnSuccess();
        successString = success;
        return this;
    }

    /// <summary>
    /// Invokes a given action when this request is successful
    /// </summary>
    /// <param name="success">invoked with the body and status of the response</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnSuccess(Action<string, int> success) {
        ClearOnSuccess();
        successStringStatus = success;
        return this;
    }

    /// <summary>
    /// Invokes a given action when this request is successful
    /// </summary>
    /// <param name="success">invoked with the body of the response as bytes</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnSuccessBytes(Action<byte[]> success) {
        ClearOnSuccess();
        successBytes = success;
        return this;
    }

    /// <summary>
    /// Invokes a given action when this request is successful
    /// </summary>
    /// <param name="success">invoked with the response object</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnSuccessResponse(Action<RESTEasyResponse> success) {
        ClearOnSuccess();
        successResponse = success;
        return this;
    }

    private void ClearOnSuccess() {
        successResponse = null;
        successString = null;
        successBytes = null;
        successStringStatus = null;
        successEmpty = null;
    }

    /// <summary>
    /// Invokes a given action when this request is unsuccessful
    /// </summary>
    /// <param name="failure">parameterless action to invoke on failure</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnFailure(Action failure) {
        ClearOnFailure();
        failureEmpty = failure;
        return this;
    }

    /// <summary>
    /// Invokes a given action when this request is unsuccessful
    /// </summary>
    /// <param name="failure">invoked with the body of the response as a string</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnFailure(Action<string> failure) {
        ClearOnFailure();
        failureString = failure;
        return this;
    }
    
    /// <summary>
    /// Invokes a given action when this request is unsuccessful
    /// </summary>
    /// <param name="failure">invoked with the body and the status of the response</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnFailure(Action<string, int> failure) {
        ClearOnFailure();
        failureStringStatus = failure;
        return this;
    }
    
    /// <summary>
    /// Invokes a given action when this request is unsuccessful
    /// </summary>
    /// <param name="failure">invoked with the body of the response as bytes</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnFailureBytes(Action<byte[]> failure) {
        ClearOnFailure();
        failureBytes = failure;
        return this;
    }
    
    /// <summary>
    /// Invokes a given action when this request is unsuccessful
    /// </summary>
    /// <param name="failure">invoked with the response object</param>
    /// <returns>this request</returns>
    public RESTEasyRequest OnFailureResponse(Action<RESTEasyResponse> failure) {
        ClearOnFailure();
        failureResponse = failure;
        return this;
    }

    private void ClearOnFailure() {
        failureResponse = null;
        failureBytes = null;
        failureString = null;
        failureStringStatus = null;
        failureEmpty = null;
    }

    /// <summary>
    /// Set the METHOD of this request to GET and execute the request
    /// </summary>
    /// <returns>an IEnumerator that you must wrap in a StartCoroutine function</returns>
    public IEnumerator Get() {
        return Get(this);
    }

    /// <summary>
    /// Set the METHOD of this request to PUT and execute the request
    /// </summary>
    /// <returns>an IEnumerator that you must wrap in a StartCoroutine function</returns>
    public IEnumerator Put() {
        return Put(this);
    }

    /// <summary>
    /// Set the METHOD of this request to POST and execute the request
    /// </summary>
    /// <returns>an IEnumerator that you must wrap in a StartCoroutine function</returns>
    public IEnumerator Post() {
        return Post(this);
    }

    /// <summary>
    /// Set the METHOD of this request to DELETE and execute the request
    /// </summary>
    /// <returns>an IEnumerator that you must wrap in a StartCoroutine function</returns>
    public IEnumerator Delete() {
        return Delete(this);
    }

    public override string ToString() {
        return string.Format("{0}", url);
    }

    public static RESTEasyRequest Build() {
        return new RESTEasyRequest();
    }

    private static IEnumerator Get(RESTEasyRequest request) {
        var req = UnityWebRequest.Get(request.url);
        if (request.timeout > 0) req.timeout = request.timeout;
        request.headers.ForEach((header) => req.SetRequestHeader(header.key, header.value));
        using (req) {
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError) HandleFailure(req, request);
            else HandleSuccess(req, request);
        }
    }

    private static IEnumerator Post(RESTEasyRequest request) {
        var req = UnityWebRequest.Post(request.url, "empty");
        if (request.timeout > 0) req.timeout = request.timeout;
        req.uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(request.body));
        request.headers.ForEach((header) => req.SetRequestHeader(header.key, header.value));
        using (req) {
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError) HandleFailure(req, request);
            else HandleSuccess(req, request);
        }
    }

    private static IEnumerator Put(RESTEasyRequest request) {
        var req = UnityWebRequest.Put(request.url, "empty");
        if (request.timeout > 0) req.timeout = request.timeout;
        req.uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(request.body));
        request.headers.ForEach((header) => req.SetRequestHeader(header.key, header.value));
        using (req) {
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError) HandleFailure(req, request);
            else HandleSuccess(req, request);
        }
    }

    private static IEnumerator Delete(RESTEasyRequest request) {
        var req = UnityWebRequest.Delete(request.url);
        if (request.timeout > 0) req.timeout = request.timeout;
        req.downloadHandler = new DownloadHandlerBuffer();
        request.headers.ForEach((header) => req.SetRequestHeader(header.key, header.value));
        using (req) {
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError) HandleFailure(req, request);
            else HandleSuccess(req, request);
        }
    }
    
    private static void HandleSuccess(UnityWebRequest req, RESTEasyRequest handler) {
        if (handler.successBytes != null) handler.successBytes(req.downloadHandler.data);
        else if (handler.successString != null || handler.successStringStatus != null || handler.successResponse != null) {
            var bodyStr = req.downloadHandler.text;
            if (handler.successString != null) handler.successString(bodyStr);
            else if (handler.successStringStatus != null)
                handler.successStringStatus(bodyStr, (int) req.responseCode);
            else handler.successResponse(new RESTEasyResponse((int) req.responseCode, bodyStr, req.GetResponseHeaders(), req));
        } else if (handler.successEmpty != null) handler.successEmpty();
    }

    private static void HandleFailure(UnityWebRequest req, RESTEasyRequest handler) {
        if (handler.failureBytes != null) handler.failureBytes(req.downloadHandler.data);
        else if (handler.failureString != null || handler.failureStringStatus != null || handler.failureResponse != null) {
            var bodyStr = req.downloadHandler.text;
            if (handler.failureString != null) handler.failureString(bodyStr);
            else if (handler.failureStringStatus != null)
                handler.failureStringStatus(bodyStr, (int) req.responseCode);
            else handler.failureResponse(new RESTEasyResponse((int) req.responseCode, bodyStr, req.GetResponseHeaders(), req));
        } else if (handler.failureEmpty != null) handler.failureEmpty();
    }
    
    public class RESTEasyHeader {
        public string key;
        public string value;

        public RESTEasyHeader(string key, string value) {
            this.key = key;
            this.value = value;
        }

        public RESTEasyHeader() {
            key = "";
            value = "";
        }

        public RESTEasyHeader Key(string key) {
            this.key = key;
            return this;
        }

        public RESTEasyHeader Value(string value) {
            this.value = value;
            return this;
        }
    }

    public class RESTEasyResponse {
        public string body;
        public int status;
        public List<RESTEasyHeader> headers = new List<RESTEasyHeader>();
        public UnityWebRequest request;

        public RESTEasyResponse(int status, string body, Dictionary<string, string> headers, UnityWebRequest request) {
            this.status = status;
            this.body = body;
            this.request = request;
            foreach (var header in headers) {
                this.headers.Add(new RESTEasyHeader(header.Key, header.Value));
            }
        }

        public string GetHeader(string name) {
            var h = headers.Find(r => r.key == name);
            if (h != null) return h.value;
            return null;
        }
    }
}