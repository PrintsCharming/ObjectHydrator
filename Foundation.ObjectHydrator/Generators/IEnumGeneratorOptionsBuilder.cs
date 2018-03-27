using System;

namespace Foundation.ObjectHydrator.Generators
{
    public interface IEnumGeneratorOptionsBuilder<TEnum> 
        where TEnum : struct, IConvertible
    {
        IEnumGeneratorOptionsBuilder<TEnum> Excluding(TEnum value);
    }
}