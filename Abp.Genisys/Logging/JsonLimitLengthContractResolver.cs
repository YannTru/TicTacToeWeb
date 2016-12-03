using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Abp.Logging
{
    public class JsonLimitLengthContractResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly int _maxPropertyLength;

        public JsonLimitLengthContractResolver(int maxPropertyLength = 50)
        {
            _maxPropertyLength = maxPropertyLength;
        }

        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            var memberValueProvider =  base.CreateMemberValueProvider(member);
            return new JsonLimitLengthValueProvider(memberValueProvider, _maxPropertyLength);
        }

        class JsonLimitLengthValueProvider : IValueProvider
        {
            private readonly IValueProvider _innerValueProvider;
            private readonly int _maxPropertyLength;

            public JsonLimitLengthValueProvider(IValueProvider innerValueProvider, int maxPropertyLength = 50)
            {
                _innerValueProvider = innerValueProvider;
                _maxPropertyLength = maxPropertyLength;
            }

            public void SetValue(object target, object value)
            {
                _innerValueProvider.SetValue(target,value);
            }

            public object GetValue(object target)
            {
                var value = _innerValueProvider.GetValue(target);
                
                var s = value as string;
                if (s != null && s.Length > _maxPropertyLength)
                {
                    return s.Substring(0, _maxPropertyLength) + "..."; // max 50 chars per string property
                }

                return value;
            }
        }
    }

    






}
