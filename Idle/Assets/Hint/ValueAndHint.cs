using System;
using Unity.VisualScripting;

namespace Hint {
    public readonly struct ValueAndHint<T> where T:  System.IComparable, System.IComparable<T>, System.IConvertible, System.IEquatable<T>, System.IFormattable {
        private readonly Type _typeOfValue;
        public readonly T Value;
        public readonly ETypeHint Hint;

        private ValueAndHint(T value, ETypeHint hint) : this() {
            Value = value;
            Hint = hint;
            _typeOfValue = typeof(T);
        }

        public static ValueAndHint<long> Factory(long value, ETypeHint hint) => new(value, hint);
        
        public static ValueAndHint<ulong> Factory(ulong value, ETypeHint hint) => new(value, hint);
        
        public static ValueAndHint<int> Factory(int value, ETypeHint hint) => new(value, hint);
        
        public static ValueAndHint<uint> Factory(uint value, ETypeHint hint) => new(value, hint);
        
        public static ValueAndHint<float> Factory(float value, ETypeHint hint) => new(value, hint);
        
        public static ValueAndHint<double> Factory(double value, ETypeHint hint) => new(value, hint);

        private U ConvertNumberTo<U>() => (U)(this.Value as object);
        
        public ValueAndHint<U> VaConvertTo<U>() where U : System.IComparable, System.IComparable<U>, System.IConvertible, System.IEquatable<U>, System.IFormattable {
            var toType = typeof(U);

            return toType == typeof(long) ||
                   toType == typeof(ulong) ||
                   toType == typeof(int) ||
                   toType == typeof(uint) ||
                   toType == typeof(float) ||
                   toType == typeof(double)
                ? new ValueAndHint<U>(ConvertNumberTo<U>(), Hint)
                : throw new Exception($"Can Not Convert TypeOf: {typeof(U)}");
        }
        
        public static ValueAndHint<T> operator+(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value + (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value + (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value + (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value + (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value + (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value + (double)(object)value2.Value);
            else {
                throw new Exception($"type not Found TypeOf:{typeof(T)}");
            }

            return new ValueAndHint<T>(num, value.Hint);
        }
        
        public static ValueAndHint<T> operator-(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value - (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value - (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value - (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value - (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value - (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value - (double)(object)value2.Value);
            else {
                throw new Exception($"type not Found TypeOf:{typeof(T)}");
            }

            return new ValueAndHint<T>(num, value.Hint);
        }
        
        public static ValueAndHint<T> operator/(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value / (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value / (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value / (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value / (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value / (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value / (double)(object)value2.Value);
            else {
                throw new Exception($"type not Found TypeOf:{typeof(T)}");
            }

            return new ValueAndHint<T>(num, value.Hint);
        }
        
        public static ValueAndHint<T> operator*(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value * (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value * (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value * (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value * (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value * (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value * (double)(object)value2.Value);
            else {
                throw new Exception($"type not Found TypeOf:{typeof(T)}");
            }

            return new ValueAndHint<T>(num, value.Hint);
        }

        public static bool operator ==(ValueAndHint<T> value, ValueAndHint<T> value2) => value.Value.Equals(value2.Value);

        public static bool operator !=(ValueAndHint<T> value, ValueAndHint<T> value2) => !(value == value2);

        public static bool operator <(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value < (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value < (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value < (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value < (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value < (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value < (double)(object)value2.Value);
            throw new Exception($"type not Found TypeOf:{typeof(T)}");
        }

        public static bool operator >(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value > (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value > (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value > (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value > (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value > (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value > (double)(object)value2.Value);
            throw new Exception($"type not Found TypeOf:{typeof(T)}");
        }

        public static bool operator >=(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value >= (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value >= (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value >= (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value >= (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value >= (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value >= (double)(object)value2.Value);
            throw new Exception($"type not Found TypeOf:{typeof(T)}");
        }

        public static bool operator <=(ValueAndHint<T> value, ValueAndHint<T> value2) {
            var type = typeof(T);
            T num = default;
            
            if (type == typeof(long)) 
                num = (T)(object)((long)(object)value.Value <= (long)(object)value2.Value);
            if (type == typeof(ulong)) 
                num = (T)(object)((ulong)(object)value.Value <= (ulong)(object)value2.Value);
            if (type == typeof(int)) 
                num = (T)(object)((int)(object)value.Value <= (int)(object)value2.Value);
            if (type == typeof(uint)) 
                num = (T)(object)((uint)(object)value.Value <= (uint)(object)value2.Value);
            if (type == typeof(float)) 
                num = (T)(object)((float)(object)value.Value <= (float)(object)value2.Value);
            if (type == typeof(double)) 
                num = (T)(object)((double)(object)value.Value <= (double)(object)value2.Value);
            throw new Exception($"type not Found TypeOf:{typeof(T)}");
        }
    }
}






















