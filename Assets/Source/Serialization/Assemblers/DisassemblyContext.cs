using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class DisassemblyContext
    {
        private Dictionary<object, Tuple<Guid, ValueModel>> _objectMap = new Dictionary<object, Tuple<Guid, ValueModel>>(new ReferenceEqualityComparer());
        private List<GuidRequest> _guidRequests = new List<GuidRequest>();

        public T MakeReferencable<T>(object obj, T model) where T : ValueModel
        {
            Guid id = Guid.NewGuid();
            _objectMap.TryAdd(obj, new Tuple<Guid, ValueModel>(id, model));
            return model;
        }

        public void RequestGuid (object obj, Action<Guid> callback)
        {
            _guidRequests.Add(new GuidRequest(obj, callback));
        }

        internal void ReturnGuidRequests ()
        {
            foreach (GuidRequest request in _guidRequests)
            {
                if (_objectMap.TryGetValue(request.Object, out Tuple<Guid, ValueModel> obj))
                {
                    request.ReturnGuid(obj.Item1);
                    obj.Item2.Guid = obj.Item1;
                }
                else
                {
                    throw new InvalidOperationException("Tried to return a Guid for an object that hasn't been assembled in this assembly, or the object hasn't been marked as referencable using context.MakeReferencable(...). You can only reference objects in the same assembly. To reference external objects, use the [ModelFile] attribute.");
                }
            }
        }

        private class GuidRequest
        {
            public object Object { get; private set; }
            private Action<Guid> _callback;

            public GuidRequest(object obj, Action<Guid> callback)
            {
                Object = obj;
                _callback = callback;
            }

            public void ReturnGuid (Guid guid)
            {
                _callback(guid);
            }
        }
    }
}
