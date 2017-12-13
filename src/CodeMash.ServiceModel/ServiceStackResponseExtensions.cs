﻿namespace CodeMash.ServiceModel
{
    public static class ServiceStackResponseExtensions
    {
        public static bool IsNull<T>(this ResponseBase<T> obj) where T : class
        {
            return obj == null;
        }

        public static bool HasData<T>(this ResponseBase<T> obj) where T : class
        {
            return obj?.Result != null;
        }

        public static bool WasOk(this ResponseBase<bool> obj) 
        {
            return obj != null && obj.Result;
        }
        
        
    }
}