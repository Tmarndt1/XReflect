using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XReflect
{
    internal class Chain : IEnumerable<LambdaExpression>
    {
        public Queue<LambdaExpression> Queue { get; private set; } = new Queue<LambdaExpression>();

        public int Count => Queue.Count;

        public IEnumerator<LambdaExpression> GetEnumerator()
        {
            return Queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
