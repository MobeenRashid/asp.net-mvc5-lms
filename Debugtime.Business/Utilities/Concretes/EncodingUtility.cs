using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Debugtime.Business.Utilities.Contracts;

namespace Debugtime.Business.Utilities.Concretes
{
    public class EncodingUtility/*<T> : IEncodingUtility<T> where T : class*/
    {
        public FormUrlEncodedContent MapToUrlEncodedContent<T1>(T1 sourceObject) where T1:class
        {
            if (sourceObject == null)
                return null;

            var sourceType = typeof(T1);
            var sourceProperties = sourceType.GetProperties();


            var collection = new KeyValuePair<string, string>[sourceProperties.Length];
            for (int i = 0; i < sourceProperties.Length; i++)
            {
                collection[i] = new KeyValuePair<string, string>(sourceProperties[i].Name, sourceProperties[i].GetValue(sourceObject).ToString());
            }

            return new FormUrlEncodedContent(collection);
        }
    }
}
