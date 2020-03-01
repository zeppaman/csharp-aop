using QueryProxy.Proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryProxy.Model
{
    public abstract class FruitRepositoryImpl
    {
        [Query("SELECT * FROM fruits where tree=?")]
        public abstract List<Fruit> GetFruits(string tree);
    }
}
