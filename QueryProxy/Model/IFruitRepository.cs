using QueryProxy.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryProxy.Model
{
    public interface IFruitRepository
    {
        [Query("SELECT * FROM fruits where tree=?")]
        List<Fruit> GetFruits(string tree);

       
    }
}
