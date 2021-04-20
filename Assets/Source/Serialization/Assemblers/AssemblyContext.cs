using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class AssemblyContext
    {
        private Dictionary<Guid, object> _objectMap = new Dictionary<Guid, object>();
        private List<ReferenceRequest> _referenceRequest = new List<ReferenceRequest>();

        public T MakeReferencable<T>(T obj, Guid id) where T : class
        {
            if (id != Guid.Empty) // Only add if the object was assigned an ID during disassembly.
            {
                _objectMap.Add(id, obj);
            }
            return obj;
        }

        public void RequestReference(Guid guid, Action<object> callback)
        {
            _referenceRequest.Add(new ReferenceRequest(guid, callback));
        }

        internal void ReturnReferenceRequests()
        {
            foreach (ReferenceRequest request in _referenceRequest)
            {
                if (_objectMap.TryGetValue(request.Guid, out object reference))
                {
                    request.ReturnReference(reference);
                }
                else
                {
                    throw new InvalidOperationException("Tried to return a Guid for an object that hasn't been assembled in this assembly, or the object hasn't been marked as referencable using context.MakeReferencable(...). You can only reference objects in the same assembly. To reference external objects, use the [ModelFile] attribute.");
                }
            }
        }

        private class ReferenceRequest
        {
            public Guid Guid { get; private set; }
            private Action<object> _callback;

            public ReferenceRequest(Guid guid, Action<object> callback)
            {
                Guid = guid;
                _callback = callback;
            }

            public void ReturnReference(object reference)
            {
                _callback(reference);
            }
        }
    }
}
