using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApi.BO
{
    public class ReplyBo<T>
    {
        public bool isSuccess { get; set; }
        private int _statusCode;
        public int statusCode
        {
            get { return _statusCode; }
            set
            {
                isSuccess = _statusCode == 0;
                _statusCode = value;
            }
        }
        [JsonIgnore]
        public string ErrorMessage { get; set; }
        public T ModelBo { get; set; }
    }

    public class ReplyBuilder<T> : ReplyBo<T>
    {
        public ReplyBuilder<T> setErrorMessage(string _errorMessage)
        {
            this.ErrorMessage = _errorMessage;
            return this;
        }
        public ReplyBuilder<T> setStatusCode(int _statusCode)
        {
            this.statusCode = _statusCode;
            return this;
        }
        public ReplyBuilder<T> setModelBo(T _ModelBo)
        {
            this.ModelBo = _ModelBo;
            return this;
        }
    }
}